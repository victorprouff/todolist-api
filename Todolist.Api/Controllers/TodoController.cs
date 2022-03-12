using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Todolist.Api.Models;

namespace Todolist.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("v{version:apiVersion}/todo")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class TodoController : ControllerBase
{
    /// <summary>
    ///     Ajout d'une tâche
    /// </summary>
    /// <param name="createTaskRequest">La tâche à créer</param>
    [HttpPost(Name = "CreateTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public Task<IActionResult> CreateTask(CreateTaskRequest createTaskRequest)
    {
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    ///     Récupère la liste des tâches
    /// </summary>
    /// <response code="200">La liste de tâches</response>
    [HttpGet(Name = "GetTasks")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public Task<IActionResult> GetTasks()
    {
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    ///     Modifie une tâche
    /// </summary>
    /// <param name="taskId">L'id de la tâche</param>
    /// <param name="updateTaskRequest">Les infos de la tâche à modifier</param>
    /// <response code="200">La tâche</response>
    [HttpPut("{taskId:guid}", Name = "UpdateTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskRequest updateTaskRequest)
    {
        return Task.FromResult<IActionResult>(NoContent());
    }

    /// <summary>
    ///     Supprime une tâche
    /// </summary>
    /// <param name="taskId">L'id d'une tâche</param>
    [HttpDelete("{taskId:guid}", Name = "DeleteTask")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public Task<IActionResult> DeleteTask(Guid taskId)
    {
        return Task.FromResult<IActionResult>(NoContent());
    }
}