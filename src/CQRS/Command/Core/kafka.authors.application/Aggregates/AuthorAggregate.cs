using kafka.authors.application.Features.Authors;
using kafka.authors.domain.Abstracts;
using library.core.domain.Events.Authors;

namespace kafka.authors.application.Aggregates;
public class AuthorAggregate : AggregateRoot
{
  public bool Active { get; set; }
  public AuthorAggregate()
  {
    
  }

  public AuthorAggregate(AuthorCreateCommand command)
  {
    AuthorCreatedEvent authorCreatedEvent = new()
    {
      Id = command.id,
      AuthorFirstName = command.request.AuthorFirstName,
      AuthorLastName = command.request.AuthorLastName,
      AuthorPhone = command.request.AuthorPhone,
    };

    RaiseEvent(authorCreatedEvent);
  }

  public void Apply(AuthorCreatedEvent @event)
  {
    _id = @event.Id;
    Active = true;
  }

  public void EditAuthor(string authorFirstName, string authorLastName, int authorPhoneNumber)
  {
    if (!Active)
      throw new InvalidOperationException("No se puede editar un autor inactivo");

    RaiseEvent(new AuthorCreatedEvent
    {
      Id = Id,
      AuthorFirstName = authorFirstName,
      AuthorLastName = authorLastName,
      AuthorPhone = authorPhoneNumber
    });
  }

  //public void Apply()
}