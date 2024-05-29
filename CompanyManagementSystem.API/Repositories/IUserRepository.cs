using CompanyManagementSystem.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.API.Repositories;

public interface IUserRepository
{
    public Task<UserEntity?> GetUserByUsernameAsync(string username);
    public Task<UserEntity?> RegisterUser(string username, string password, Role role);
}