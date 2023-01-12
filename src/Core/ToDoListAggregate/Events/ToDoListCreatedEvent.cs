using SharedKernel.Domain;

namespace Core.ToDoListAggregate.Events;

public class ToDoListCreatedEvent : DomainEventBase
{
    required public string Name { get; init; }
}
