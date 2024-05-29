using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyManagementSystem.Web.Attributes;

public class CustomAuthorizeAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var hasToken = context.HttpContext.Request.Cookies.TryGetValue("AuthToken", out var token);

        if (!hasToken && string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToActionResult("Signup", "Auth", null);
        }
        
        base.OnActionExecuting(context);
    }
}