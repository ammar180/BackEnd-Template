namespace ToDo.Api.DTOs;

public sealed class TodoDTO
{
    public int UserId { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> Labels { get; set; } = [];
    public int Priority { get; set; }
}
