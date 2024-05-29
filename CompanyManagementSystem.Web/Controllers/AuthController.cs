using System.Diagnostics;
using CompanyManagementSystem.Web.DTO;
using CompanyManagementSystem.Web.Models;
using CompanyManagementSystem.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagementSystem.Web.Controllers;

public class AuthController(HttpService http) : Controller
{
    public IActionResult Signup()
    {
        return View(new SignupViewModel());
    }

    public IActionResult PostData(SignupViewModel model)
    {
        
        try 
        {
            var response = http.PostCredentialsAsync(new LoginRequestDto()
            {
                Username = model.Username, 
                Password = model.Password
            });

            if (response.Result.Token != null)
            {
                var token = response.Result.Token;
                
                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });
                
                return RedirectToAction("Dashboard", "Main");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials.");
            }
        }
        
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, "Credentials are invalid");
        }

        return View("Signup", model);
    }

    
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}