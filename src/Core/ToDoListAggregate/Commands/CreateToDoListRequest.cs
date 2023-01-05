using SharedKernel.Messaging;

namespace Core.ToDoListAggregate.Commands;

public record CreateToDoListRequest(string Name) : ICommand<CreateToDoListResponse>;
