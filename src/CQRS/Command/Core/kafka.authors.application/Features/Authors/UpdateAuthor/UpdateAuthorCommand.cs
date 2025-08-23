using MediatR;

namespace kafka.authors.application.Features.Authors.UpdateAuthor;
public record UpdateAuthorCommand
(UpdateAuthorRequest Request)
: IRequest<bool>;