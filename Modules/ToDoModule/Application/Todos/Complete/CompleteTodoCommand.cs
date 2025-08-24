namespace Application.Todos.Complete;

public sealed record CompleteTodoCommand(int TodoItemId) : IRequest<Result>;
