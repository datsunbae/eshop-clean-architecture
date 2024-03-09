using Swashbuckle.AspNetCore.SwaggerUI;

namespace CleanArchitecture.Api.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder UseApiApplication(this IApplicationBuilder app)
    {
        return app;
    }

    public static void UseSwaggerExtension(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
        });
    }
}
