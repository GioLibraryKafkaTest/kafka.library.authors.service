using kafka.authors.domain.Abstractions;
using kafka.authors.domain.ValueObjects;

namespace kafka.authors.domain.Entities;
public class Author : Entity
{
  public virtual AuthorName AuthorName { get; set; }
  public virtual AuthorPhoneNumber AuthorPhoneNumber { get; set; }

  private Author()
  {
    
  }

  private Author(Guid id, string authorFirstName, string authorLastName, string authorPhone)
  : base(id)
  {
    AuthorName = AuthorName.Of(authorFirstName, authorLastName);
    AuthorPhoneNumber = AuthorPhoneNumber.Of(authorPhone)!;
    CreatedBy = "Develop";
    CreatedOn = DateTime.UtcNow;
  }

  public static Author Create(Guid id, string authorFirstName, string authorLastName, string authorPhone)
    => new(id, authorFirstName, authorLastName, authorPhone);
}