using SharedKernel.Data;
using SharedKernel.Domain;

namespace Core.ToDoListAggregate;

public class ToDoItem : EntityBase, IEntity
{
    required public string Description { get; set; }
    public bool Done { get; set; }

    public ToDoItem() { }

    public ToDoItem(string description, bool done = false)
    {
        Id = GuidProvider.NewGuid();
        Description = description;
        Done = done;
    }
}
