namespace Core.ToDoListAggregate.Repositories;

public interface ICreateToDoListRepository
{
    Task InsertAsync(ToDoList todoList, CancellationToken cancellationToken = default);
}
