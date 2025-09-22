namespace Backend.Application.Middlewares;

public static class SessionAuthMiddlewareExtensions
{
    public static IApplicationBuilder UseSessionAuth(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SessionAuthMiddleware>();
    }
}