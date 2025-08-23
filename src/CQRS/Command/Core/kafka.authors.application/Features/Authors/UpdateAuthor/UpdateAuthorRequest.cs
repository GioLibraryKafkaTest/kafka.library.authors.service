namespace kafka.authors.application.Features.Authors.UpdateAuthor;
public class UpdateAuthorRequest
{
  public Guid AuthorId { get; set; }
  public string AuthorFirstName { get; set; }
  public string AuthorLastName { get; set; }
  public int AuthorPhone { get; set; }
}