using System.Net.Http.Headers;
using Core.Todos;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Delete;

internal sealed class DeleteTodoCommandHandler(IRepository<TodoItem> _repo)
    : IRequestHandler<DeleteTodoCommand, Result>
{
    public async Task<Result> Handle(DeleteTodoCommand command, CancellationToken cancellationToken)
    {
        TodoItem? todoItem = await _repo.GetFirstOrDefaultAsync( _repo.Query
            .Where(t => t.Id == command.TodoItemId),cancellation: cancellationToken);

        if (todoItem is null)
        {
            return Result.Failure(TodoItemErrors.NotFound(command.TodoItemId));
        }

        await _repo.Delete(todoItem, cancellationToken);

        return Result.Success();
    }
}
