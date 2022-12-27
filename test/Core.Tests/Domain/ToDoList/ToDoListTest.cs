using Core.Domain.ToDoList;

namespace Core.Tests.Domain.ToDoListTest;

public class ToDoListTest
{
    [Fact]
    public void Should_CreateNew_ToDoList()
    {
        var sut = new ToDoList("Name");

        sut.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Should_Add_Item()
    {
        // Given
        var sut = new ToDoList("my todo");

        // When
        sut.AddItem(new Item("new item"));

        // Then
        sut.Items.Count().Should().Be(1);
    }
}
