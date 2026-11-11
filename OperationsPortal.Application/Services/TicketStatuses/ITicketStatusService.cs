using OperationsPortal.Application.Dtos.TicketStatuses;

namespace OperationsPortal.Application.Services.TicketStatuses;

public interface ITicketStatusService
{
    Task<IReadOnlyList<TicketStatusResponse>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<TicketStatusResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}