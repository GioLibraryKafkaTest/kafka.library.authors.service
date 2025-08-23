using library.core.domain.Events;

namespace kafka.authors.domain.Abstractions;
public interface IEventHandler<TEvent> where TEvent : class
{
  Task On(TEvent @event);
}