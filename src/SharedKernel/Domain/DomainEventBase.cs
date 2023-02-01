namespace SharedKernel.Domain;

public abstract class DomainEventBase : IDomainEvent
{
    public Guid Id { get; set; }
    public DateTime DateOccurred { get; protected set; } = DateTimeProvider.UtcNow;
}
