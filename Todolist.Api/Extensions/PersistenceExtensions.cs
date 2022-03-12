using Dapper;
using Npgsql;

namespace Todolist.Api.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return services;
    }
}