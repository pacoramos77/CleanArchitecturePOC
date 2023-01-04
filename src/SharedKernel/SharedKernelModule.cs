using SharedKernel.Domain;

namespace Microsoft.Extensions.DependencyInjection;

public static class SharedKernelModule
{
    public static IServiceCollection AddSharedKernelServices(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }
}
