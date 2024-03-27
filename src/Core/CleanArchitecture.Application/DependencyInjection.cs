using CleanArchitecture.Application.Common.Behaviours;
using CleanArchitecture.Domain.Services.Checkout;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));

            configuration.AddOpenBehavior(typeof(TransactionPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        // Domain - Services
        services.AddTransient<CheckoutService>();

        return services;
    }
}
