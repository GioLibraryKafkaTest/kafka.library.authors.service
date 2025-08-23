using Confluent.Kafka;
using infraestructure.Converters;
using kafka.authors.domain.Abstractions;
using library.core.domain.Consumers;
using library.core.domain.Events;
using library.core.domain.Events.Authors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace infraestructure.Consumers;
public class EventConsumer : IEventConsumer
{
  private readonly ConsumerConfig config;
  private readonly IServiceScopeFactory serviceProvider;

  public EventConsumer
  (
    IOptions<ConsumerConfig> config,
    IServiceScopeFactory serviceProvider
  )
  {
    this.config = config.Value;
    this.serviceProvider = serviceProvider;
  }

  public void Consume(string topic)
  {
    using var consumer = new ConsumerBuilder<string, string>(config)
      .SetKeyDeserializer(Deserializers.Utf8)
      .SetValueDeserializer(Deserializers.Utf8)
      .Build();

    consumer.Subscribe(topic);

    while (true)
    {
      var consumerResult = consumer.Consume();

      if (consumerResult is null)
        continue;

      if(consumerResult.Message is null)
        continue;

      var options = new JsonSerializerOptions
      {
        Converters = { new EventJsonConverter() }
      };

      var @event = JsonSerializer
        .Deserialize<BaseEvent>(consumerResult.Message.Value, options);

      if (@event is null)
        throw new ArgumentNullException("Message couldn't be processing");

      var eventType = @event.GetType();

      var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

      using var scope = serviceProvider.CreateScope();

      var handler = scope.ServiceProvider.GetRequiredService(handlerType);

      var method = handlerType.GetMethod("On");

      method.Invoke(handler, new object[] { @event });

      //indicates event consumed to remove from kafka
      consumer.Commit(consumerResult);
    }
  }
}