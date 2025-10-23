using BCrypt.Net;
using KalraFintech.AuthService.DTOs;
using KalraFintech.AuthService.Models;
using KalraFintech.AuthService.Repositories;
using KalraFintech.AuthService.Utils;
using Microsoft.Extensions.Logging;

namespace KalraFintech.AuthService.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtUtils _jwtUtils;
        private readonly ILogger<AuthenticationService> _logger;


        public AuthenticationService(UserRepository userRepository, JwtUtils jwtUtils, ILogger<AuthenticationService> logger)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _logger = logger;
        }

        // Register a new user
        public async Task<User> RegisterAsync(string username, string password, Role role, int? orgId = null)
        {
            _logger.LogInformation("Starting registration for user: {Username}, Role: {Role}", username, role);

            try
            {
                // Hash password
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var user = new User
                {
                    Username = username,
                    PasswordHash = hashedPassword,
                    Role = role.ToString(),
                    OrgId = orgId
                };

                var createdUser = await _userRepository.CreateUserAsync(user);

                _logger.LogInformation("User {Username} successfully created with ID: {UserId}", username, createdUser.Id);

                return createdUser;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user: {Username}", username);
                throw; // rethrow so controller can handle it
            }

        }

        // Validate login
        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            _logger.LogInformation("Checking credentials for {Username}", username);

            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                _logger.LogWarning("User not found OR Invalid password : {Username}", username);
                return null; // Invalid credentials
            }
                

            // Generate JWT
            var token = _jwtUtils.GenerateToken(user.Id, user.Username, user.Role);
            _logger.LogInformation("JWT generated successfully for {Username}", username);

            return new LoginResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                OrgId = user.OrgId,
                Token = token
            };
        }

        private bool VerifyPassword(string plainPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, passwordHash);
        }

    }
}
