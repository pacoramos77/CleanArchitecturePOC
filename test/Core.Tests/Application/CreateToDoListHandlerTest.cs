using Core.ToDoListAggregate;
using Core.ToDoListAggregate.Commands;
using Core.ToDoListAggregate.Exceptions;

using SharedKernel.Data;
using SharedKernel.GuidContext;

namespace Core.Tests.Application;

public class CreateToDoListHandlerTest
{
    private readonly Mock<IRepository<ToDoList>> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Handle_Should_CreateToDoList()
    {
        // Arrange
        CreateToDoListRequest command = new(Name: "new name");
        CreateToDoListHandler handler = new(_repository.Object, _unitOfWork.Object);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be("new name");
    }

    [Fact]
    public async Task Handle_Should_Fail_WhenNotHaveName()
    {
        // Arrange
        CreateToDoListRequest command = new(Name: string.Empty);
        CreateToDoListHandler handler = new(_repository.Object, _unitOfWork.Object);

        // Act / Assert
        await Assert.ThrowsAsync<EmptyNameException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}
