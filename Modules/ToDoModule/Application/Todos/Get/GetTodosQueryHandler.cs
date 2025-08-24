using Domain.Todos;
using Microsoft.EntityFrameworkCore;

namespace Application.Todos.Get;

internal sealed class GetTodosQueryHandler(IReadRepository<TodoItem> _repo)
    : IRequestHandler<GetTodosQuery, Result<List<TodoResponse>>>
{
    public async Task<Result<List<TodoResponse>>> Handle(GetTodosQuery query, CancellationToken cancellationToken)
    {
        List<TodoResponse> todos = await _repo.GetListAsync(
            query: _repo.Query
            .Where(t => t.UserId == query.UserId),
            selector: todoItem => new TodoResponse
            {
                Id = todoItem.Id,
                UserId = todoItem.UserId,
                Description = todoItem.Description,
                DueDate = todoItem.DueDate,
                Labels = todoItem.Labels,
                IsCompleted = todoItem.IsCompleted,
                CreatedAt = todoItem.CreatedAt,
                CompletedAt = todoItem.CompletedAt
            }, cancellation: cancellationToken);

        return todos;
    }
}
