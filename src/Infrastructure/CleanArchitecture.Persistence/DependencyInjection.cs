using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.AggregatesModels.Categories.Repository;
using CleanArchitecture.Domain.AggregatesModels.Orders.Repository;
using CleanArchitecture.Domain.AggregatesModels.Products.Repository;
using CleanArchitecture.Persistence.Common;
using CleanArchitecture.Persistence.Outbox;
using CleanArchitecture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                  throw new ArgumentNullException(nameof(configuration));


        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddRepositories();

        services.AddOutbox(configuration);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IOrderRepository, OrderRepositoy>();

        return services;
    }

    public static async Task MigrationsDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
    }

    private static IServiceCollection AddOutbox(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxSettings>(configuration.GetSection("OutboxSettings"));
        return services;
    }

    public static void AddOutBoxJob(this IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var job = scope.ServiceProvider.GetRequiredService<IJobService>();

            string intervalValue = configuration?.GetSection("OutboxSettings:IntervalInMinutes")?.Value;
            int intervalInMinutes = 1; // Default value if configuration is null or if value is not parsable

            if (!string.IsNullOrEmpty(intervalValue) && int.TryParse(intervalValue, out int parsedInterval))
            {
                intervalInMinutes = parsedInterval;
            }

            job.Recurring<ProcessOutboxMessagesJob>("ProcessOutboxMessages", job => job.Execute(), $"*/{intervalInMinutes} * * * *");
        }
    }
}
