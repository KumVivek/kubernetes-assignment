namespace Infrastructure.Db;

public sealed record DbOptions(
    string Host,
    int Port,
    string Database,
    string Username,
    string Password,
    string AppName,
    int PoolMin,
    int PoolMax,
    int ConnectTimeoutSeconds,
    int CommandTimeoutSeconds
);

