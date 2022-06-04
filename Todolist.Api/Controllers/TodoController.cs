using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Todolist.Api.Data.Repositories.Interfaces;
using Todolist.Api.Models;
using Todolist.Api.TodoAggregate;
using Task = Todolist.Api.TodoAggregate.Task;

namespace Todolist.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/todo")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class TodoController : ControllerBase
{
    private readonly TodoRepository repository;
    private readonly IClock clock;

    public TodoController(TodoRepository repository, IClock clock)
    {
        this.repository = repository;
        this.clock = clock;
    }

    /// <summary>
    ///     Ajout d'une todolist
    /// </summary>
    /// <param name="createTodoListRequest">La tâche à créer</param>
    /// <param name="cancellationToken"></param>
    [HttpPost(Name = "CreateTodoList")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateTodoList(CreateTodoListRequest createTodoListRequest, CancellationToken cancellationToken)
    {
        var id = await repository.CreateTodoListAsync((Todo)createTodoListRequest, cancellationToken);
        return Ok(id);
    }

    /// <summary>
    ///     Récupère la liste des TodoList
    /// </summary>
    /// <response code="200">La liste de Todolist</response>
    /// <param name="cancellationToken"></param>
    [HttpGet(Name = "GetTodoLists")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(IEnumerable<GetTodoListsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetTodoLists(CancellationToken cancellationToken)
    {
        var todos = await repository.GetTodosListAsync(cancellationToken);
        return Ok(todos.Select(t => (GetTodoListsResponse)t));
    }

    /// <summary>
    ///     Modifie une TodoList
    /// </summary>
    /// <param name="todoId">L'id de la tâche</param>
    /// <param name="updateTodoListRequest">Les infos de la tâche à modifier</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">La tâche</response>
    [HttpPut("{todoId:guid}", Name = "UpdateTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateTask(Guid todoId, UpdateTodoListRequest updateTodoListRequest, CancellationToken cancellationToken)
    {
        await repository.UpdateTodoAsync(
            new Todo(
                todoId,
                updateTodoListRequest.Title,
                updateTodoListRequest.Description,
                new List<Task>(),
                updateTodoListRequest.DeadlineAt,
                updateTodoListRequest.StartedAt,
                updateTodoListRequest.EndedAt), cancellationToken);
        return NoContent();
    }
    //
    // /// <summary>
    // ///     Supprime une tâche
    // /// </summary>
    // /// <param name="taskId">L'id d'une tâche</param>
    // /// <param name="cancellationToken"></param>
    // [HttpDelete("{taskId:guid}", Name = "DeleteTask")]
    // [Consumes("application/json")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesDefaultResponseType]
    // public async Task<IActionResult> DeleteTask(Guid taskId, CancellationToken cancellationToken)
    // {
    //     await repository.DeleteTodoAsync(taskId, cancellationToken);
    //     return NoContent();
    // }
}