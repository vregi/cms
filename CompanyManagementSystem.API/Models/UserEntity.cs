using System.ComponentModel.DataAnnotations;

namespace CompanyManagementSystem.API.Models;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public Role Role { get; set; }
}