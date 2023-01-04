namespace Core.ToDoListAggregate.Commands;

public record CreateToDoListResponse(string Id, string Name)
{
    public static explicit operator CreateToDoListResponse(ToDoList toDoList)
    {
        return new CreateToDoListResponse(Id: toDoList.Id.ToString(), Name: toDoList.Name);
    }
}
