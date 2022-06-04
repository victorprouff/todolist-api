using Dapper;
using Todolist.Api.TodoAggregate.Projections;

namespace Todolist.Api.Data.Projections;

public class GetTodoListsBuilder : BaseProjectionBuilder, Interfaces.GetTodoListsBuilder
{
    public GetTodoListsBuilder(string connectionString)
        : base(connectionString)
    {
    }

    public async Task<GetTodoListsProjection[]> GetTodosListAsync(CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        var todos = await connection.QueryAsync<GetTodoListsProjection>(
            @"SELECT id, title, description FROM todo_list;",
            commandTimeout: 1);

        return todos.ToArray();
    }
}