using System;

namespace TodoApi.Models;
public class Subtask
{
    public int Id { get; set; }
    public int TodoId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Todo Todo { get; set; } = null!;
}
