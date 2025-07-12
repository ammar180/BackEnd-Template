using Application.Users.GetById;
using Domain.Users;

namespace Application.Users;
internal class UserService : IUserService
{
  private readonly IReadRepository<User> _repo;

  public UserService(IReadRepository<User> repo)
  {
    _repo = repo;
  }

  public async Task<Result<UserResponse>> GetById(int id, CancellationToken cancellationToken)
  {
    User? u = await _repo.GetByIdAsync(id, cancellation: cancellationToken);

    if (u is null)
    {
      return Result.Failure<UserResponse>(UserErrors.NotFound(id));
    }

    var user = new UserResponse
    {
      Id = u.Id,
      FirstName = u.FirstName,
      LastName = u.LastName,
      Email = u.Email
    };

    return user;

  }
}
