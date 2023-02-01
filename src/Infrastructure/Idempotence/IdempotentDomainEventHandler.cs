using Infrastructure.Data;
using Infrastructure.Data.Outbox;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SharedKernel.Domain;

namespace Infrastructure.Idempotence;

public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent

{
    private readonly INotificationHandler<TDomainEvent> _decorated;
    private readonly AppDbContext _dbContext;

    public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, AppDbContext dbContext)
        => (_decorated, _dbContext) = (decorated, dbContext);

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;
        bool alreadyHandled = await _dbContext.Set<OutboxMessageConsumer>()
                    .AnyAsync(
                        outboxMessageConsumer =>
                            outboxMessageConsumer.Id == notification.Id &&
                            outboxMessageConsumer.Name == consumer,
                        cancellationToken
                    );

        if (alreadyHandled)
        {
            return;
        }

        await _decorated.Handle(notification, cancellationToken);

        _dbContext.Set<OutboxMessageConsumer>()
            .Add(new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
