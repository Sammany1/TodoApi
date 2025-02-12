using System.Threading.Tasks;
using TraskioApi.DTOs;

namespace TraskioApi.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDTO);
        Task<AuthResponseDTO?> RegisterAsync(CreateUserDTO registerDTO);
    }
}
