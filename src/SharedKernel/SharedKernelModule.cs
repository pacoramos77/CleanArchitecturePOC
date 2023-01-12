using System.Reflection;
using FluentValidation;
using MediatR;
using SharedKernel.Behaviors;
using SharedKernel.Domain;

namespace Microsoft.Extensions.DependencyInjection;

public static class SharedKernelModule
{
    public static IServiceCollection AddMediatorAndValidationPipeline(this IServiceCollection services, Assembly domainAssembly)
    {
        services.AddMediatR(domainAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(domainAssembly);

        return services;
    }
}
