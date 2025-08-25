using Domain.Todos;
using SharedKernel.Messaging;

namespace Application.Todos.Complete;

internal sealed class CompleteTodoCommandHandler(IRepository<TodoItem> _repo)
    : ICommandHandler<CompleteTodoCommand>
{
    public async Task<Result> Handle(CompleteTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await _repo.GetByIdAsync(command.TodoItemId, cancellationToken);

        if (todoItem is null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));
        }

        if (todoItem.IsCompleted)
        {
            return Result.Failure(TodoItemErrors.AlreadyCompleted(command.TodoItemId));
        }

        todoItem.IsCompleted = true;
        todoItem.CompletedAt = DateTime.UtcNow;

        todoItem.Raise(new TodoItemCompletedDomainEvent(todoItem.Id));

        await _repo.Update(todoItem, cancellationToken);

        return Result.Success();
    }
}
