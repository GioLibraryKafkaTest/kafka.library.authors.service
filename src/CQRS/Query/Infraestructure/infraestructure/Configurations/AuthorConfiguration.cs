using kafka.authors.domain.Entities;
using kafka.authors.domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infraestructure.Configurations;
public class AuthorConfiguration
: IEntityTypeConfiguration<Author>
{
  public void Configure(EntityTypeBuilder<Author> builder)
  {
    builder.ToTable("authors");
    
    builder.HasKey(a => a.Id);

    builder.Property(a => a.AuthorPhoneNumber)
      .HasConversion
      (
        p => p.Value,
        value => AuthorPhoneNumber.Of(value)!
      );

    builder.ComplexProperty
    (
      x => x.AuthorName,
      an =>
      {
        an.Property(x => x.FirstName)
        .HasMaxLength(50)
        .IsRequired();

        an.Property(x => x.LastName)
        .HasMaxLength(50)
        .IsRequired();
      }
    );
  }
}