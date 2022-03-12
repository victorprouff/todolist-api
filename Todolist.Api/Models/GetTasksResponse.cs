using Todolist.Api.TodoAggregate;

namespace Todolist.Api.Models;

public record GetTasksResponse(Guid Id, string Title, string Description)
{
    public static explicit operator GetTasksResponse(Todo todo) => new(todo.Id, todo.Title, todo.Description);
}