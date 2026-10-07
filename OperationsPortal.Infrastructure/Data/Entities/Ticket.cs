using System;
using System.Collections.Generic;

namespace OperationsPortal.Infrastructure.Data.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int OpenedByUserId { get; set; }

    public int? AssignedToUserId { get; set; }

    public int CategoryId { get; set; }

    public int TicketStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User OpenedByUser { get; set; } = null!;

    public virtual ICollection<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();

    public virtual TicketStatus TicketStatus { get; set; } = null!;
}
