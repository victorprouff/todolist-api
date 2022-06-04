using Todolist.Api.TodoAggregate.Projections;

namespace Todolist.Api.Data.Projections.Interfaces;

public interface GetTodoListsBuilder
{
    Task<GetTodoListsProjection[]> GetTodosListAsync(CancellationToken cancellationToken);
}