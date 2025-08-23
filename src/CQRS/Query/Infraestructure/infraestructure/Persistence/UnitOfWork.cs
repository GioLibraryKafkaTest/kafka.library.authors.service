using infraestructure.Repositories;
using kafka.authors.domain.Abstractions;
using MySqlX.XDevAPI.Common;
using System.Collections;

namespace infraestructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
  private readonly DbContextFactory dbContextFactory;
  private readonly AuthorDbContext authorDbContext;
  private Hashtable repositories = new();

  public UnitOfWork(DbContextFactory dbContextFactory)
  {
    this.dbContextFactory = dbContextFactory;
    this.authorDbContext = this.dbContextFactory.CreateDbContext();
  }
  public async Task<int> Complete()
  {
    return await authorDbContext.SaveChangesAsync();
  }

  public IGenericRepository<TEntity> RepositoryGeneric<TEntity>() where TEntity : class
  {
    if(repositories is null)
      repositories = new Hashtable();

    var type = typeof(TEntity).Name;

    if(!repositories.Contains(type))
    {
      var repositoryType = typeof(GenericRepository<>);
      var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), authorDbContext);
      repositories.Add(type, repositoryInstance);
    }

    var result = (IGenericRepository<TEntity>)repositories[type]!;

    return result; //return one instance of the repository
  }
}