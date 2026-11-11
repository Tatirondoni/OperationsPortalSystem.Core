namespace OperationsPortal.Application.Dtos.TicketStatuses;

public sealed class TicketStatusResponse
{
    public int TicketStatusId { get; init; }

    public string Name { get; init; } = null!;

    public string? Description { get; init; }
}