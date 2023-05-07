using AutoMapper;
using Newtonsoft.Json;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IPositionRepository _positionRepo;
    private readonly IStockRepository _stockRepo;
    private readonly IShiftRepository _shiftRepo;
    private readonly IMapper _mapper;

    public EmployeeService(IPositionRepository positionRepo, IEmployeeRepository employeeRepo, IStockRepository stockRepo, IMapper mapper, IShiftRepository shiftRepo)
    {
        _positionRepo = positionRepo;
        _employeeRepo = employeeRepo;
        _stockRepo = stockRepo;
        _mapper = mapper;
        _shiftRepo = shiftRepo;
    }

    public async Task<RegistrationResponse> RegisterEmployee(RegistrationRequest request)
    {
        Position? employeePosition = await _positionRepo.GetPositionByIdAsync(request.PositionId);

        if (employeePosition is null)
            throw new NullReferenceException($"Должности с ID:{request.PositionId} не существует!");

        if (!CheckStocks(JsonConvert.DeserializeObject<List<int>>(request.Stock!)!))
            throw new NullReferenceException("Складов с такими ID не существует");

        Employee employee = new Employee(
            request.Password,
            request.Name,
            request.Surname,
            request.Patronymic,
            request.Birthday,
            request.PassportNumber,
            request.PassportIssuer,
            request.PassportIssueDate,
            request.StartOfTotalSeniority,
            request.StartOfLuchSeniority,
            request.DateOfTermination,
            request.PositionId,
            request.Salary,
            employeePosition.QuarterlyBonus,
            request.PercentageOfSalaryInAdvance,
            request.Link,
            request.Stock,
            request.ForkliftControl,
            request.RolleyesControl,
            request.DateOfStartInTheCurrentPosition,
            request.DateOfStartInTheCurrentStock);

        try
        {
            await _employeeRepo.AddEmployeeAsync(employee);
        }
        catch(InvalidCastException)
        {
            throw new InvalidDataException($"Сотрудник c номером паспорта \"{employee.PassportNumber}\" уже есть!");
        }
        

        return _mapper.Map<RegistrationResponse>(employee);
    }

    public async Task<GetEmployeeResponse> GetEmployee(Guid id)
    {
        Employee? employee = await _employeeRepo.GetEmployeeById(id);

        if (employee is null)
            throw new NullReferenceException($"Работника с ID:{id} не существует!");
        
        PositionDTO employeePosition = new PositionDTO 
        {
            PositionId = employee.PositionId,
            Name = employee.Position!.Name,
            Salary = employee.Position!.Salary,
            QuarterlyBonus = employee.Position!.QuarterlyBonus,
            InterfaceAccesses =  JsonConvert.DeserializeObject<InterfaceAccesses>(employee.Position!.InterfaceAccesses)!
        };

        return new GetEmployeeResponse(employee, employeePosition, _stockRepo.GetAllStocksNameByEmployee(employee.Stocks));
    }

    public async Task<IEnumerable<AllEmployeesResponse>> GetAllEmployee()
    {
        IEnumerable<AllEmployeesResponse> result = await _employeeRepo.GetAllEmployees(_stockRepo);

        return result;
    }

    public async Task<UpdateEmployeeResponse> UpdateEmployee(Guid id, UpdateEmployeeRequest request)
    {
        Employee? employee = await _employeeRepo.GetEmployeeById(id);

        if (employee is null)
            throw new NullReferenceException($"Работника с ID:{id} не существует!");

        if (!CheckStocks(JsonConvert.DeserializeObject<List<int>>(request.Stocks!)!))
            throw new NullReferenceException("Складов с такими ID не существует");

        EmployeeHistory history = new EmployeeHistory(employee,
            request.PositionId,
            request.Stocks,
            request.Link
        );

        employee.Update(
            request.Name,
            request.Surname,
            request.Password,
            request.Patronymic,
            request.Birthday,
            request.PassportNumber,
            request.PassportIssuer,
            request.PassportIssueDate,
            request.StartOfTotalSeniority,
            request.StartOfLuchSeniority,
            request.DateOfTermination,
            request.PositionId,
            request.Link,
            request.Stocks,
            request.ForkliftControl,
            request.RolleyesControl,
            request.Salary,
            request.PercentageOfSalaryInAdvance,
            request.QuarterlyBonus);

        try
        {
            await _employeeRepo.AddEmployeeHistory(history);
        }
        catch(InvalidDataException)
        {
            throw new InvalidDataException($"Сотрудник c номером паспорта \"{employee.PassportNumber}\" уже есть!");
        }
        

        return _mapper.Map<UpdateEmployeeResponse>(employee);
    }

    public async Task FireEmployee(Guid id, FireEmployeeRequest request)
    {
        Employee? employee = await _employeeRepo.GetEmployeeById(id);
        Guid? firedId = await _positionRepo.GetPositionIdByName("Уволен");

        if (employee is null)
            throw new NullReferenceException($"Работника с ID:{id} не существует!");

        EmployeeHistory history = new EmployeeHistory(employee);

        employee.Fire(request.DateOfTermination, firedId);

        await _employeeRepo.AddEmployeeHistory(history);
    }

    public List<GetAttendanceResponse> GetAttendance(GetAttendanceRequest request)
    {
        IEnumerable<AttendaceEmployee> employeesOfStock = _shiftRepo.GetFullEmployeesByShiftInfo(request.StockId, request.Month, request.Year);
        IEnumerable<ShiftInfoDTO> shiftsOfMonth = _shiftRepo.GetShiftsByMonth(request.StockId, request.Month, request.Year);

        List<GetAttendanceResponse> response = new();
        foreach (AttendaceEmployee employee in employeesOfStock)
        {
            GetAttendanceResponse attendance = new GetAttendanceResponse(
                employee,
                shiftsOfMonth.Where(s => s.EmployeeId == employee.EmployeeId).ToList());

            if (attendance.Shifts.Count() != 0)
            {
                response.Add(attendance);
            }
        }

        return response;
    }

    public bool CheckStocks(List<int> stocks)
    {
        IEnumerable<int> stocksId = _stockRepo.GetAllStocksId();

        foreach(int stock in stocks)
        {
            if (stocksId.Contains(stock))
                return false;
        }

        return true;
    }
}
