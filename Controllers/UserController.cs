using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Interfaces;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly int userId;

        public UserController(IUserService userService)
        {
            _userService = userService;
            userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userService.GetUserAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            var updated = await _userService.UpdateUserAsync(userId, updateUserDTO);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            return deleted ? NoContent() : NotFound();
        }
    }
}