using Dapper;
using Npgsql;
using Todolist.Api.TodoAggregate;
using Task = System.Threading.Tasks.Task;

namespace Todolist.Api.Data.Repositories;

public class TodoRepository : Interfaces.TodoRepository
{
    private readonly string connectionString;

    private NpgsqlConnection GetConnection() => new(connectionString);

    public TodoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<Guid> CreateTodoListAsync(Todo todo, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        return await connection.ExecuteScalarAsync<Guid>(
            @"INSERT INTO todo_list (title, description, deadline_at, started_at, ended_at)
           VALUES (@Title, @Description, @DeadlineAt, @StartedAt, @EndedAt)
                RETURNING id;",
            todo,
            commandTimeout: 1);
    }

    public async Task UpdateTodoAsync(Todo todo, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        await connection.ExecuteAsync(
            @"UPDATE todo_list SET title = @Title, description = @Description, deadline_at = @DeadlineAt, started_at = @StartedAt, ended_at = @EndedAt WHERE id = @Id;",
            todo,
            commandTimeout: 1);
    }

    // public async Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken)
    // {
    //     await using var connection = GetConnection();
    //     await connection.ExecuteAsync(
    //         @"DELETE FROM todo_list WHERE id = @Id;",
    //         new { Id = id },
    //         commandTimeout: 1);
    // }
}