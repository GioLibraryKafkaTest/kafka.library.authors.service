using kafka.authors.application.Features.Authors.Commands;
using kafka.authors.domain.Abstractions;
using library.core.domain.Events.Authors;
using MediatR;

namespace infraestructure.Handlers;
public class AuthorCreatedEventHandler
(IMediator mediator)
: IEventHandler<AuthorCreatedEvent>
{
  public async Task On(AuthorCreatedEvent @event)
  {
    AuthorCreateCommand command = new
    (
      @event.Id,
      @event.AuthorFirstName,
      @event.AuthorLastName,
      @event.AuthorPhone.ToString()
    );

    await mediator.Send(command);
  }
}