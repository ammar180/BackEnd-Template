using SharedKernel;

namespace Core.Todos;

public sealed record TodoItemCompletedDomainEvent(int TodoItemId) : IDomainEvent;
