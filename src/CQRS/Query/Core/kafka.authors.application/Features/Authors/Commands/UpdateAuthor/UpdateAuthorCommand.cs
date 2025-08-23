using MediatR;

namespace kafka.authors.application.Features.Authors.Commands.UpdateAuthor;
public record UpdateAuthorCommand
(string Id, string AuthorFirstName, string AuthorLastName, string AuthorPhone)
: IRequest<string>;