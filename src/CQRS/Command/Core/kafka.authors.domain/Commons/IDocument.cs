using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace kafka.authors.domain.Commons;
public interface IDocument
{
  [BsonId]
  [BsonRepresentation(BsonType.String)]
  ObjectId Id { get; set; }
}