using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var userRole = context.Session.GetString("UserRole");

        if (context.Request.Path.StartsWithSegments("/admin"))
        {
            if (userRole != "admin")
            {
                context.Response.StatusCode = 403; // Forbidden
                await context.Response.WriteAsync("Access denied.");
                return;
            }
        }

        await _next(context);
    }
}
