using CompanyManagementSystem.API.DTO;
using CompanyManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.API.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
            var response = await _authService.AuthenticateAsync(loginRequestDto);

            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}