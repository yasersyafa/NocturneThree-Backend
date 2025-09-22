using Backend.Application.Interfaces;

namespace Backend.Application.Middlewares;

public class SessionAuthMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, ISessionRepository sessionRepo)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();

        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader["Bearer ".Length..].Trim();

            var session = await sessionRepo.GetByTokenAsync(token);
            if (session != null)
            {
                // inject playerId ke HttpContext
                context.Items["PlayerId"] = session.PlayerId;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Invalid or expired session." });
                return;
            }
        }

        await _next(context);
    }
}