using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.API.Models;

public class EmployeeEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Position { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public decimal Salary { get; set; }
}