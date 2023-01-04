using CleanArchitectureTemplate.SharedKernel;

using SharedKernel.Interfaces;

namespace Core.ToDoListAggregate;

public class ToDoList : EntityBase, IAggregateRoot
{
    public required string Name { get; set; }
    public IEnumerable<ToDoItem> Items
    {
        get => _items;
    }

    private IList<ToDoItem> _items;

    public void AddItem(ToDoItem item)
    {
        _items.Add(item);
    }

    public ToDoList()
    {
        Id = Guid.NewGuid();
        _items = new List<ToDoItem>();
    }

    public static ToDoList Load(Guid id, string name, IList<ToDoItem> items)
    {
        var todoList = new ToDoList() { Id = id, Name = name };

        todoList._items = items;

        return todoList;
    }
}
