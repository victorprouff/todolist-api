namespace Todolist.Api.Models;

public record UpdateTaskRequest(string Title, string Description, string Status);