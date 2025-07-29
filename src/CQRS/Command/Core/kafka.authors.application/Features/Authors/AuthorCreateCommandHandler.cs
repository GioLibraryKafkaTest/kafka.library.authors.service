using kafka.authors.application.Aggregates;
using kafka.authors.domain.Abstracts;
using MediatR;

namespace kafka.authors.application.Features.Authors;
public class AuthorCreateCommandHandler
(IEventSourcingHandler<AuthorAggregate> eventSourcingHandler)
: IRequestHandler<AuthorCreateCommand, bool>
{
  public async Task<bool> Handle(AuthorCreateCommand request, CancellationToken cancellationToken)
  {
    var aggregate = new AuthorAggregate(request); //dispatch event
    await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    return true;
  }
}