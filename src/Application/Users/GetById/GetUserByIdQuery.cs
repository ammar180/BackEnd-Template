using MediatR;
using SharedKernel;

namespace Application.Users.GetById;

public sealed record GetUserByIdQuery(int UserId) : IRequest<Result<UserResponse>>;
