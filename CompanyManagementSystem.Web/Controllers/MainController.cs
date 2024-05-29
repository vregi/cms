using CompanyManagementSystem.Web.Attributes;
using CompanyManagementSystem.Web.DTO;
using CompanyManagementSystem.Web.Models;
using CompanyManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Controllers;

[CustomAuthorize]
public class MainController : Controller
{
    private readonly HttpService _service;

    public MainController(HttpService service)
    {
        _service = service;
    }
    
    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Employees()
    {
        var employees = _service.GetEmployeesAsync().Result;
        
        return View(employees);
    }
    
    public async Task<IActionResult> AddEmployee(EmployeeRequestDto employee)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _service.PostEmployeeAsync(employee);
                return RedirectToAction("Employees", "Main"); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to add employee. Please try again.");
                return View("Employees/AddEmployee", employee);
            }
        }

        return View("Employees/AddEmployee", employee);
    }

    
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var result = await _service.DeleteEmployeeAsync(id);
            if (result)
            {
                return RedirectToAction("Employees");
            }
            else
            {
                return RedirectToAction("Employees");
            }
        }
        catch (Exception ex)
        {
            return RedirectToAction("Employees");
        }
    }

    
}