using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // Only authenticated users can access
    [HttpGet("protected")]
    [Authorize]
    public IActionResult Protected()
    {
        return Ok("You are authenticated!");
    }

    // Only SuperAdmin role can access
    [HttpGet("admin")]
    [Authorize(Roles = "SuperAdmin")]
    public IActionResult AdminOnly()
    {
        return Ok("You are a SuperAdmin!");
    }

    // Public endpoint
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("This is public.");
    }
}
