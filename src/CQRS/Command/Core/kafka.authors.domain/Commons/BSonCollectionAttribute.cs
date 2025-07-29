namespace kafka.authors.domain.Commons;
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BSonCollectionAttribute : Attribute
{
  public string CollectionName;
  public BSonCollectionAttribute(string collectionName)
  {
    this.CollectionName = collectionName;    
  }
}