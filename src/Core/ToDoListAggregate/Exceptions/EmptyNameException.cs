using SharedKernel.Domain;

namespace Core.Application.CreateToDoList;

[Serializable]
public class EmptyNameException : DomainException
{
    public EmptyNameException()
        : base("Name should not be empty.")
    {
    }
}
