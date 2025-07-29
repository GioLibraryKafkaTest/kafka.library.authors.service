using kafka.authors.application.Features.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace kafka.authors.api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
  private readonly IMediator mediator;

  public AuthorController
  (
    IMediator mediator
  )
  {
    this.mediator = mediator;
  }

  [HttpPost]
  public async Task<ActionResult> Post([FromBody] AuthorCreateRequest request)
  {
    string id = Guid.CreateVersion7(DateTimeOffset.UtcNow).ToString();
    AuthorCreateCommand command = new(id, request);
    return Ok(await mediator.Send(command));
  }
}