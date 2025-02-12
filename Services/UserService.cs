using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraskioApi.Utils;
using TraskioApi.Interfaces;
using TraskioApi.Models;
using TraskioApi.DTOs;

namespace TraskioApi.Services;
public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
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

    public async Task<UserItemDTO> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        var user = new User
        {
            Username = createUserDTO.Username,
            Password = PasswordHasher.HashPassword(createUserDTO.Password),
            Email = createUserDTO.Email
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return new UserItemDTO(user);
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDTO updateUserDTO)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return false;
        }

        user.Username = updateUserDTO.Username;
        user.Email = updateUserDTO.Email;
        
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

    public async Task<UserItemDTO?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        return user != null ? new UserItemDTO(user) : null;
    }

    public async Task<User?> GetFullUserByEmailAsync(string email) 
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}