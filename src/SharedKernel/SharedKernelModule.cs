using SharedKernel.Domain;

namespace Microsoft.Extensions.DependencyInjection;

public static class SharedKernelModule
{
    public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services) =>
        services.AddEventDispatcher<DomainEventDispatcher>();

    public static IServiceCollection AddEventDispatcher<TEventDispatcher>(
        this IServiceCollection services
    ) where TEventDispatcher : class, IDomainEventDispatcher =>
        services.AddScoped<IDomainEventDispatcher, TEventDispatcher>();
}
