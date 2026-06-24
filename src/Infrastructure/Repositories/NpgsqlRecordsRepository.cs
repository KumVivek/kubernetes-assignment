using System.Data;
using Application.Abstractions;
using Domain.Entities;
using Npgsql;

namespace Infrastructure.Repositories;

public sealed class NpgsqlRecordsRepository(NpgsqlDataSource dataSource) : IRecordsRepository
{
    public async Task<IReadOnlyList<Record>> GetRecordsAsync(CancellationToken ct)
    {
        var results = new List<Record>();

        await using var conn = await dataSource.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(
            "SELECT id, name, created_at FROM records ORDER BY id;",
            conn);
        cmd.CommandType = CommandType.Text;

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            results.Add(new Record(
                Id: reader.GetInt32(0),
                Name: reader.GetString(1),
                CreatedAt: reader.GetFieldValue<DateTimeOffset>(2)
            ));
        }

        return results;
    }

    public async Task<bool> CanConnectAsync(CancellationToken ct)
    {
        try
        {
            await using var conn = await dataSource.OpenConnectionAsync(ct);
            await using var cmd = new NpgsqlCommand("SELECT 1;", conn);
            cmd.CommandType = CommandType.Text;
            _ = await cmd.ExecuteScalarAsync(ct);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

