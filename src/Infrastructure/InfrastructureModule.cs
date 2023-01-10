using Core.ToDoListAggregate;

using Infrastructure.Data;

using Infrastructure.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using SharedKernel.Data;
using SharedKernel.Domain;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfrastructureModule
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

        services
            .AddEventDispatcher<DomainEventDispatcher>()
            .AddScoped<IRepository<ToDoList>, GenericRepository<ToDoList>>()
            .AddScoped<IUnitOfWork, AppDbContext>();

        return services;
    }
}
