using CompanyManagementSystem.API.Models;

namespace CompanyManagementSystem.API.Services;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity user);
}