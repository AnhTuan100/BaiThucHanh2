using BaiThucHanh2.Services;

namespace BaiThucHanh2.Middlewares;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
    {
        var path = context.Request.Path.Value?.ToLower() ?? "";
        
        if (path.Equals("/auth") || path.Contains("/api/hello"))
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"401 Unauthorized: Thieu hoac sai dinh dang Bearer token\"}");
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            var principal = jwtService.ValidateToken(token);

            if (principal == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"401 Unauthorized: Token khong hop le hoac het han\"}");
                return;
            }

            context.User = principal;
        }

        await _next(context);
    }
}