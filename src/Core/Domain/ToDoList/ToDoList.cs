using Core.Kernel;

namespace Core.Domain.ToDoList;

public class ToDoList : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Item> Items { get => _items; }

    private IList<Item> _items;
   
    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public ToDoList(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        _items = new List<Item>();
    }

    private ToDoList()
    {
        Name = "";
        _items = new List<Item>();
    }

    public static ToDoList Load(Guid id, string name, IList<Item> items)
    {
        var todoList = new ToDoList();

        todoList.Id = id;
        todoList.Name = name;
        todoList._items = items;

        return todoList;
    }
}
