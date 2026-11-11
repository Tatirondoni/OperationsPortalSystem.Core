using OperationsPortal.Application.Dtos.TicketStatuses;
using OperationsPortal.Application.Interfaces.Repositories;

using DomainTicketStatus =
    OperationsPortal.Domain.Entities.TicketStatus;

namespace OperationsPortal.Application.Services.TicketStatuses;

public sealed class TicketStatusService : ITicketStatusService
{
    private readonly ITicketStatusRepository _repository;

    public TicketStatusService(ITicketStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TicketStatusResponse>> GetAllAsync( 
        CancellationToken cancellationToken = default)
    {
        var statuses = await _repository.GetAllAsync(cancellationToken);

        return statuses
            .Select(ToResponse)
            .ToList();
    }

    public async Task<TicketStatusResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var status = await _repository.GetByIdAsync(
            id,
            cancellationToken);

        return status is null ? null : ToResponse(status);
    }

    private static TicketStatusResponse ToResponse(
        DomainTicketStatus status)
    {
        return new TicketStatusResponse
        {
            TicketStatusId = status.TicketStatusId,
            Name = status.Name,
            Description = status.Description
        };
    }
}