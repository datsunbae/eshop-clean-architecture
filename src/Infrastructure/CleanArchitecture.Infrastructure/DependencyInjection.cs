using CleanArchitecture.Application.Common.Caching;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.BackgroundJobs;
using CleanArchitecture.Infrastructure.Caching;
using CleanArchitecture.Infrastructure.Notifications;
using CleanArchitecture.Infrastructure.Services;
using FSH.WebApi.Infrastructure.Caching;
using FSH.WebApi.Infrastructure.Mailing;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddBackgroundJobs(config)
            .AddCaching(config)
            .AddMail(config)
            .AddNotifications();

        services.AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        builder
            .UseHangfireDashboard()
            .UseFileStorage();

        return builder;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapNotifications();

        return builder;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISerializerService, NewtonSoftService>();
        services.AddScoped<IDateTimeService, DateTimeService>();

        return services;
    }

    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddHangfireServer();

        services.AddHangfire(hangfireConfig => hangfireConfig
            .UseSqlServerStorage(config.GetConnectionString("DefaultConnection"))
            .UseFilter(new LogJobFilter()));

        return services;
    }

    private static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration config)
    {
        var dashboardOptions = config.GetSection("HangfireSettings:Dashboard").Get<DashboardOptions>();
        if (dashboardOptions is null) throw new Exception("Hangfire Dashboard is not configured.");
        dashboardOptions.Authorization = new[]
        {
           new HangfireCustomBasicAuthenticationFilter
           {
                User = config.GetSection("HangfireSettings:Credentials:User").Value!,
                Pass = config.GetSection("HangfireSettings:Credentials:Password").Value!
           }
        };

        return app.UseHangfireDashboard(config["HangfireSettings:Route"], dashboardOptions);
    }

    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(nameof(CacheSettings)).Get<CacheSettings>();
        if (settings == null) return services;
        if (settings.UseDistributedCache)
        {
            if (settings.PreferRedis)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = settings.RedisURL;
                    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                    {
                        AbortOnConnectFail = true,
                        EndPoints = { settings.RedisURL }
                    };
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            services.AddTransient<ICacheService, DistributedCacheService>();
        }
        else
        {
            services.AddTransient<ICacheService, LocalCacheService>();
        }

        services.AddMemoryCache();
        return services;
    }

    private static IServiceCollection AddMail(this IServiceCollection services, IConfiguration config) =>
        services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));

    private static IApplicationBuilder UseFileStorage(this IApplicationBuilder app) =>
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
            RequestPath = new PathString("/Files")
        });

    private static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    private static IEndpointRouteBuilder MapNotifications(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHub<NotificationHub>("/notifications", options =>
        {
            options.CloseOnAuthenticationExpiration = true;
        });
        return endpoints;
    }
}
