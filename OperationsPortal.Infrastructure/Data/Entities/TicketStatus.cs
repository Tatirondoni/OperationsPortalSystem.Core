using System;
using System.Collections.Generic;

namespace OperationsPortal.Infrastructure.Data.Entities;

public partial class TicketStatus
{
    public int TicketStatusId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<TicketHistory> TicketHistoryNewTicketStatuses { get; set; } = new List<TicketHistory>();

    public virtual ICollection<TicketHistory> TicketHistoryPreviousTicketStatuses { get; set; } = new List<TicketHistory>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
