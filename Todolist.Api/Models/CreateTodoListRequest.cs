using NodaTime;
using Todolist.Api.TodoAggregate;

namespace Todolist.Api.Models;

public record CreateTodoListRequest(string Title, string? Description, Instant? DeadlineAt, Instant? StartedAt, Instant? EndedAt)
{
    public static explicit operator Todo(CreateTodoListRequest response) =>
        new(
            Guid.Empty,
            response.Title,
            response.Description,
            new List<TodoAggregate.Task>(),
            response.DeadlineAt,
            response.StartedAt,
            response.EndedAt);
}