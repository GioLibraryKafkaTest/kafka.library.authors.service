using kafka.authors.domain.Commons;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace kafka.authors.domain.Abstracts;
public interface IMongoRepository<TDocument>
: ISession where TDocument : IDocument
{
  IQueryable<TDocument> AsQueriable();
  Task InsertOneAsync(TDocument document, IClientSessionHandle clientSessionHandle, CancellationToken cancellationToken);
  Task<IEnumerable<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> expressionFilter, CancellationToken cancellationToken);
}