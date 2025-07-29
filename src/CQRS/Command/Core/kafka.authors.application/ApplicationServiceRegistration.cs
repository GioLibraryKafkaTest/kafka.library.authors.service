using kafka.authors.application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace kafka.authors.application;
public static class ApplicationServiceRegistration
{
  public static IServiceCollection AddApplicationService
  (
    this IServiceCollection service,
    IConfiguration configuration
  )
  {
    service.Configure<MongoSettings>(configuration.GetSection(nameof(MongoSettings)));
    
    service.Configure<KafkaSettings>(configuration.GetSection(nameof(KafkaSettings)));

    service.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
    });

    return service;
  }
}