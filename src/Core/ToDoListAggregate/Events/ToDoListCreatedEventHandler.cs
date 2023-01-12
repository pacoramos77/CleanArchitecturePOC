using SharedKernel.Domain;

namespace Core.ToDoListAggregate.Events;

public class ToDoListCreatedEventHandler : IDomainEventHandler<ToDoListCreatedEvent>
{
    public Task Handle(ToDoListCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("ToDoListCreatedEventHandler");
        return Task.CompletedTask;
    }
}
