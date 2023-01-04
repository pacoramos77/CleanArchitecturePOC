using SharedKernel.Messaging;

namespace Core.ToDoListAggregate.Commands;

public record CreateToDoListRequest(string Name) : ICommand<CreateToDoListResponse>
{
    public static explicit operator ToDoList(CreateToDoListRequest request)
    {
        return new ToDoList { Name = request.Name };
    }
}
