public interface IWorkPlanService
{
    Task AddWorkPlan(Guid employeeId, int year, int month, AddWorkPlanRequest request);
    Task<GetWorkPlanResponse> GetWorkPlan(Guid employeeId, int year, int month);
}