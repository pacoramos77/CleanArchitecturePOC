using Core.ToDoListAggregate.Commands;
using Core.ToDoListAggregate.Events;

using Mapster;

using SharedKernel.Data;
using SharedKernel.Domain;

namespace Core.ToDoListAggregate;

public class ToDoList : EntityBase, IAggregateRoot
{
    required public string Name { get; set; }

    public IEnumerable<ToDoItem> Items
    {
        get => _items;
        set => _items = value.ToList();
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

    public static ToDoList Create(CreateToDoListRequest request)
    {
        var toDoList = request.Adapt<ToDoList>();
        toDoList.RaiseDomainEvent(new ToDoListCreatedEvent { Name = toDoList.Name });
        return toDoList;
    }
}
