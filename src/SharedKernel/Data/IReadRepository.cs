using Ardalis.Specification;

using SharedKernel.Domain;

namespace SharedKernel.Data;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot { }
