using SharedKernel.Data;

namespace SharedKernel.Domain;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}
