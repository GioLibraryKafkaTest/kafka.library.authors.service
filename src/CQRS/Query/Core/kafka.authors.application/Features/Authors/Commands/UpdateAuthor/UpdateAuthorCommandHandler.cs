using kafka.authors.domain.Abstractions;
using kafka.authors.domain.Entities;
using kafka.authors.domain.ValueObjects;
using MediatR;

namespace kafka.authors.application.Features.Authors.Commands.UpdateAuthor;
public class UpdateAuthorCommandHandler
(IUnitOfWork unitOfWork)
: IRequestHandler<UpdateAuthorCommand, string>
{
  public async Task<string> Handle
  (
    UpdateAuthorCommand command,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var author = await unitOfWork.RepositoryGeneric<Author>().GetByIdAsyn(new Guid(command.Id));
      if (author == null)
        throw new Exception("Author not found");

      author.AuthorName = AuthorName.Of(command.AuthorFirstName, command.AuthorLastName);
      author.AuthorPhoneNumber = AuthorPhoneNumber.Of(command.AuthorPhone);

      await unitOfWork.Complete();

      return author.Id.ToString();
    }
    catch ( Exception ex )
    {
      throw new Exception($"{ex.Message}");
    }
  }
}