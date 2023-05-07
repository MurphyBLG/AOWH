using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {   
        services.AddTransient<IAccountingService, AccountingService>();
        services.AddTransient<IEmployeeService, EmployeeService>();
        services.AddTransient<ILogInService, LogInService>();
        services.AddTransient<IPositionService, PositionService>();
        services.AddTransient<IShiftService, ShiftService>();
        services.AddTransient<IStockService, StockService>();
        services.AddTransient<IWorkPlanService, WorkPlanService>();

        return services;
    }
}