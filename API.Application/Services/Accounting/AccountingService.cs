using System.Security.Claims;
using AutoMapper;

public class AccountingService : IAccountingService
{
    private readonly IAccountingRepository _accountingRepo;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IShiftRepository _shiftRepo;
    private readonly IWorkPlanRepository _workPlanRepo;
    private readonly IMapper _mapper;

    public AccountingService(IAccountingRepository accountingRepo, 
                             IEmployeeRepository employeeRepo, 
                             IShiftRepository shiftRepo,
                             IWorkPlanRepository workPlanRepo,
                             IMapper mapper)
    {
        _accountingRepo = accountingRepo;
        _employeeRepo = employeeRepo;
        _shiftRepo = shiftRepo;
        _workPlanRepo = workPlanRepo;
        _mapper = mapper;
    }

    public async Task<List<GetAccountingResponse>> GetAccounting(int year, int month, int stockId)
    {
        IEnumerable<AccountingEmployee> employees = _shiftRepo.GetEmployeesByShiftInfo(stockId, month, year);

        List<GetAccountingResponse> response = new();
        foreach(AccountingEmployee employee in employees)
        {
            Accounting? accounting = await _accountingRepo.GetAccountingByEmployeeIdMonthYear(employee.EmployeeId, month, year);
            WorkPlan? workPlan = _workPlanRepo.GetWorkPlanByIdYearMonth(employee.EmployeeId, year, month);
            ShiftSummary shiftSummary = _shiftRepo.GetShiftSummary(employee.EmployeeId, month, year);

            if (workPlan is null)
                continue;
            
            Accounting newAccounting;

            if (accounting is null)
            {
                newAccounting = new Accounting(
                    employee.EmployeeId,
                    month,
                    year,
                    workPlan.WorkPlanId,
                    employee.Salary * 0.5m);    // Аванс

                await _accountingRepo.AddAccounting(newAccounting);
            }
            else
            {
                newAccounting = accounting;
            }

            newAccounting.Calculate(workPlan,
                                    employee.Salary,
                                    employee.Seniority,
                                    shiftSummary.CountOfDayHours,
                                    shiftSummary.CountOfNightHours,
                                    shiftSummary.Penalties,
                                    shiftSummary.Sends);
            
            response.Add(_mapper.Map<GetAccountingResponse>(newAccounting));
        }
        
        await _accountingRepo.Save();

        return response;
    }

    public async Task UpdateAccountings(ClaimsPrincipal user, List<UpdateAccountingRequest> request)
    {
        Guid employeeid = new Guid(user.FindFirstValue("EmployeeId")!);

        Employee? updaterEmployee = await _employeeRepo.GetEmployeeById(employeeid);

        if (updaterEmployee.Position!.Name != "Начальник склада" && updaterEmployee.Position!.Name != "Администратор")
            throw new UnauthorizedAccessException("У вас недостаточно прав для редактирования");
    
        foreach (var record in request)
        {
            Accounting? accounting = await _accountingRepo.GetAccountingByEmployeeIdMonthYear(record.EmployeeId, record.Month, record.Year);

            if (accounting is null)
                throw new NullReferenceException($"Запись с учетом для сотрудника {record.EmployeeId} не найдена");

            AccountingHistory accountingHistory = new AccountingHistory(
                record.EmployeeId,
                record.Year,
                record.Month,
                record.Mentoring,
                record.Teaching,
                record.Bonus,
                record.Vacation,
                record.Advance,
                accounting.Mentoring,
                accounting.Teaching,
                accounting.Bonus,
                accounting.Vacation,
                accounting.Advance);

            accounting.Update(
                record.Mentoring,
                record.Teaching,
                record.Bonus,
                record.Vacation,
                record.Advance);

            await _accountingRepo.AddAccouningHistory(accountingHistory);
        }
    }
}
