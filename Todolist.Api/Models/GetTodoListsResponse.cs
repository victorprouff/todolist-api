using NodaTime;
using Todolist.Api.TodoAggregate;
using Todolist.Api.TodoAggregate.Projections;
using Task = Todolist.Api.TodoAggregate.Task;

namespace Todolist.Api.Models;

public record GetTodoListsResponse(Guid Id, string Title, string? Description)
{
    public static explicit operator GetTodoListsResponse(GetTodoListsProjection todo) => new(todo.Id, todo.Title, todo.Description);
}