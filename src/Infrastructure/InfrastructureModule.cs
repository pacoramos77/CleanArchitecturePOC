using Core.ToDoListAggregate;

using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
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

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<AppDbContext>(
            (serviceProvier, options) =>
            {
                var databaseOptions = serviceProvier
                    .GetRequiredService<IOptions<DatabaseOptions>>()
                    .Value;
                var interceptor =
                    serviceProvier.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                options
                    .UseSqlServer(
                        databaseOptions.ConnectionString,
                        sqlServerAction =>
                        {
                            sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                            sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                        }
                    )
                    .AddInterceptors(interceptor);

                options.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            }
        );

        services
            .AddScoped<IRepository<ToDoList>, GenericRepository<ToDoList>>()
            .AddScoped<IUnitOfWork, AppDbContext>();

        return services;
    }
}
