using kafka.authors.application.Aggregates;
using kafka.authors.domain.Abstracts;
using MediatR;

namespace kafka.authors.application.Features.Authors.UpdateAuthor;
public sealed class UpdateAuthorCommandHandler
(IEventSourcingHandler<AuthorAggregate> eventSourcingHandler)
: IRequestHandler<UpdateAuthorCommand, bool>
{
  public async Task<bool> Handle
  (
    UpdateAuthorCommand command,
    CancellationToken cancellationToken
  )
  {
    var aggregate = await eventSourcingHandler.GetByIdAsync(command.Request.AuthorId.ToString(), cancellationToken);
    aggregate.EditAuthor(command.Request.AuthorFirstName, command.Request.AuthorLastName, command.Request.AuthorPhone);
    await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    return true;
  }
}