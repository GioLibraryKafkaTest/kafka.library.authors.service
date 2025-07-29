using kafka.authors.domain.Abstracts;

namespace kafka.authors.domain.EventModels;
public interface IEventModelRepository : IMongoRepository<EventModel>
{
}