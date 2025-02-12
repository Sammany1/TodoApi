using System;
using System.Collections.Generic;

namespace TodoApi.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();
}