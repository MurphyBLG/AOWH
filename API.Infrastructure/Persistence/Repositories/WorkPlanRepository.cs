using Microsoft.EntityFrameworkCore;

public class WorkPlanRepository : IWorkPlanRepository
{
    private readonly AccountingContext _context;

    public WorkPlanRepository(AccountingContext context)
    {
        _context = context;
    }

    public async Task AddWorkPlan(WorkPlan workPlan)
    {
        await _context.WorkPlans.AddAsync(workPlan);
        
        await _context.SaveChangesAsync();
    }

    public WorkPlan? GetWorkPlanByIdYearMonth(Guid employeeId, int year, int month)
    {
        WorkPlan? workPlan = _context.WorkPlans.AsNoTracking().Where(workPlan => workPlan.EmployeeId == employeeId
            && workPlan.Year == year
            && workPlan.Month == month).SingleOrDefault();

        return workPlan;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
