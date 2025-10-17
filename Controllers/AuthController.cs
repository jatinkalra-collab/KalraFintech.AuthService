using KalraFintech.AuthService.DTOs;
using KalraFintech.AuthService.Models;
using KalraFintech.AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace KalraFintech.AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthController(AuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var role = dto.Role == "SuperAdmin" ? Role.SuperAdmin : Role.OrgAdmin;
            var user = await _authService.RegisterAsync(dto.Username, dto.Password, role, dto.OrgId);
            return Ok(new { user.Id, user.Username, user.Role, user.OrgId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto.Username, dto.Password);
            if (result == null)
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(result);
        }

    }
}
