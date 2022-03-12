using NodaTime;
using Todolist.Api.TodoAggregate;

namespace Todolist.Api.Data.Repositories.Interfaces;

public interface TodoRepository
{
    Task<Guid> CreateTodoAsync(string title, string description, Instant currentDate, CancellationToken cancellationToken);
    Task UpdateTodoAsync(Guid id, string title, string description, Instant currentDate, CancellationToken cancellationToken);
    Task<Todo[]> GetTodosAsync(CancellationToken cancellationToken);
    Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken);
}