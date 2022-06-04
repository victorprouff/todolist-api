using NodaTime;

namespace Todolist.Api.TodoAggregate;

public record Todo(Guid Id, string Title, string? Description, List<Task> Tasks, Instant? DeadlineAt, Instant? StartedAt, Instant? EndedAt);
public record Task(Guid Id, string Title, string? Description, Status Status, Instant? StartedAt, Instant? EndedAt);

public enum Status
{
    Todo = 0,
    InProgress = 1,
    StandBy = 2,
    Done = 3,
    Cancel = 4
}