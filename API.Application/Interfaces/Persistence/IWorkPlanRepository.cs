public interface IWorkPlanRepository : IRepository
{

    WorkPlan? GetWorkPlanByIdYearMonth(Guid employeeId, int year, int month);
    Task AddWorkPlan(WorkPlan workPlan);
}