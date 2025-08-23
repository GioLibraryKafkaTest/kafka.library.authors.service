namespace kafka.authors.domain.Abstractions;
public interface IGenericRepository<T> where T : class
{
  Task<IReadOnlyList<T>> GetAllAsync();
  Task<T> GetByIdAsyn(Guid id);
  void AddEntity(T Entity);
  void UpdateEntity(T Entity);
  void DeleteEntity(T Entity);
}