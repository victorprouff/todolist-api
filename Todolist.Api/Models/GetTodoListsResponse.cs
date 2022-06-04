using NodaTime;
using Todolist.Api.TodoAggregate;
using Task = Todolist.Api.TodoAggregate.Task;

namespace Todolist.Api.Models;

public record GetTodoListsResponse(Guid Id, string Title, string? Description, Instant? Deadline, Instant? StartedAt, Instant? EndedAt)
{
    public static explicit operator GetTodoListsResponse(Todo todo) => new(todo.Id, todo.Title, todo.Description, todo.DeadlineAt, todo.StartedAt, todo.EndedAt);
}