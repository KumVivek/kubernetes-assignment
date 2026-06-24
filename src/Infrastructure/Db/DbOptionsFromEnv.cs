namespace Infrastructure.Db;

public static class DbOptionsFromEnv
{
    public static DbOptions Load()
    {
        var host = GetEnvOrThrow("DB_HOST");
        var port = int.Parse(GetEnvOrDefault("DB_PORT", "5432"));
        var database = GetEnvOrThrow("DB_NAME");
        var username = GetEnvOrThrow("DB_USER");
        var password = GetEnvOrThrow("DB_PASSWORD");
        var appName = GetEnvOrDefault("APP_NAME", "nagp-api");

        return new DbOptions(
            Host: host,
            Port: port,
            Database: database,
            Username: username,
            Password: password,
            AppName: appName,
            PoolMin: int.Parse(GetEnvOrDefault("DB_POOL_MIN", "0")),
            PoolMax: int.Parse(GetEnvOrDefault("DB_POOL_MAX", "100")),
            ConnectTimeoutSeconds: int.Parse(GetEnvOrDefault("DB_CONNECT_TIMEOUT_SECONDS", "5")),
            CommandTimeoutSeconds: int.Parse(GetEnvOrDefault("DB_COMMAND_TIMEOUT_SECONDS", "5"))
        );
    }

    private static string GetEnvOrThrow(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"Missing required environment variable '{name}'.");
        return value;
    }

    private static string GetEnvOrDefault(string name, string defaultValue)
    {
        var value = Environment.GetEnvironmentVariable(name);
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }
}

