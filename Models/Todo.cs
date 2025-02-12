using System;
using System.Collections.Generic;

namespace TraskioApi.Models;
public class Todo
{
    public int Id { get; set; }
    public int DashboardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public Dashboard Dashboard { get; set; } = null!;
    public ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
}
