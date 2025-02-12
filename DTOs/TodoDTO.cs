using System;
using System.ComponentModel.DataAnnotations;
using TraskioApi.Models;

namespace TraskioApi.DTOs;
public class TodoItemDTO
{
    public int Id { get; set; }
    public int DashboardId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public TodoItemDTO() { }
    public TodoItemDTO(Todo todo) =>
        (Id, DashboardId, Title, Description, Priority, StartDate, DueDate, CreatedAt, UpdatedAt) = 
        (todo.Id, todo.DashboardId, todo.Title, todo.Description, todo.Priority, todo.StartDate, todo.DueDate, todo.CreatedAt, todo.UpdatedAt);
}

public class CreateTodoDTO
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    public int DashboardId { get; set; }
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
}

public class UpdateTodoDTO
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium";
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
}
