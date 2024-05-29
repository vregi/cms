using CompanyManagementSystem.Web.DTO;
using CompanyManagementSystem.Web.Models;

namespace CompanyManagementSystem.Web.Services;

public interface IHttpService
{
    public Task<T?> GetAsync<T>(string url);
    
    public Task<LoginResponseDto?> PostCredentialsAsync(LoginRequestDto loginRequestDto);
    public Task<EmployeeResponseDto?> PostEmployeeAsync(EmployeeRequestDto employeeRequestDto);
    public Task<IEnumerable<EmployeeResponseDto>?> GetEmployeesAsync();
}