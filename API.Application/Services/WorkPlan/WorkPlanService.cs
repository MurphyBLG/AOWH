using AutoMapper;

public class WorkPlanService : IWorkPlanService
{
    private readonly IWorkPlanRepository _workPlanRepo;
    private readonly IMapper _mapper;

    public WorkPlanService(IWorkPlanRepository workPlanRepo, IMapper mapper)
    {
        _workPlanRepo = workPlanRepo;
        _mapper = mapper;
    }

    public async Task AddWorkPlan(Guid employeeId, int year, int month, AddWorkPlanRequest request)
    {
        WorkPlan? workPlan = _workPlanRepo.GetWorkPlanByIdYearMonth(
            employeeId,
            year,
            month);

        if (workPlan is not null)
            throw new NullReferenceException($"Рабочий план для сотрудника ID:{employeeId} на данный промежуток времени уже есть!");

        await _workPlanRepo.AddWorkPlan(new WorkPlan
        {
            WorkPlanId = Guid.NewGuid(),
            Year = year,
            Month = month,
            EmployeeId = employeeId,
            NumberOfDayShifts = request.NumberOfDayShifts,
            NumberOfHoursPerDayShift = request.NumberOfHoursPerDayShift,
            NumberOfNightShifts = request.NumberOfNightShifts,
            NumberOfHoursPerNightShift = request.NumberOfHoursPerNightShift
        });
    }

    public async Task<GetWorkPlanResponse> GetWorkPlan(Guid employeeId, int year, int month)
    {
        WorkPlan? workPlan = _workPlanRepo.GetWorkPlanByIdYearMonth(
            employeeId,
            year,
            month);

        if (workPlan is null)
            throw new NullReferenceException($"Рабочий план для сотрудника ID:{employeeId} не найден");

        return _mapper.Map<GetWorkPlanResponse>(workPlan);
    }
}
