using System.Reflection;

namespace Web.Api.Extensions;

internal static class ServiceCollectionExtensions
{
   internal static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        /*services*//*.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        *//*AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        })*/

        return services;
    }

    internal static WebApplication MapVersionedEndpoints(this WebApplication app)
    {
        RouteGroupBuilder versionedGroup = app.WithVersioning();
        app.MapEndpoints(versionedGroup);

        return app;
    }

    internal static IServiceCollection AddMappings(this IServiceCollection services, Assembly assembly)
    {
        var mapperTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>)))
            .ToList();

        foreach (Type? mapperType in mapperTypes)
        {
            Type mapperInterface = mapperType.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>));
            services.AddScoped(mapperInterface, mapperType);
        }

        return services;
    }
}
