using CleanArchitectureTemplate.SharedKernel;

using SharedKernel.Interfaces;

namespace Core.ToDoListAggregate;

public class ToDoItem : EntityBase, IEntity
{
    required public string Description { get; set; }
    public bool Done { get; }

    public ToDoItem() { }

    public ToDoItem(string description, bool done = false)
    {
        Id = Guid.NewGuid();
        Description = description;
        Done = done;
    }
}
