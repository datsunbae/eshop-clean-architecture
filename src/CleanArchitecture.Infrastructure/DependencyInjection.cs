using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Interfaces.Services;
using CleanArchitecture.Infrastructure.Outbox;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Persistence.Repositories;
using CleanArchitecture.Infrastructure.Persistence.Repositories.Common;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();

        AddPersistence(services, configuration);

        AddBackgroundJobs(services, configuration);

        return services;
    }

    private static void AddPersistence(
        IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                  throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        #endregion
    }

    private static void AddBackgroundJobs(
        IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartz();

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }
}
