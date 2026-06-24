using Application.UseCases.GetRecords;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddInfrastructure();
builder.Services.AddScoped<GetRecordsHandler>();

var app = builder.Build();

app.MapGet("/", () =>
{
    var appVersion = GetEnvOrDefault("APP_VERSION", "dev");
    return Results.Ok(new { status = "ok", appVersion });
});

app.MapGet("/health/live", () => Results.Ok(new { status = "live" }));

app.MapGet("/health/ready", async (Application.Abstractions.IRecordsRepository repo, CancellationToken ct) =>
{
    var ok = await repo.CanConnectAsync(ct);
    return ok
        ? Results.Ok(new { status = "ready" })
        : Results.StatusCode(StatusCodes.Status503ServiceUnavailable);
});

app.MapGet("/records", async (GetRecordsHandler handler, CancellationToken ct) =>
{
    var records = await handler.Handle(new GetRecordsQuery(), ct);

    var appVersion = GetEnvOrDefault("APP_VERSION", "dev");
    return Results.Ok(new
    {
        appVersion,
        count = records.Count,
        records = records.Select(r => new { id = r.Id, name = r.Name, createdAt = r.CreatedAt })
    });
});

app.Run();

static string GetEnvOrDefault(string name, string defaultValue)
{
    var value = Environment.GetEnvironmentVariable(name);
    return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
}

