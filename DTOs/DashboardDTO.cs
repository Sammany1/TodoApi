using System;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.DTOs;
public class DashboardItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int UserId { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; }

    public DashboardItemDTO() { }
    public DashboardItemDTO(Dashboard dashboard) =>
        (Id, Name, Description, UserId, Color, CreatedAt) = 
        (dashboard.Id, dashboard.Name, dashboard.Description, dashboard.UserId, dashboard.Color, dashboard.CreatedAt);
}

public class CreateDashboardDTO
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string? Description { get; set; }
    public string? Color { get; set; }
}

public class UpdateDashboardDTO
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public string? Color { get; set; }
}
