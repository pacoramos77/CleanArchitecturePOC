using Core.ToDoListAggregate.Commands;
using Core.ToDoListAggregate.Exceptions;
using Core.ToDoListAggregate.Repositories;

using SharedKernel.DataAccess;

namespace Core.Tests.Application;

public class CreateToDoListHandlerTest
{
    private readonly Mock<ICreateToDoListRepository> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Should_Handle_CreateToDoList()
    {
        CreateToDoListHandler handler = new(_repository.Object, _unitOfWork.Object);
        CreateToDoListRequest request = new(Name: "new name");

        var response = await handler.Handle(request);

        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be("new name");
    }

    [Fact]
    public async Task Should_Fail_WithEmptyName()
    {
        CreateToDoListHandler handler = new(_repository.Object, _unitOfWork.Object);
        CreateToDoListRequest request = new(Name: "");

        await Assert.ThrowsAsync<EmptyNameException>(() => handler.Handle(request));
    }
}
