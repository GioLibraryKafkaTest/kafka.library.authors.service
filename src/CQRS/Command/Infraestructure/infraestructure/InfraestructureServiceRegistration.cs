using infraestructure.EventSourcing;
using infraestructure.Persistences;
using infraestructure.Repositories;
using kafka.authors.application.Aggregates;
using kafka.authors.domain.Abstracts;
using kafka.authors.domain.EventModels;
using library.core.domain.Events;
using library.core.domain.Events.Authors;
using library.core.domain.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace infraestructure;
public static class InfraestructureServiceRegistration
{
  public static IServiceCollection AddInfraestructureService
  (
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    BsonClassMap.RegisterClassMap<BaseEvent>();
    BsonClassMap.RegisterClassMap<AuthorCreatedEvent>();

    services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    services.AddTransient<IEventModelRepository, EventModelRepository>();
    services.AddSingleton<IMongoClient, MongoClient>
      (sp => new MongoClient(configuration.GetConnectionString("MongoDb")));
    services.AddTransient<IEventStore, EventStore>();
    services.AddTransient<IEventSourcingHandler<AuthorAggregate>, AuthorEventSourcingHandler>();
    services.AddScoped<IEventProducer, AuthorEventProducer>();

    return services;
  }
}