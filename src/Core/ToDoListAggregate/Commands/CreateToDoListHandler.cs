using Mapster;

using SharedKernel.DataAccess;
using SharedKernel.Interfaces;
using SharedKernel.Messaging;

namespace Core.ToDoListAggregate.Commands;

public class CreateToDoListHandler : ICommandHandler<CreateToDoListRequest, CreateToDoListResponse>
{
    private readonly IRepository<ToDoList> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoListHandler(IRepository<ToDoList> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateToDoListResponse> Handle(
        CreateToDoListRequest request,
        CancellationToken cancellationToken
    )
    {
        ToDoList todoList = request.Adapt<ToDoList>();

        await _repository.AddAsync(todoList, cancellationToken).ConfigureAwait(false);

        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return todoList.Adapt<CreateToDoListResponse>();
    }
}
