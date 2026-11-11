namespace OperationsPortal.Domain.Entities;

public sealed class TicketStatus
{
    public int TicketStatusId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}