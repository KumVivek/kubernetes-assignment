using Application.Abstractions;
using Domain.Entities;

namespace Application.UseCases.GetRecords;

public sealed class GetRecordsHandler(IRecordsRepository repository)
{
    public Task<IReadOnlyList<Record>> Handle(GetRecordsQuery _query, CancellationToken ct) =>
        repository.GetRecordsAsync(ct);
}

