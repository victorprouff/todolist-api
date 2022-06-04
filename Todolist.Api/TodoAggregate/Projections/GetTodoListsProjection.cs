namespace Todolist.Api.TodoAggregate.Projections;

public record GetTodoListsProjection(Guid Id, string Title, string? Description);