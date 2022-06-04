using NodaTime;
using Todolist.Api.TodoAggregate;
using Task = System.Threading.Tasks.Task;

namespace Todolist.Api.Data.Repositories.Interfaces;

public interface TodoRepository
{
    Task<Guid> CreateTodoListAsync(Todo todo, CancellationToken cancellationToken);
    Task UpdateTodoAsync(Todo todo, CancellationToken cancellationToken);
    // Task DeleteTodoAsync(Guid id, CancellationToken cancellationToken);
}