using Ardalis.Specification.EntityFrameworkCore;

using SharedKernel.Data;
using SharedKernel.Domain;

namespace Infrastructure.Data;

public class GenericRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public GenericRepository(AppDbContext dbContext) : base(dbContext) { }
}
