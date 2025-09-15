using kafka.authors.application.Models;
using library.core.domain.Events;
using library.core.domain.Producers;
using Microsoft.Extensions.Options;

using Confluent.Kafka;
using Newtonsoft.Json;
using library.core.domain._Constants;

namespace infraestructure.Persistences;
public class AuthorEventProducer
: IEventProducer
{
  public AuthorEventProducer()
  {
  }
  public async Task ProducerAsync(string topic, BaseEvent @event)
  {
    var config = new ProducerConfig
    {
      BootstrapServers = $"{KafkaConnection.Kafka_HostName}: {KafkaConnection.Kafka_Port}"
    };

    using var producer = new ProducerBuilder<string, string>(config)
      .SetKeySerializer(Serializers.Utf8)
      .SetValueSerializer(Serializers.Utf8)
      .Build();

    var eventMessage = new Message<string, string>
    {
      Key = Guid.NewGuid().ToString(),
      Value = JsonConvert.SerializeObject(@event)
    };

    var deliveringStatus = await producer.ProduceAsync(topic, eventMessage);

    if (deliveringStatus.Status == PersistenceStatus.NotPersisted)
      throw new Exception($"message not sended of type {@event.GetType().Name} to the topic {topic} fot thid reazon: {deliveringStatus.Message}");
  }
}