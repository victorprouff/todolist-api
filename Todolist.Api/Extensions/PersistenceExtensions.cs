using Autofac;
using Dapper;
using Npgsql;
using Todolist.Api.Data.Handlers;
using Todolist.Api.Data.Projections;

namespace Todolist.Api.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        SqlMapper.AddTypeHandler(new InstantHandler());

        return services;
    }
}