using Microsoft.EntityFrameworkCore;

namespace infraestructure.Persistence;
public class DbContextFactory
{
  private readonly Action<DbContextOptionsBuilder> configureDbContext;

  public DbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
  {
    this.configureDbContext = configureDbContext;
  }

  public AuthorDbContext CreateDbContext()
  {
    DbContextOptionsBuilder<AuthorDbContext> optionsBuilder = new();
    configureDbContext(optionsBuilder);
    return new AuthorDbContext(optionsBuilder.Options);
  }
}