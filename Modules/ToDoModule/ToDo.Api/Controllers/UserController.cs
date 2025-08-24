using Application.Users.GetById;
using Application.Users;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace ToDo.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
  {
    Result<UserResponse> result = await userService.GetById(id, cancellationToken);

    if(!result.IsSuccess)
    {
      return result.Error.Type switch
      {
        ErrorType.NotFound => NotFound(result.Error),
        ErrorType.Validation => BadRequest(result.Error),
        _ => StatusCode(500, result.Error)
      };
    }

    return Ok(result.Value);
  }
}
