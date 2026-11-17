using Saveetha_Demo_Project.DTO;

namespace Saveetha_Demo_Project.Interface
{
    public interface IAuthInterface
    {
        Task<AuthResponseDto> RegistrationAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);

    }
}
