using MediatR;

namespace kafka.authors.application.Features.Authors;
public record AuthorCreateCommand
(string id, AuthorCreateRequest request)
: IRequest<bool>;