using CleanArchitecture.Application.Common.ApplicationServices.BackgroundJob;
using CleanArchitecture.Application.Common.ApplicationServices.Caching;
using CleanArchitecture.Application.Common.ApplicationServices.Email;
using CleanArchitecture.Application.Common.ApplicationServices.FileStorage;
using CleanArchitecture.Application.Common.ApplicationServices.Serializer;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.BackgroundJob;
using CleanArchitecture.Infrastructure.BackgroundJobs;
using CleanArchitecture.Infrastructure.Caching;
using CleanArchitecture.Infrastructure.Email;
using CleanArchitecture.Infrastructure.FileStorage;
using CleanArchitecture.Infrastructure.Notification;
using CleanArchitecture.Infrastructure.Serializer;
using FSH.WebApi.Infrastructure.Caching;
using FSH.WebApi.Infrastructure.Mailing;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            .AddNotifications()
            .AddFileStorage()
            .AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration configuration, IWebHostEnvironment env)
    {
        builder
            .UseHangfireDashboard(configuration)
            .UseFileStorage(env);

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
        services.AddScoped<IJobService, HangfireService>();

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
        services.AddScoped<ICacheKeyService, CacheKeyService>();

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
                        EndPoints = { settings.RedisURL! }
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

    private static IServiceCollection AddMail(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<IMailService, SmtpMailService>();
        return services;
    }

    private static IApplicationBuilder UseFileStorage(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        //var test = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files"));

        //app.UseStaticFiles(new StaticFileOptions()
        //{
        //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
        //    RequestPath = new PathString("/Files")
        //});

        var filesPath = Path.Combine(env.ContentRootPath, "Files");

        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(filesPath),
            RequestPath = new PathString("/Files")
        });

        return app;
    }
        

    public static IServiceCollection AddFileStorage(this IServiceCollection services) =>
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

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
