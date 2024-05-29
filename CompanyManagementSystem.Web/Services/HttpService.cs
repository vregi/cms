using System.Text;
using System.Text.Json;
using CompanyManagementSystem.Web.DTO;

namespace CompanyManagementSystem.Web.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
     public async Task<T?> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<LoginResponseDto?> PostCredentialsAsync(LoginRequestDto loginRequestDto)
    {
        var response = await _httpClient.PostAsync( "api/auth", 
            new StringContent(JsonSerializer.Serialize(loginRequestDto), 
                Encoding.UTF8,
                "application/json"));
        
        response.EnsureSuccessStatusCode();
        var loginResponseDto = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        
        return loginResponseDto;
    }

    public async Task<EmployeeResponseDto?> PostEmployeeAsync(EmployeeRequestDto employeeRequestDto)
    {  
        var response = await _httpClient.PostAsync("api/employee/add",
            new StringContent(JsonSerializer.Serialize(employeeRequestDto),
                Encoding.UTF8,
                "application/json"));
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return !string.IsNullOrEmpty(responseContent)
            ? JsonSerializer.Deserialize<EmployeeResponseDto>(responseContent)
            : null;
    }

    public async Task<IEnumerable<EmployeeResponseDto>?> GetEmployeesAsync()
    {
        var response = await _httpClient.GetAsync("api/employee");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeResponseDto>>();
    }

    public async Task<IEnumerable<EmployeeResponseDto>?> SearchEmployeesAsync(string searchBy, string searchTerm)
    {
        var response = await _httpClient.GetAsync($"api/employee/search?searchBy={searchBy}&searchTerm={searchTerm}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeResponseDto>>();
    }

    public async Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/employee/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmployeeResponseDto>();
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/employee/delete/{id}");
        return response.IsSuccessStatusCode;
    }
}