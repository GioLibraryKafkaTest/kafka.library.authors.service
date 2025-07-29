using library.core.domain.Events;

namespace kafka.authors.domain.Abstracts;
public interface IEventStore
{
  Task<List<BaseEvent>> GetEventAsync(string aggregateId, CancellationToken cancellationToken);
  Task SaveEventAsync(string aggregateId, IEnumerable<BaseEvent> baseEvents, int expectedVersion, CancellationToken cancellationToken);
}