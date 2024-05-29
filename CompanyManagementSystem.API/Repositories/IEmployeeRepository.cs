using CompanyManagementSystem.API.Models;

namespace CompanyManagementSystem.API.Repositories;

public interface IEmployeeRepository
{
    public Task<List<EmployeeEntity>> GetEmployeesAsync();
    public Task<EmployeeEntity?> AddEmployeeAsync(EmployeeEntity employee);
    
    public Task<IEnumerable<EmployeeEntity?>> SearchEmployeeAsync(
        EmployeeSearchField? searchBy,
        string searchTerm
        );
    
    public Task<EmployeeEntity?> UpdateEmployeeAsync(EmployeeEntity employee);
}