public class UserItemDTO
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedAt { get; set; }

    public UserItemDTO() { }
    public UserItemDTO(User user) =>
        (Id, Username, Password, Email, CreatedAt) = (user.Id, user.Username, user.Password, user.Email, user.CreatedAt);
}