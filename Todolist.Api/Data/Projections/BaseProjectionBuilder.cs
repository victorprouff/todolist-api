using Npgsql;

namespace Todolist.Api.Data.Projections;

public abstract class BaseProjectionBuilder
{
    private readonly string connectionString;

    protected BaseProjectionBuilder(string connectionString)
    {
        this.connectionString = connectionString;
    }

    protected NpgsqlConnection GetConnection() => new(connectionString);
}