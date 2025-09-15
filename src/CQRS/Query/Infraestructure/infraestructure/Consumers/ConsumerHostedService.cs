
using Confluent.Kafka;
using library.core.domain._Constants;
using library.core.domain.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace infraestructure.Consumers;
public class ConsumerHostedService
: IHostedService
{
  private readonly ConsumerConfig config;
  private readonly ILogger<ConsumerHostedService> logger;
  private readonly IServiceProvider serviceProvider;

  public ConsumerHostedService
  (
    IOptions<ConsumerConfig> config,
    ILogger<ConsumerHostedService> logger,
    IServiceProvider serviceProvider
  )
  {
    this.config = config.Value;
    this.logger = logger;
    this.serviceProvider = serviceProvider;
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    logger.LogInformation("Event consumer is working");
    var topic = KafkaConnection.Author_Topic;

    using(IServiceScope scope = serviceProvider.CreateAsyncScope())
    {
      var eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

      //Create new thread to prevent api blocks
      Task.Run(() => eventConsumer.Consume(topic), cancellationToken);
    }

    return Task.CompletedTask;
  }

  public Task StopAsync(CancellationToken cancellationToken)
  {
    logger.LogInformation($"Service consumes stopped");
    return Task.CompletedTask;
  }
}