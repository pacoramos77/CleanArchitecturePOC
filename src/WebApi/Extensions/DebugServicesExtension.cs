using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionDebugExtension
{
    public static IServiceCollection DebugServices(this IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            typeof(CoreModule).Assembly,
            typeof(InfrastructureModule).Assembly,
            typeof(SharedKernelModule).Assembly,
            typeof(ServiceCollectionDebugExtension).Assembly
        };
        Console.WriteLine($"Total Services Registered: {services.Count}");
        foreach (var service in services)
        {
            bool isTargetAssembly = assemblies.Any(
                assembly =>
                    assembly.FullName == service.ServiceType?.Assembly.FullName
                    || assembly.FullName == service.ImplementationType?.Assembly.FullName
            );
            if (!isTargetAssembly)
            {
                continue;
            }

            Console.WriteLine(
                $$"""
                    ===========================
                    {{service.ServiceType.Name}} -> {{service.ImplementationType?.Name}}
                    Lifetime: {{service.Lifetime}}
                    Service: {{service.ServiceType.Namespace}}.{{service.ServiceType.Name}}
                    Instance: {{service.ImplementationType?.Namespace}}.{{service.ImplementationType?.Name}}
                    """
            );
        }

        Console.WriteLine("===========================");

        return services;
    }
}
