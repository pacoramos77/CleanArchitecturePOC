using Ardalis.Specification.EntityFrameworkCore;

using SharedKernel.Interfaces;

namespace Infrastructure.Data;

public class GenericRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>, IRepo<T>
    where T : class, IAggregateRoot
{
    public GenericRepository(AppDbContext dbContext) : base(dbContext) { }
}
