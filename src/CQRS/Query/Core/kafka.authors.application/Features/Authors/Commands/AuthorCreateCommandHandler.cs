using kafka.authors.domain.Abstractions;
using kafka.authors.domain.Entities;
using MediatR;

namespace kafka.authors.application.Features.Authors.Commands;
public class AuthorCreateCommandHandler
(IUnitOfWork unitOfWork)
: IRequestHandler<AuthorCreateCommand, string>
{
  public async Task<string> Handle
  (
    AuthorCreateCommand command,
    CancellationToken cancellationToken
  )
  {
    try
    {
      Author authorToCreate = Author.Create(new Guid(command.Id), command.AuthorFirstName, command.AuthorLastName, command.AuthorPhone);
      unitOfWork
        .RepositoryGeneric<Author>()
        .AddEntity
        (
        authorToCreate
        );

      //insert in database
      await unitOfWork.Complete();

      return authorToCreate.Id.ToString();
    }
    catch( Exception ex )
    {
      throw new Exception($"{ex.Message}");
    }
  }
}