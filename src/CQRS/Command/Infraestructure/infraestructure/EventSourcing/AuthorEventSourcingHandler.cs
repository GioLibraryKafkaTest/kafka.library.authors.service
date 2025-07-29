using kafka.authors.application.Aggregates;
using kafka.authors.domain.Abstracts;

namespace infraestructure.EventSourcing;
public class AuthorEventSourcingHandler
(IEventStore eventStore)
: IEventSourcingHandler<AuthorAggregate>
{
  public async Task<AuthorAggregate> GetByIdAsync(string aggregateId, CancellationToken cancellationToken)
  {
    var aggregate = new AuthorAggregate();
    var events = await eventStore.GetEventAsync(aggregateId, cancellationToken);

    if (events == null || !events.Any())
      return aggregate;

    aggregate.ReplayEvents(events);
    aggregate.Version = events.Select(x => x.Version).Max();

    return aggregate;
  }

  public async Task SaveAsync(AggregateRoot aggregateRoot, CancellationToken cancellationToken)
  {
    await eventStore.SaveEventAsync(aggregateRoot.Id, aggregateRoot.GetUncommitedChanges(), aggregateRoot.Version, cancellationToken);

    aggregateRoot.MarkChangesAsCommited();
  }
}