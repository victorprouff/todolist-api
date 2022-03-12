using Dapper;
using NodaTime;
using Npgsql;
using Todolist.Api.TodoAggregate;

namespace Todolist.Api.Data.Repositories;

public class TodoRepository : Interfaces.TodoRepository
{
    private readonly string connectionString;

    private NpgsqlConnection GetConnection() => new(connectionString);

    public TodoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<Guid> CreateTodoAsync(string title, string description, Instant currentDate, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        return await connection.ExecuteScalarAsync<Guid>(
            @"INSERT INTO todo (title, description, created_at, updated_at)
           VALUES (@Title, @Description, @CurrentDate, @CurrentDate)
                RETURNING id;",
            new { Title = title, Description = description, CurrentDate = currentDate },
            commandTimeout: 1);
    }

    public async Task UpdateTodoAsync(Guid id, string title, string description, Instant currentDate, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        await connection.ExecuteAsync(
            @"UPDATE todo SET title = @Title, description = @Description, updated_at = @CurrentDate WHERE id = @Id;",
            new { Title = title, Description = description, CurrentDate = currentDate, @Id = id },
            commandTimeout: 1);
    }

    public async Task<Todo[]> GetTodosAsync(CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        var todos = await connection.QueryAsync<Todo>(
            @"SELECT id, title, description FROM todo;",
            commandTimeout: 1);
        return todos.ToArray();
    }

    public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        await connection.ExecuteAsync(
            @"DELETE FROM todo WHERE id = @Id;",
            new { Id = id },
            commandTimeout: 1);
    }
}