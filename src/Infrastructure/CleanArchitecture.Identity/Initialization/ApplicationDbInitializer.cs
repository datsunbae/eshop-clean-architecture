using CleanArchitecture.Identity.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Identity.Initialization;

internal class ApplicationDbInitializer
{
    private readonly ApplicationIdentityDbContext _dbContext;
    private readonly ApplicationDbSeeder _dbSeeder;
    private readonly ILogger<ApplicationDbInitializer> _logger;

    public ApplicationDbInitializer(ApplicationIdentityDbContext dbContext, ApplicationDbSeeder dbSeeder, ILogger<ApplicationDbInitializer> logger)
    {
        _dbContext = dbContext;
        _dbSeeder = dbSeeder;
        _logger = logger;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Database.GetMigrations().Any())
        {
            if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
            {
                _logger.LogInformation("Applying Migrations for database");
                await _dbContext.Database.MigrateAsync(cancellationToken);
            }

            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                _logger.LogInformation("Connection to database succeeded.");
                await _dbSeeder.SeedDatabaseAsync(_dbContext, cancellationToken);
            }
        }
    }
}
