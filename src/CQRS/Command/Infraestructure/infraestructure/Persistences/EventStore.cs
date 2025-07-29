using kafka.authors.application.Models;
using kafka.authors.domain.Abstracts;
using kafka.authors.domain.EventModels;
using library.core.domain.Events;
using library.core.domain.Producers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace infraestructure.Persistences;
public class EventStore
: IEventStore
{
  private readonly IEventModelRepository eventModelRepository;
  private readonly KafkaSettings kafkaSettings;
  private readonly IEventProducer eventProducer;

  public EventStore
  (
    IEventModelRepository eventModelRepository,
    IOptions<KafkaSettings> settings,
    IEventProducer eventProducer
  )
  {
    this.eventModelRepository = eventModelRepository;
    this.kafkaSettings = settings.Value;
    this.eventProducer = eventProducer;
  }
  public async Task<List<BaseEvent>> GetEventAsync(string aggregateId, CancellationToken cancellationToken)
  {
    var eventStream = await eventModelRepository
      .FilterByAsync(x => x.AggregateIdentifier == aggregateId, cancellationToken);

    if (eventStream == null || !eventStream.Any())
      throw new Exception("Aggregate have not events");

    return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList()!;
  }

  public async Task SaveEventAsync(string aggregateId, IEnumerable<BaseEvent> baseEvents, int expectedVersion, CancellationToken cancellationToken)
  {
    var eventStream = await eventModelRepository
      .FilterByAsync(x => x.AggregateIdentifier == aggregateId, cancellationToken);

    if (eventStream.Any() && expectedVersion != 1 && eventStream.Last().Version != expectedVersion)
      throw new Exception("Concurrency error");

    var version = expectedVersion;

    foreach (var @event in baseEvents)
    {
      version++;
      @event.Version = version;
      var evenType = @event.GetType().Name;
      var eventModel = new EventModel
      {
        Timestamp = DateTime.UtcNow,
        AggregateIdentifier = aggregateId,
        Version = version,
        EventType = evenType,
        EventData = @event
      };

      await AddEventStore(eventModel, cancellationToken);

      var topic = kafkaSettings.Topic ?? throw new Exception("KafkaSettings topic not defined");

      await eventProducer.ProducerAsync(topic, @event);
    }
  }

  private async Task AddEventStore(EventModel @event, CancellationToken cancellationToken)
  {
    IClientSessionHandle session = await eventModelRepository.BeginSessionAsync(cancellationToken);

    try
    {
      eventModelRepository.BeginTransaction(session);
      await eventModelRepository.InsertOneAsync(@event, session, cancellationToken);
      await eventModelRepository.CommitTransactionAsync(session, cancellationToken);
      eventModelRepository.DisposeSession(session);
    }catch (Exception ex)
    {
      await eventModelRepository.RollbackTransactionAsync(session, cancellationToken);
      eventModelRepository.DisposeSession(session);
    }
  }
}