using Core.Domain.ToDoList;

namespace Core.Application.CreateToDoList;

public interface IToDoListRepository
{
    Task InsertAsync(ToDoList todoList);
}
