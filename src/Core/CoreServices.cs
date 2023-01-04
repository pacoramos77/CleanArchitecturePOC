using System.Reflection;

using Core.Common.Behaviors;

using FluentValidation;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class CoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CoreServices).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
