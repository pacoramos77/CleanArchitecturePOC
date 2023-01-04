using CleanArchitectureTemplate.Infrastructure.Data;

using Infrastructure.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SharedKernel.DataAccess;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddDbContext<AppDbContext>(
            (serviceProvier, options) =>
            {
                var databaseOptions = serviceProvier.GetService<IOptions<DatabaseOptions>>()!.Value;

                options.UseSqlServer(
                    databaseOptions.ConnectionString,
                    sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    }
                );

                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            }
        );

        return services.Scan(
            selector =>
                selector
                    .FromAssemblies(typeof(InfrastructureServices).Assembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo<IUnitOfWork>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
        );
    }
}
