using CompanyManagementSystem.Web.Services;

namespace CompanyManagementSystem.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        
        builder.Services.AddHttpClient<HttpService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7065");
            client.Timeout = TimeSpan.FromSeconds(10);
        });
        builder.Services.AddAuthentication("CookieAuth")
            .AddCookie("CookieAuth", options =>
            {
                options.Cookie.Name = "AuthToken";
                options.LoginPath = "/Auth/Signup";
                options.AccessDeniedPath = "/Auth/Signup";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });
        
        
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
    
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}