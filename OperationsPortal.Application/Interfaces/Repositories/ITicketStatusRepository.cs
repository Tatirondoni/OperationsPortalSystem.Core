using OperationsPortal.Domain.Entities;

namespace OperationsPortal.Application.Interfaces.Repositories;

public interface ITicketStatusRepository
{
    Task<IReadOnlyList<TicketStatus>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<TicketStatus?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}