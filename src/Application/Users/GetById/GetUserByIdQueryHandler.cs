using Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.GetById;

internal sealed class GetUserByIdQueryHandler(IReadRepository<User> _repo)
    : IRequestHandler<GetUserByIdQuery, Result<UserResponse>>
    
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        User? u = await _repo.GetByIdAsync(query.UserId, cancellation: cancellationToken);

        if (u is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId));
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
