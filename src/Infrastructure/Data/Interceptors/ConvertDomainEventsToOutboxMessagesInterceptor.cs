using Core.Util;

using Infrastructure.Data.Outbox;

using Microsoft.EntityFrameworkCore.Diagnostics;

using SharedKernel.Data;
using SharedKernel.Domain;

namespace Infrastructure.Data.Interceptors;

public class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var outboxMessages = dbContext.ChangeTracker
            .Entries<EntityBase>()
            .Select(x => x.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents().Select(x => x).ToArray();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(
                domainEvent =>
                {
                    var content = DomainEventsSerializer.Serialize(domainEvent);

                    return new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        OcurredOnUtc = DateTime.UtcNow,
                        Type = domainEvent.GetType().Name,
                        Content = content
                    };
                }
            )
            .ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
