using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Application.CreateToDoList;
using Core.Kernel.Abstractions.Store;

namespace Core.Tests.Application;

public class CreateToDoListHandlerTest
{
    private readonly Mock<IToDoListRepository> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Should_Handle_CreateToDoList()
    {
        var handler = new CreateToDoListHandler(_repository.Object, _unitOfWork.Object);
        CreateToDoListRequest request = new CreateToDoListRequest { Name = "new name" };

        var response = await handler.Handle(request);

        response.Id.Should().NotBeEmpty();
        response.Name.Should().Be("new name");
    }
}
