using MediatR;

namespace SharedKernel.Domain;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTimeProvider.UtcNow;
}
