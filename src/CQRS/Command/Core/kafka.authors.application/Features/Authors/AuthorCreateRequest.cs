namespace kafka.authors.application.Features.Authors;
public sealed class AuthorCreateRequest
{
  public string AuthorFirstName { get; set; }
  public string AuthorLastName { get; set; }
  public int AuthorPhone { get; set; }
}