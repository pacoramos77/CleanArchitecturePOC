using Core.Domain.ToDoList;
using SharedKernel.DataAccess;
using SharedKernel.Messaging;

namespace Core.Application.CreateToDoList;

public class CreateToDoListHandler : ICommandHandler<CreateToDoListRequest, AddToDoResponse>
{
    private readonly ICreateToDoListRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoListHandler(ICreateToDoListRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AddToDoResponse> Handle(
        CreateToDoListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new EmptyNameException();
        }
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
