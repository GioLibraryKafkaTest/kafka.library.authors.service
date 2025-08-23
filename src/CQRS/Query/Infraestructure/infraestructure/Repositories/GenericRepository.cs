using kafka.authors.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace infraestructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
  private readonly AuthorDbContext DbContext;

  public GenericRepository(AuthorDbContext authorDbContext)
  {
    this.DbContext = authorDbContext;
  }

  public void AddEntity(T Entity)
  {
    DbContext.Set<T>().Add(Entity);
  }

  public void DeleteEntity(T Entity)
  {
    DbContext.Set<T>().Remove(Entity);
  }

  public async Task<IReadOnlyList<T>> GetAllAsync()
  {
    return await DbContext.Set<T>().ToListAsync();
  }

  public async Task<T> GetByIdAsyn(Guid id)
  {
    return await DbContext.Set<T>().FindAsync(id);
  }

  public void UpdateEntity(T Entity)
  {
    DbContext.Set<T>().Attach(Entity);
    DbContext.Entry(Entity).State = EntityState.Modified;
  }
}