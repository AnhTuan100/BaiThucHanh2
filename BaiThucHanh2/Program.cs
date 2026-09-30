using BaiThucHanh2.Middlewares;
using BaiThucHanh2.Services;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký Controllers và Service
builder.Services.AddControllers();
builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();

app.UseRouting();

// Middleware xác thực Token
app.UseMiddleware<JwtValidationMiddleware>();

app.UseAuthorization();
app.MapControllers();

app.Run();