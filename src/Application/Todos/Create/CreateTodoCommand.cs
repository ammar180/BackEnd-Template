﻿using Core.Todos;

namespace Application.Todos.Create;

public sealed class CreateTodoCommand : IRequest<Result<int>>
{
    public int UserId { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> Labels { get; set; } = [];
    public Priority Priority { get; set; }
}
