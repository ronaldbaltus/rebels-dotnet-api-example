namespace ExampleProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using Rebels.ExampleProject.CQRS.V1.Queries;
using Rebels.ExampleProject.CQRS.V1.Commands;
using MediatR;
using Rebels.ExampleProject.Dtos.V1;

[ApiController]
[Route("v1/[controller]")]
public class RebelsController : ControllerBase
{
    private readonly ILogger<RebelsController> _logger;
    private readonly IMediator _mediator;

    public RebelsController(ILogger<RebelsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = nameof(GetRebelsList))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RebelDto>))]
    public async Task<IActionResult> GetRebelsList(
        [FromQuery] GetRebelsQuery query,
        CancellationToken cancellationToken
    )
    => Ok(await _mediator.Send(query, cancellationToken));

    [HttpGet("{id:guid}", Name = nameof(GetRebelById))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RebelDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetRebelById(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(new GetRebelByIdQuery() { Id = id }, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);
        else
            return Ok(result.Value);
    }

    [HttpPost(Name = nameof(PostRebel))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> PostRebel(
        [FromBody] CreateRebelCommand command,
        CancellationToken cancellationToken
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
            return NoContent();
        else
            return BadRequest(result.Error);
    }

}