namespace Application.Todos.Get;

public sealed record GetTodosQuery(int UserId) : IRequest<Result<List<TodoResponse>>>;
