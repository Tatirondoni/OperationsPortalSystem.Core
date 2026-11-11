using Microsoft.AspNetCore.Mvc;
using OperationsPortal.Application.Dtos.TicketStatuses;
using OperationsPortal.Application.Services.TicketStatuses;

namespace OperationsPortal.Api.Controllers;

[ApiController]
[Route("api/ticket-statuses")]
[Produces("application/json")]
public sealed class TicketStatusesController : ControllerBase
{
    private readonly ITicketStatusService _service;

    public TicketStatusesController(ITicketStatusService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(IReadOnlyList<TicketStatusResponse>),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TicketStatusResponse>>>
        GetAll(CancellationToken cancellationToken)
    {
        var statuses = await _service.GetAllAsync(cancellationToken);

        return Ok(statuses);
    }

    [HttpGet("{id:int:min(1)}")]
    [ProducesResponseType(
        typeof(TicketStatusResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketStatusResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var status = await _service.GetByIdAsync(
            id,
            cancellationToken);

        if (status is null)
        {
            return NotFound();
        }

        return Ok(status);
    }
}