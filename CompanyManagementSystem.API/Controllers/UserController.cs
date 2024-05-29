using CompanyManagementSystem.API.Models;
using CompanyManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.API.Controllers;
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserRepository _repository;
    
    public UserController(UserRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(string username, string password, Role role)
    {
        await _repository.RegisterUser(username, password, role);
        return Ok();
    }
    
    [HttpDelete("delete")]
    public async Task<ActionResult> Delete(string username)
    {
        await _repository.DeleteUser(username);
        return Ok();
    }

}