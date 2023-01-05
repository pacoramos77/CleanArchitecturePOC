using System.Reflection;

using FluentValidation;

using MediatR;

using SharedKernel.Behaviors;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreModule
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CoreModule).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
