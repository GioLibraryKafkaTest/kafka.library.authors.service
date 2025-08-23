using kafka.authors.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infraestructure;
public class AuthorDbContext : DbContext
{
  public AuthorDbContext(DbContextOptions<AuthorDbContext> options) : base(options)
  {
    
  }

  public virtual DbSet<Author> Authors => Set<Author>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}