using Application.Abstractions;
using Infrastructure.Db;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton(_ =>
        {
            var options = DbOptionsFromEnv.Load();
            return NpgsqlDataSourceFactory.Create(options);
        });

        services.AddSingleton<IRecordsRepository, NpgsqlRecordsRepository>();

        return services;
    }
}

