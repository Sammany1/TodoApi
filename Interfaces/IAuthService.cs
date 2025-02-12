using System.Threading.Tasks;
using TodoApi.DTOs;

namespace TodoApi.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDTO);
        Task<AuthResponseDTO?> RegisterAsync(CreateUserDTO registerDTO);
    }
}
