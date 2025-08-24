namespace Application.Todos.Delete;

public sealed record DeleteTodoCommand(int TodoItemId) : IRequest<Result>;
