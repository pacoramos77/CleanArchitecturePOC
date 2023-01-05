using SharedKernel.Domain;
using SharedKernel.Exceptions;

namespace Core.ToDoListAggregate.Exceptions;

[Serializable]
public class EmptyNameException : DomainException
{
    public EmptyNameException() : base("Name should not be empty.") { }
}
