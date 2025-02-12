using System;
using System.ComponentModel.DataAnnotations;
using TodoApi.Models;

namespace TodoApi.DTOs;
public class SubtaskItemDTO
{
    public int Id { get; set; }
    public int TodoId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;
    public DateTime CreatedAt { get; set; }

    public SubtaskItemDTO() { }
    public SubtaskItemDTO(Subtask subtask) =>
        (Id, TodoId, Description, IsDone, CreatedAt) = 
        (subtask.Id, subtask.TodoId, subtask.Description, subtask.IsDone, subtask.CreatedAt);
}

public class CreateSubtaskDTO
{
    [Required]
    public string Description { get; set; } = string.Empty;
    public int TodoId { get; set; }

}

public class UpdateSubtaskDTO
{
    [Required]
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;
}
