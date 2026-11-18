using CIt_Demo_Project.DTO;

namespace CIt_Demo_Project.Interface
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
