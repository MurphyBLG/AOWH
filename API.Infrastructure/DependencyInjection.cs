using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AccountingContext>(options =>
            options.UseNpgsql(config.GetConnectionString("Db")));

        services.AddSingleton<ITokenBuilder, TokenBuilder>();

        services.AddScoped<IAccountingRepository,AccountingRepository>();
        services.AddScoped<IEmployeeRepository,EmployeeRepository>();
        services.AddScoped<IPositionRepository,PositionRepository>();
        services.AddScoped<IShiftRepository,ShiftRepository>();
        services.AddScoped<IStockRepository,StockRepository>();
        services.AddScoped<IWorkPlanRepository, WorkPlanRepository>();
        return services;
    }
}