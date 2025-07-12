using Application.Todos.Complete;
using Application.Todos.Create;
using Application.Todos.Delete;
using Application.Todos.Get;
using Application.Todos.GetById;
using Core.Todos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.DTOs;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDosController(ISender sender) : ControllerBase
{
  [HttpGet]
  public async Task<IActionResult> GetByUserId(int userId, ISender sender, CancellationToken cancellationToken)
  {
    var command = new GetTodosQuery(userId);

    Result<List<Application.Todos.Get.TodoResponse>> result = await sender.Send(command, cancellationToken);

    return result.Match(r => Ok(r), CustomResults.Problem);
  }
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
  {
    var command = new GetTodoByIdQuery(id);

    Result<Application.Todos.GetById.TodoResponse> result = await sender.Send(command, cancellationToken);

    return result.Match(r => Ok(r), CustomResults.Problem);
  }
  [HttpPut("{id}/complete")]
  public async Task<IActionResult> SetComplete(int id, CancellationToken cancellationToken)
  {
    var command = new CompleteTodoCommand(id);

    Result result = await sender.Send(command, cancellationToken);

    return result.Match(NoContent, CustomResults.Problem);
  }
  [HttpPost]
  public async Task<IActionResult> NewTodo(TodoDTO request, CancellationToken cancellationToken)
  {
    var command = new CreateTodoCommand
    {
      UserId = request.UserId,
      Description = request.Description,
      DueDate = request.DueDate,
      Labels = request.Labels,
      Priority = (Priority)request.Priority
    };

    Result<int> result = await sender.Send(command, cancellationToken);

    return result.Match(success => Ok(success), failure => CustomResults.Problem(failure));
  }
  [HttpDelete("{id}")]
  public async Task<IActionResult> RemoveTodo(int id, CancellationToken cancellationToken)
  {
    var command = new DeleteTodoCommand(id);

    Result result = await sender.Send(command, cancellationToken);

    return result.Match(NoContent, CustomResults.Problem);
  }
}
