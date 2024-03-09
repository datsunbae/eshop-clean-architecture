using CleanArchitecture.Application.Common.Interfaces.Auth;
using CleanArchitecture.Application.Identity.Roles;
using CleanArchitecture.Application.Identity.Tokens;
using CleanArchitecture.Application.Identity.Users;
using CleanArchitecture.Identity.Auth.Jwt;
using CleanArchitecture.Identity.Auth.Permissions;
using CleanArchitecture.Identity.DatabaseContext;
using CleanArchitecture.Identity.Entities;
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
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services
            .AddIdentity(configuration)
            .AddCurrentUser()
            .AddPermissions()
            .AddJwtAuth();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder) =>
        builder
            .UseCurrentUser();
    

    private static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app) =>
        app.UseMiddleware<CurrentUserMiddleware>();

    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

        services.AddDbContext < ApplicationIdentityDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
        
    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

    private static IServiceCollection AddPermissions(this IServiceCollection services) =>
        services
            .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
            .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

    private static IServiceCollection AddJwtAuth(this IServiceCollection services)
    {
        services.AddOptions<JwtSettings>()
            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

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
