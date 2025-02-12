using Microsoft.AspNetCore.Mvc;
using TraskioApi.DTOs;
using TraskioApi.Interfaces;
using TraskioApi.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TraskioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            var updated = await _userService.UpdateUserAsync(id, updateUserDTO);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
