using Core.Todos;

namespace Application.Todos.GetById;

internal sealed class GetTodoByIdQueryHandler(IReadRepository<TodoItem> _repo)
    : IRequestHandler<GetTodoByIdQuery, Result<TodoResponse>>
{
    public async Task<Result<TodoResponse>> Handle(GetTodoByIdQuery query, CancellationToken cancellationToken)
    {
        TodoResponse? todo = await _repo.GetFirstOrDefaultAsync(_repo.Query
            .Where(todoItem => todoItem.Id == query.TodoItemId),
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
            },
            cancellation: cancellationToken);

        if (todo is null)
        {
            return Result.Failure<TodoResponse>(TodoItemErrors.NotFound(query.TodoItemId));
        }

        return todo;
    }
}
