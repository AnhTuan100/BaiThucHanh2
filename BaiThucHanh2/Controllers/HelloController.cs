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
            AccessedBy = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value
        });
    }
}