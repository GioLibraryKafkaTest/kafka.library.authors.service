using Confluent.Kafka;
using infraestructure.Consumers;
using infraestructure.Handlers;
using infraestructure.Persistence;
using infraestructure.Repositories;
using kafka.authors.domain.Abstractions;
using library.core.domain.Consumers;
using library.core.domain.Events.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infraestructure;
public static class InfraestructureServiceRegistration
{
  public static IServiceCollection RegisterInfraestructureServices
  (
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    Action<DbContextOptionsBuilder> configureDbContext;

    string connectionString = configuration.GetConnectionString("AuthorDbConnectionString")
      ?? throw new ArgumentNullException(nameof(configuration));

    configureDbContext = o => o
    .UseLazyLoadingProxies(false)
    .UseMySQL(connectionString)
    .UseSnakeCaseNamingConvention();

    services.AddDbContext<AuthorDbContext>(
      opt =>
      opt.UseMySQL(connectionString).UseSnakeCaseNamingConvention()
    );

    services.AddSingleton<DbContextFactory>(new DbContextFactory(configureDbContext));
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    services.AddScoped<IEventHandler<AuthorCreatedEvent>, AuthorCreatedEventHandler>();
    services.AddScoped<IEventHandler<AuthorUpdatedEvent>, AuthorUpdatedEventHandler>();

    services.AddScoped<IEventConsumer, EventConsumer>();

    services.AddHostedService<ConsumerHostedService>();

    services.Configure<ConsumerConfig>(configuration.GetSection(nameof(ConsumerConfig)));

    return services;
  }
}