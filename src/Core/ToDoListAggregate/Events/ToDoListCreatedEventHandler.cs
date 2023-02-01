using System.Globalization;

using SharedKernel.Domain;

namespace Core.ToDoListAggregate.Events;

public class ToDoListCreatedEventHandler : IDomainEventHandler<ToDoListCreatedEvent>
{
    public Task Handle(ToDoListCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"------ ToDoListCreatedEventHandler ----- {notification.Name}, {notification.DateOccurred.ToLongTimeString()} ------");
        return Task.CompletedTask;
    }
}
