using Domain.Todos;
using Domain.Users;

namespace Application.Todos.Create;

public sealed class CreateTodoCommandHandler(
    IReadRepository<User> _userRepo,
    IRepository<TodoItem> _todoRepo)
    : IRequestHandler<CreateTodoCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateTodoCommand command, CancellationToken cancellationToken)
    {
        User? user = await _userRepo.GetByIdAsync(command.UserId, cancellation: cancellationToken);

        if (user is null)
        {
            return Result.Failure<int>(UserErrors.NotFound(command.UserId));
        }

        var todoItem = new TodoItem
        {
            UserId = user.Id,
            Description = command.Description,
            Priority = command.Priority,
            DueDate = command.DueDate,
            Labels = command.Labels,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await _todoRepo.Add(todoItem, cancellationToken);

        return todoItem.Id;
    }
}
