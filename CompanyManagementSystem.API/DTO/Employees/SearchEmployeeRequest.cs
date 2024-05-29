using CompanyManagementSystem.API.Models;

namespace CompanyManagementSystem.API.DTO;

public class SearchEmployeeRequest
{
    public EmployeeSearchField? SearchBy { get; set; }
    public string SearchTerm { get; set; }
}