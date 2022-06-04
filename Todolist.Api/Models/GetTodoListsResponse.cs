using Todolist.Api.TodoAggregate.Projections;

namespace Todolist.Api.Models;

public record GetTodoListsResponse(Guid Id, string Title, string? Description)
{
    public static explicit operator GetTodoListsResponse(GetTodoListsProjection todo) => new(todo.Id, todo.Title, todo.Description);
}