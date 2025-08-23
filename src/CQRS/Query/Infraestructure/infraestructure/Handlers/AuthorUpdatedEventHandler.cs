using kafka.authors.application.Features.Authors.Commands.UpdateAuthor;
using kafka.authors.domain.Abstractions;
using library.core.domain.Events.Authors;
using MediatR;

namespace infraestructure.Handlers;
public class AuthorUpdatedEventHandler
(IMediator mediator)
: IEventHandler<AuthorUpdatedEvent>
{
  public async Task On(AuthorUpdatedEvent @event)
  {
    var updateAuthorCommand = new UpdateAuthorCommand
      (
        @event.Id,
        @event.AuthorFirstName,
        @event.AuthorLastName,
        @event.AuthorPhone.ToString()
      );

    await mediator.Send( updateAuthorCommand );
  }
}