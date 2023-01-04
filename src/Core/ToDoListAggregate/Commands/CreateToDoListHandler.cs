using Core.ToDoListAggregate.Repositories;

using Mapster;

using SharedKernel.DataAccess;
using SharedKernel.Messaging;

namespace Core.ToDoListAggregate.Commands;

public class CreateToDoListHandler : ICommandHandler<CreateToDoListRequest, CreateToDoListResponse>
{
    private readonly ICreateToDoListRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoListHandler(ICreateToDoListRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateToDoListResponse> Handle(
        CreateToDoListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        ToDoList todoList = request.Adapt<ToDoList>();

        await _repository.InsertAsync(todoList, cancellationToken).ConfigureAwait(false);

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return todoList.Adapt<CreateToDoListResponse>();
    }
}
