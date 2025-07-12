using Application.Users.GetById;

namespace Application.Users;
public interface IUserService
{
  Task<Result<UserResponse>> GetById(int id, CancellationToken cancellationToken);
}
