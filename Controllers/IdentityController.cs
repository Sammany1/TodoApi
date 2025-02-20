using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoApi.DTOs;
using TodoApi.Interfaces;
using TodoApi.Utils;
using Microsoft.Extensions.Logging;

namespace TraskioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(IUserService userService, ILogger<IdentityController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userService.GetFullUserByEmailAsync(loginDTO.Email);
            if (user == null || !PasswordHasher.VerifyPassword(loginDTO.Password, user.Password))
            {
                return Unauthorized();
            }

            var token = GenerateToken(new UserItemDTO(user));
            return Ok(new AuthResponseDTO 
            { 
                Token = token,
                User = new UserItemDTO(user)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDTO registerDTO)
        {
            var existingUser = await _userService.GetUserByEmailAsync(registerDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var user = await _userService.CreateUserAsync(registerDTO);
            var token = GenerateToken(user);
            
            return Ok(new AuthResponseDTO 
            { 
                Token = token,
                User = user 
            });
        }

        private string GenerateToken(UserItemDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Environment.GetEnvironmentVariable("JWT_KEY") ?? 
                throw new InvalidOperationException("JWT_KEY not found");
        
            // Fix the logging to use the same key source
            _logger.LogInformation("JWT_KEY used for generating token: {JWT_KEY}", key);
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}