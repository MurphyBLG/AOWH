using System.Security.Claims;
using AutoMapper;
using Newtonsoft.Json;

public class ShiftService : IShiftService
{
    private readonly IShiftRepository _shiftRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IStockRepository _stockRepo;
    private readonly IMapper _mapper;

    public ShiftService(IShiftRepository shiftRepo, IEmployeeRepository employeeRepo,IStockRepository stockRepo, IMapper mapper)
    {
        _shiftRepo = shiftRepo;
        _employeeRepo = employeeRepo;
        _stockRepo  = stockRepo;
        _mapper = mapper;
    }

    public async Task<Guid> OpenShift(ClaimsPrincipal user, OpenShiftRequest request)
    {
        Guid employeeid = new Guid(user.FindFirstValue("EmployeeId")!);

        Employee? openerEmployee = await _employeeRepo.GetEmployeeById(employeeid);

        IEnumerable<int> stocks = _stockRepo.GetAllStocksId();

        if (!stocks.Contains(request.StockId))
            throw new NullReferenceException($"Склада ID:{request.StockId} не существует!");

        IEnumerable<Guid> employees = _employeeRepo.GetAllEmployeesId();

        foreach(string id in request.Employees)
        {
            if (!employees.Contains(new Guid(id)))
                throw new NullReferenceException($"Сотрудника ID:{id} не существует!");
        }

        Shift shift = new Shift(
            request.StockId,
            openerEmployee.EmployeeId,
            request.DayOrNight,
            JsonConvert.SerializeObject(request.Employees));

        await _shiftRepo.AddShift(shift);

        return shift.ShiftId;
    }

    public async Task CloseShift(CloseShiftRequest request)
    {
        Shift? shift = await _shiftRepo.GetShiftByIdAsync(request.ShiftId);

        if (shift is null)
            throw new NullReferenceException($"Нет открытой смены ID:{request.ShiftId}!");
            
        shift.Close();

        ShiftHistory shiftHistory = new ShiftHistory(
            shift.ShiftId,
            shift.StockId,
            shift.EmployeeWhoPostedTheShiftId,
            shift.DayOrNight,
            shift.ClosingDateAndTime,
            shift.Employees,
            shift.ClosingDateAndTime,
            shift.LastUpdate);

        await _shiftRepo.AddShiftHistory(shift, shiftHistory);

        List<ShiftInfo> info = new();
        List<Employee> nullEmployees = new();
        List<Guid> notEmployees = new();
        foreach (var kv in request.WorkedHours)
        {
            Guid employeeId = new Guid($"{kv.Key}");
            Mark? employeeMark = await _employeeRepo.GetMarkByEmployeeId(employeeId);
            
            if (employeeMark is null)
            {
                Employee? employee = await _employeeRepo.GetEmployeeById(employeeId);

                if (employee is null)
                    notEmployees.Add(employeeId);
                else
                    nullEmployees.Add(employee);
                
                continue;
            }
                
            info.Add(new ShiftInfo(
                shiftHistory.ShiftHistoryId,
                employeeId,
                employeeMark.MarkDate,
                shiftHistory.DayOrNight,
                kv.Value
            ));

            _employeeRepo.DeleteMark(employeeMark);
        }

        IEnumerable<Mark> marksToRemove = _employeeRepo.GetMarksByStockId(shiftHistory.StockId);

        List<Employee> untrackedEmployees = new();
        foreach (var mark in marksToRemove)
        {
            if (mark.Employee is not null)
                untrackedEmployees.Add(mark.Employee);
        }

        string nullEmployeesMessage = "";
        string untrackedEmployeesMessage = "";
        string notEmployeesMessage = "";
        string errorMessage = "";
        
        if (nullEmployees.Count != 0)
        {
            nullEmployeesMessage = "Следующие сотрудники не были отмечены:";
            foreach(Employee employee in nullEmployees)
            {
                nullEmployeesMessage += $"\n[{employee.EmployeeId}] {employee.Surname} {employee.Name} {employee.Patronymic}";
            }
            errorMessage += nullEmployeesMessage + "\n";
        }

        if (untrackedEmployees.Count != 0)
        {
            untrackedEmployeesMessage = "Следующие сотрудники были отмечены, но не были назначены на смену:";
            foreach(Employee employee in untrackedEmployees)
            {
                untrackedEmployeesMessage += $"\n[{employee.EmployeeId}] {employee.Surname} {employee.Name} {employee.Patronymic}";
            }
            errorMessage += untrackedEmployeesMessage + "\n";
        }

        if (notEmployees.Count != 0)
        {
            notEmployeesMessage = "Сотрудников с такими ID не существует:";
            foreach(Guid id in notEmployees)
            {
                notEmployeesMessage += $"\n{id}";
            }
            errorMessage += notEmployeesMessage + "\n";
        }

        if (errorMessage != "")
            throw new NullReferenceException(errorMessage);

        _employeeRepo.DeleteMarks(marksToRemove);
        await _shiftRepo.AddShiftInfos(info);

    }

    public async Task<GetCurrentShiftResponse> GetCurrentShift(int stockId)
    {
        Shift? shift = _shiftRepo.GetShiftByStock(stockId);

        if (shift is null)
            throw new NullReferenceException($"Склада ID:{stockId} не существует!");

        List<string> employeeIds = JsonConvert.DeserializeObject<List<string>>(shift.Employees)!;
        List<EmployeeDTO> employees = new();
        foreach (string employeeId in employeeIds)
        {
            Employee? employee = await _employeeRepo.GetEmployeeById(new Guid(employeeId));

            employees.Add(_mapper.Map<EmployeeDTO>(employee));
        }

        return new GetCurrentShiftResponse(
            shift.ShiftId,
            shift.DayOrNight,
            employees);
    }

    public async Task UpdateCurrentShift(Guid shiftId, UpdateShiftRequest request)
    {
        Shift? shift = await _shiftRepo.GetShiftByIdAsync(shiftId);

        if (shift is null)
            throw new NullReferenceException($"Смена ID:{shiftId} не существует!");

        IEnumerable<Guid> employees = _employeeRepo.GetAllEmployeesId();

        foreach(string id in request.Employees)
        {
            if (!employees.Contains(new Guid(id)))
                throw new NullReferenceException($"Сотрудника ID:{id} не существует!");
        }

        shift.Update(
            JsonConvert.SerializeObject(request.Employees),
            request.DayOrNight);

        await _shiftRepo.Save();
    }

    public async Task UpdateShiftInfo(Guid shiftInfoId, UpdateShiftInfoRequest request)
    {
        ShiftInfo? shiftInfo = await _shiftRepo.GetShiftInfoById(shiftInfoId);

        if (shiftInfo is null)
            throw new NullReferenceException($"Инфорации о смене ID:{shiftInfoId} не существует!");

        shiftInfo.Update(
            request.Penalty,
            request.PenaltyComment,
            request.Penalty,
            request.PenaltyComment);

        await _shiftRepo.Save();
    }
}