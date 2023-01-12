using System.ComponentModel.DataAnnotations.Schema;

using SharedKernel.Domain;

namespace SharedKernel.Data;

public abstract class EntityBase : EntityBase<Guid> { }

public abstract class EntityBase<TId>
{
    public TId? Id { get; set; }

    private readonly List<DomainEventBase> _domainEvents = new();

    public IReadOnlyCollection<DomainEventBase> GetDomainEvents() => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(DomainEventBase domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
