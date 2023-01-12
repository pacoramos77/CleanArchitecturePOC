using Core.ToDoListAggregate;
using Core.ToDoListAggregate.Commands;
using Core.ToDoListAggregate.Exceptions;

using SharedKernel.Data;

namespace Core.Tests.Application;

public class CreateToDoListHandlerTest
{
    private readonly Mock<IRepository<ToDoList>> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Should_Handle_CreateToDoList()
    {
        CreateToDoListHandler handler = new(_repository.Object, _unitOfWork.Object);
        CreateToDoListRequest request = new(Name: "new name");

        var response = await handler.Handle(request, CancellationToken.None);

        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be("new name");
    }
}
