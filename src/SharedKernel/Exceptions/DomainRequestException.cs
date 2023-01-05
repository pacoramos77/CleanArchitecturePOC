namespace SharedKernel.Exceptions;

public abstract class DomainException : ApplicationException
{
    protected DomainException(string message)
        : base("Domain Exception", message)
    {
    }
}