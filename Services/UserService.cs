using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserService
{
    private readonly UserDb _context;

    public UserService(UserDb context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserItemDTO>> GetAllUsers()
    {
        return await _context.Users.Select(x => new UserItemDTO(x)).ToListAsync();
    }

    public async Task<UserItemDTO?> GetUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user != null ? new UserItemDTO(user) : null;
    }

    public async Task CreateUserAsync(UserItemDTO userItemDTO)
    {
        var user = new User
        {
            Username = userItemDTO.Username,
            Password = userItemDTO.Password,
            Email = userItemDTO.Email,
            CreatedAt = userItemDTO.CreatedAt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateUserAsync(int id, UserItemDTO userItemDTO)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        user.Username = userItemDTO.Username;
        user.Password = userItemDTO.Password;
        user.Email = userItemDTO.Email;
        user.CreatedAt = userItemDTO.CreatedAt;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}