using System;
using System.Collections.Generic;

namespace TodoApi.Models;
public class Dashboard
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int UserId { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public User User { get; set; } = null!;
    public ICollection<Todo> Todos { get; set; } = new List<Todo>();
}
