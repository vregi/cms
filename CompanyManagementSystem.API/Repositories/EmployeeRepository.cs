using CompanyManagementSystem.API.Data;
using CompanyManagementSystem.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagementSystem.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeEntity>> GetEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<EmployeeEntity?> AddEmployeeAsync([FromBody]EmployeeEntity employee)
    {
        var result = await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IEnumerable<EmployeeEntity?>> SearchEmployeeAsync(EmployeeSearchField? searchBy, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return await _context.Employees.ToListAsync();
        }

        IQueryable<EmployeeEntity?> employees = _context.Employees;

        switch (searchBy)
        {
            case EmployeeSearchField.Id:
                if (int.TryParse(searchTerm, out var id))
                {
                    employees = employees.Where(e => e != null && e.Id == id);
                }
                break;
            case EmployeeSearchField.Name:
                employees = employees.Where(e => e != null && e.Name.Contains(searchTerm));
                break;
            case EmployeeSearchField.Surname:
                employees = employees.Where(e => e != null && e.Surname.Contains(searchTerm));
                break;
            case EmployeeSearchField.Position:
                employees = employees.Where(e => e != null && e.Position.Contains(searchTerm));
                break;
            case EmployeeSearchField.Email:
                employees = employees.Where(e => e != null && e.Email.Contains(searchTerm));
                break;
            case EmployeeSearchField.Salary:
                if (decimal.TryParse(searchTerm, out decimal salary))
                {
                    employees = employees.Where(e => e != null && e.Salary.Scale == salary);
                }
                break;
            default:
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await _context.Employees.ToListAsync();
                }

                break;
        }

        return await employees.ToListAsync();
        }
    
    
    public async Task<EmployeeEntity?> UpdateEmployeeAsync([FromBody]EmployeeEntity employee)
    {
        var result = _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    
    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return false;
        }
        
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
}