using CleanArchitectureTemplate.SharedKernel;

using SharedKernel.Interfaces;

namespace Core.ToDoListAggregate;

public class ToDoItem : EntityBase, IEntity
{
    public string Description { get; }
    public bool Done { get; }

    public ToDoItem(string description, bool done = false)
    {
        Id = Guid.NewGuid();
        Description = description;
        Done = done;
    }
}
