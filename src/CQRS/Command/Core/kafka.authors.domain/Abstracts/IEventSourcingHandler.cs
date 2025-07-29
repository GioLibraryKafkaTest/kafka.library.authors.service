namespace kafka.authors.domain.Abstracts;
public interface IEventSourcingHandler<T>
{
  Task<T> GetByIdAsync(string aggregateId, CancellationToken cancellationToken);
  Task SaveAsync(AggregateRoot aggregateRoot, CancellationToken cancellationToken);
}