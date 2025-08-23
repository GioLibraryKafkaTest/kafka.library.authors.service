using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace kafka.authors.application;
public static class ApplicationServiceRegistration
{
  public static IServiceCollection RegisterApplicationService(this IServiceCollection services)
  {
    var currentAssembly = Assembly.GetExecutingAssembly();

    services.AddMediatR(x =>
    {
      x.RegisterServicesFromAssembly(currentAssembly);
    });

    return services;
  }
}