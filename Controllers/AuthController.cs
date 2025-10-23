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
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthenticationService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            _logger.LogInformation("Registration attempt for user: {Username} with role: {Role}", dto.Username, dto.Role);

            try
            {
                var role = dto.Role == "SuperAdmin" ? Role.SuperAdmin : Role.OrgAdmin;
                var user = await _authService.RegisterAsync(dto.Username, dto.Password, role, dto.OrgId);
                _logger.LogInformation("User {Username} registered successfully with ID: {UserId}", dto.Username, user.Id);
                return Ok(new { user.Id, user.Username, user.Role, user.OrgId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Username}", dto.Username);
                return StatusCode(500, "An error occurred while processing your request.");
            }


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            _logger.LogInformation("Login attempt for {Username}", dto.Username);

            var result = await _authService.LoginAsync(dto.Username, dto.Password);
            if (result == null)
            {
                _logger.LogWarning("Invalid login for {Username}", dto.Username);
                return Unauthorized(new { message = "Invalid username or password" });

            }

            _logger.LogInformation("User {Username} logged in successfully", dto.Username);
            return Ok(result);
        }

    }
}
