using kafka.authors.domain.Commons;
using library.core.domain.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace kafka.authors.domain.EventModels;
[BSonCollection("eventStore")]
public class EventModel : Document
{
  [BsonElement("timestamp")]
  public DateTime Timestamp { get; set; }

  [BsonElement("aggregateIdentifier")]
  public required string AggregateIdentifier { get; set; }

  [BsonElement("aggregateType")]
  public string AggregateType { get; set; }

  [BsonElement("version")]
  public int Version { get; set; }

  [BsonElement("eventType")]
  public string EventType { get; set; }

  [BsonElement("eventData")]
  public BaseEvent? EventData { get; set; }
}