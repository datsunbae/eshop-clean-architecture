using CleanArchitecture.Application.Common.ApplicationServices.Auth;
using CleanArchitecture.Application.Features.Identities.Authentication;
using CleanArchitecture.Application.Features.Identities.Roles;
using CleanArchitecture.Application.Features.Identities.Users;
using CleanArchitecture.Identity.Auth;
using CleanArchitecture.Identity.Auth.Jwt;
using CleanArchitecture.Identity.Auth.Permissions;
using CleanArchitecture.Identity.DatabaseContext;
using CleanArchitecture.Identity.Entities;
using CleanArchitecture.Identity.Initialization;
using CleanArchitecture.Identity.Middlewares;
using CleanArchitecture.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ApplicationDbInitializer>();
        services.AddTransient<ApplicationDbSeeder>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services.Configure<AdminSetting>(configuration.GetSection("SecuritySettings:AdminSettings"));

        services
            .AddIdentity(configuration)
            .AddJwtAuth(configuration)
            .AddCurrentUser()
            .AddPermissions();

        return services;
    }

    public static IApplicationBuilder UseInfrastructureIndentity(this IApplicationBuilder builder) =>
        builder
            .UseCurrentUser();

    public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // Create a new scope to retrieve scoped services
        using var scope = services.CreateScope();

        await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeAsync(cancellationToken);
    }

    private static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
        app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

        services.AddDbContext<ApplicationIdentityDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();

        return services;
    }
        
    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer) sp.GetRequiredService<ICurrentUser>());

    private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

    private static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtSettings>()
            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        services.AddTransient<TokenValidatedJwtBearerEvents>();

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!);

        return services;
    }
}
