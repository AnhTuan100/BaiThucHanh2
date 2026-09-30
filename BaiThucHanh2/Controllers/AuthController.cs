using BaiThucHanh2.DTOs;
using BaiThucHanh2.Models;
using BaiThucHanh2.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaiThucHanh2.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;
    
    private static readonly List<User> UserDatabase = new()
    {
        new User { IdUser = 1, UserName = "admin", Password = "e10adc3949ba59abbe56e057f20f883e" } 
    };

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("")]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = UserDatabase.FirstOrDefault(u =>
            u.UserName.Equals(request.UserName, StringComparison.OrdinalIgnoreCase) &&
            u.Password.Equals(request.Password));

        if (user == null)
        {
            return Unauthorized(new { Success = false, Message = "Sai username hoac password" });
        }

        var token = _jwtService.GenerateToken(user);
        user.Token = token;

        return Ok(new
        {
            Success = true,
            Message = "Dang nhap thanh cong!",
            Token = token
        });
    }

    [HttpGet("auth")]
    public IActionResult VerifyAuth()
    {
        var userName = User.Identity?.Name 
                       ?? User.Claims.FirstOrDefault(c => c.Type == "UserName" || c.Type == "sub")?.Value;
        var idUser = User.Claims.FirstOrDefault(c => c.Type == "IdUser")?.Value;

        return Ok(new
        {
            Status = "Xac thuc thanh cong",
            IdUser = idUser,
            UserName = userName
        });
    }
}