using library.core.domain.Events;

namespace kafka.authors.domain.Abstracts;
public abstract class AggregateRoot
{
  protected string _id = string.Empty;
  public string Id
  {
    get { return _id; }
  }
  public int Version;
  private readonly List<BaseEvent> _changes = new();
  public IEnumerable<BaseEvent> GetUncommitedChanges()
  {
    return _changes;
  }

  public void MarkChangesAsCommited()
  {
    _changes.Clear();
  }

  public void RaiseEvent(BaseEvent @event)
  {
    ApplyChange(@event, true);
  }

  public void ApplyChange(BaseEvent @event, bool isNewEvent)
  {
    var method = GetType().GetMethod("Apply", [@event.GetType()]);

    if (method == null)
      throw new ArgumentException(nameof(method), $"El método Apply no fue encontrado en {@event.GetType().Name}");

    method.Invoke(this, [@event]);

    if(isNewEvent)
      _changes.Add(@event);
  }

  public void ReplayEvents(IEnumerable<BaseEvent> events)
  {
    foreach (var @event in events)
    {
      ApplyChange(@event, false);
    }
  }
}