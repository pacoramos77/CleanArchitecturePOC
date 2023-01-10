using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreModule
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services.AddMediatorAndValidationPipeline(Assembly.GetExecutingAssembly());
    }
}
