using Core.Kernel;

namespace Core.Domain.ToDoList;

public class Item : IEntity
{
    public Guid Id { get; }
    public string Description { get; } = string.Empty;
    public bool Done { get; }

    public Item(string description, bool done = false)
    {
        Id = Guid.NewGuid();
        Description = description;
        Done = done;
    }
}
