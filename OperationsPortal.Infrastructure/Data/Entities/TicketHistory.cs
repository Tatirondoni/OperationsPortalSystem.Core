using System;
using System.Collections.Generic;

namespace OperationsPortal.Infrastructure.Data.Entities;

public partial class TicketHistory
{
    public int TicketHistoryId { get; set; }

    public int TicketId { get; set; }

    public int? PreviousTicketStatusId { get; set; }

    public int NewTicketStatusId { get; set; }

    public int ChangedByUserId { get; set; }

    public string? Comment { get; set; }

    public DateTime ChangedAt { get; set; }

    public virtual User ChangedByUser { get; set; } = null!;

    public virtual TicketStatus NewTicketStatus { get; set; } = null!;

    public virtual TicketStatus? PreviousTicketStatus { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
