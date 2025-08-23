using MediatR;

namespace kafka.authors.application.Features.Authors.Commands;
public record AuthorCreateCommand
(string Id, string AuthorFirstName, string AuthorLastName, string AuthorPhone)
: IRequest<string>;