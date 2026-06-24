using Npgsql;

namespace Infrastructure.Db;

public static class NpgsqlDataSourceFactory
{
    public static NpgsqlDataSource Create(DbOptions options)
    {
        var csb = new NpgsqlConnectionStringBuilder
        {
            Host = options.Host,
            Port = options.Port,
            Database = options.Database,
            Username = options.Username,
            Password = options.Password,
            ApplicationName = options.AppName,
            Pooling = true,
            MinPoolSize = options.PoolMin,
            MaxPoolSize = options.PoolMax,
            Timeout = options.ConnectTimeoutSeconds,
            CommandTimeout = options.CommandTimeoutSeconds,
            IncludeErrorDetail = false
        };

        return NpgsqlDataSource.Create(csb.ConnectionString);
    }
}

