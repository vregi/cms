using CompanyManagementSystem.API.DTO;

namespace CompanyManagementSystem.API.Services;

public interface IAuthService
{
    public Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest);
}
