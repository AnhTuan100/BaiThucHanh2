using Microsoft.AspNetCore.Mvc;

namespace BaiThucHanh2.Controllers;

[ApiController]
[Route("api/[controller]")]
[Route("hello")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Message = "Hello World",
            AccessedBy = User.Identity?.Name ?? User.Claims.FirstOrDefault(c => c.Type == "UserName" || c.Type == "sub" || c.Type == System.Security.Claims.ClaimTypes.Name)?.Value
        });
    }
}