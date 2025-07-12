using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Core.Todos;

public class TodoItem : Entity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> Labels { get; set; } = [];
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; }
    public Priority Priority { get; set; }
}
