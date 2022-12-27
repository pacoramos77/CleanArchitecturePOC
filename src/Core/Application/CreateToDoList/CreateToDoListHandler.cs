using Application.Abstractions.Messaging;

using Core.Domain.ToDoList;
using Core.Kernel.Abstractions.Store;

namespace Core.Application.CreateToDoList;

public class CreateToDoListHandler : ICommandHandler<CreateToDoListRequest, AddToDoResponse>
{
    private readonly IToDoListRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoListHandler(IToDoListRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddToDoResponse> Handle(
        CreateToDoListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var todoList = new ToDoList(request.Name);
        await _repository.InsertAsync(todoList);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AddToDoResponse { Id = todoList.Id.ToString(), Name = todoList.Name };
    }
}

public class CreateToDoListRequest : ICommand<AddToDoResponse>
{
    public string Name { get; set; } = string.Empty;
}

public class AddToDoResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
