namespace SharedKernel.Domain;

public interface IEntity : IEntity<Guid> { }

public interface IEntity<out T>
{
    T Id { get; }
}
