using kafka.authors.application.Models;
using kafka.authors.domain.EventModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace infraestructure.Repositories;
public class EventModelRepository
: MongoRepository<EventModel>, IEventModelRepository
{
  public EventModelRepository(IMongoClient mongoClient, IOptions<MongoSettings> options) : base(mongoClient, options)
  {
  }
}