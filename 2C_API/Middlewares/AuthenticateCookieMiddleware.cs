using _2C_API.Data;
using Microsoft.EntityFrameworkCore;

namespace _2C_API.Middlewares
{
    public class AuthenticateCookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public AuthenticateCookieMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!IsLoginRequest(context.Request))
            {
                // Retrieve the value of the cookie named "LoginCookie"
                var userId = context.Request.Cookies["LoginCookie"];

                // Resolve the DatabaseContext from the IServiceProvider
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                    // Now you can perform database queries using the dbContext
                    var isUserExists = await dbContext.ApplicationUsers
                        .Where(x => x.Id == userId && !x.IsDeleted)
                        .AnyAsync();

                    if (!isUserExists)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorized: Invalid or expired cookie.");
                        return;
                    }
                }
            }

            await _next(context);
        }

        private bool IsLoginRequest(HttpRequest request)
        {
            return request.Path.StartsWithSegments("/api/Auth/login", StringComparison.OrdinalIgnoreCase);
        }

    }
}
