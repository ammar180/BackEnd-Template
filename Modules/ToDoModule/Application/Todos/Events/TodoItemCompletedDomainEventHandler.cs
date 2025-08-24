using Microsoft.Extensions.Logging;
using MediatR;
using Domain.Todos;

namespace Application.Todos.Events;

internal class TodoItemCompletedDomainEventHandler(
    ILogger<TodoItemCompletedDomainEventHandler> logger)
    : INotificationHandler<TodoItemCompletedDomainEvent>
{
    public async Task Handle(TodoItemCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Todo item with ID {TodoItemId} has been completed.",
            notification.TodoItemId
        );

        await Task.CompletedTask;
    }
}
