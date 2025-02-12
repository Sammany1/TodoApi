using System;
using System.ComponentModel.DataAnnotations;
using TraskioApi.Models;

namespace TraskioApi.DTOs;
public class UserItemDTO
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedAt { get; set; }

    public UserItemDTO() { }
    public UserItemDTO(User user) =>
        (Id, Username, Email, CreatedAt) = (user.Id, user.Username, user.Email, user.CreatedAt);
}

public class CreateUserDTO
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class UpdateUserDTO
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 6)]
    public string? Password { get; set; }
}