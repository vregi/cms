namespace CompanyManagementSystem.API.DTO;

public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
}