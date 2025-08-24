namespace Application.Todos.GetById;

public sealed record GetTodoByIdQuery(int TodoItemId) : IRequest<Result<TodoResponse>>;
