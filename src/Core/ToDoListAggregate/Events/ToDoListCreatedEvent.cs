using SharedKernel.Domain;

namespace Core.ToDoListAggregate.Events;

public class ToDoListCreatedEvent : DomainEventBase
{
    public ToDoListCreatedEvent() => Id = GuidProvider.NewGuid();

    required public string Name { get; init; }
}
