using Dapper;
using Npgsql;
using Todolist.Api.Data.Handlers;

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