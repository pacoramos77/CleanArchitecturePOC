using Ardalis.Specification;

using SharedKernel.Domain;

namespace SharedKernel.Data;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot { }
