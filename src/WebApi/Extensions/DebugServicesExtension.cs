using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class DebugServicesExtension
{
    public static IServiceCollection DebugServices(this IServiceCollection services)
    {
        Console.WriteLine($"Total Services Registered: {services.Count}");
        foreach(var service in services)
        {
            Console.WriteLine(
                $$"""
                Service: {{service.ServiceType.FullName}}
                Lifetime: {{service.Lifetime}}
                Instance: {{service.ImplementationType?.FullName}}
                ===========================
                """);
        }

        return services;
    }
}
