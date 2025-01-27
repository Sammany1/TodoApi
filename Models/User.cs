// filepath: /W:/DotNet/TodoApi/Models/User.cs
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("password")]
    public string? Password { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}