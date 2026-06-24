using Domain.Entities;

namespace Application.Abstractions;

public interface IRecordsRepository
{
    Task<IReadOnlyList<Record>> GetRecordsAsync(CancellationToken ct);
    Task<bool> CanConnectAsync(CancellationToken ct);
}

