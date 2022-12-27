using Core.Domain.ToDoList;

namespace Core.Application.CreateToDoList;

public interface ICreateToDoListRepository
{
    Task InsertAsync(ToDoList todoList);
}
