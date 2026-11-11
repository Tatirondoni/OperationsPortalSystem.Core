using Microsoft.EntityFrameworkCore;
using OperationsPortal.Application.Interfaces.Repositories;
using OperationsPortal.Infrastructure.Data;

using DomainTicketStatus =
    OperationsPortal.Domain.Entities.TicketStatus;

namespace OperationsPortal.Infrastructure.Repositories;

public sealed class TicketStatusRepository : ITicketStatusRepository
{
    private readonly OperationsPortalDbContext _context;

    public TicketStatusRepository(OperationsPortalDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DomainTicketStatus>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.TicketStatuses
            .AsNoTracking()
            .OrderBy(status => status.TicketStatusId)
            .Select(status => new DomainTicketStatus
            {
                TicketStatusId = status.TicketStatusId,
                Name = status.Name,
                Description = status.Description
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<DomainTicketStatus?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.TicketStatuses
            .AsNoTracking()
            .Where(status => status.TicketStatusId == id)
            .Select(status => new DomainTicketStatus
            {
                TicketStatusId = status.TicketStatusId,
                Name = status.Name,
                Description = status.Description
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}