using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Todolist.Api.Data.Repositories.Interfaces;
using Todolist.Api.Models;

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
    ///     Ajout d'une tâche
    /// </summary>
    /// <param name="createTaskRequest">La tâche à créer</param>
    /// <param name="cancellationToken"></param>
    [HttpPost(Name = "CreateTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateTask(CreateTaskRequest createTaskRequest, CancellationToken cancellationToken)
    {
        var id = await repository.CreateTodoAsync(createTaskRequest.Title, createTaskRequest.Description, clock.GetCurrentInstant(), cancellationToken);
        return Ok(id);
    }

    /// <summary>
    ///     Récupère la liste des tâches
    /// </summary>
    /// <response code="200">La liste de tâches</response>
    /// <param name="cancellationToken"></param>
    [HttpGet(Name = "GetTasks")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(IEnumerable<GetTasksResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetTasks(CancellationToken cancellationToken)
    {
        var todos = await repository.GetTodosAsync(cancellationToken);
        return Ok(todos.Select(t => (GetTasksResponse)t));
    }

    /// <summary>
    ///     Modifie une tâche
    /// </summary>
    /// <param name="taskId">L'id de la tâche</param>
    /// <param name="updateTaskRequest">Les infos de la tâche à modifier</param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">La tâche</response>
    [HttpPut("{taskId:guid}", Name = "UpdateTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskRequest updateTaskRequest, CancellationToken cancellationToken)
    {
        await repository.UpdateTodoAsync(taskId, updateTaskRequest.Title, updateTaskRequest.Description, clock.GetCurrentInstant(), cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Supprime une tâche
    /// </summary>
    /// <param name="taskId">L'id d'une tâche</param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("{taskId:guid}", Name = "DeleteTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTask(Guid taskId, CancellationToken cancellationToken)
    {
        await repository.DeleteTodoAsync(taskId, cancellationToken);
        return NoContent();
    }
}