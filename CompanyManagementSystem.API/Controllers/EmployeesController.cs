using CompanyManagementSystem.API.DTO;
using CompanyManagementSystem.API.Models;
using CompanyManagementSystem.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.API.Controllers;

[ApiController]
[Route("api/employee")]
public class EmployeesController : Controller
{
    private readonly EmployeeRepository _repository;
    
    public EmployeesController(EmployeeRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetEmployees()
    {
        var employees = await _repository.GetEmployeesAsync();
        var responseDto = employees.Select(e => new EmployeeResponseDto
        {
            Id = e.Id,
            Name = e.Name,
            Surname = e.Surname,
            Position = e.Position,
            Email = e.Email,
            Salary = e.Salary
        });
        
        return Ok(responseDto);
    }
    
    [HttpPost("add")]
    public async Task<ActionResult<EmployeeResponseDto>> AddEmployee([FromBody] EmployeeRequestDto? employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee is null.");
        }

        var employeeEntity = new EmployeeEntity
        {
            Name = employee.Name,
            Surname = employee.Surname,
            Position = employee.Position,
            Email = employee.Email,
            Salary = employee.Salary
        };
            
        var createdEmployee = await _repository.AddEmployeeAsync(employeeEntity);
        if (createdEmployee != null)
        {
            var responseDto = new EmployeeResponseDto
            {
                Id = createdEmployee.Id,
                Name = createdEmployee.Name,
                Surname = createdEmployee.Surname,
                Position = createdEmployee.Position,
                Email = createdEmployee.Email,
                Salary = createdEmployee.Salary
            };
            return CreatedAtAction(nameof(GetEmployeeById), new { id = responseDto.Id }, createdEmployee);
        } 
        return BadRequest("Employee not created.");
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> SearchEmployee([FromQuery] SearchEmployeeRequest searchRequest)
    {
        var employees = await _repository.SearchEmployeeAsync(searchRequest.SearchBy, searchRequest.SearchTerm);
        
        var responseDto = employees.Select(e => new EmployeeResponseDto
        {
            Id = e.Id,
            Name = e.Name,
            Surname = e.Surname,
            Position = e.Position,
            Email = e.Email,
            Salary = e.Salary
        });
        
        return Ok(responseDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeEntity>> GetEmployeeById(int id)
    {
        var employees = await _repository.SearchEmployeeAsync(EmployeeSearchField.Id, id.ToString());
        var employee = employees.FirstOrDefault();
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteEmployee(int id)
    {
        var deleted = await _repository.DeleteEmployeeAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}