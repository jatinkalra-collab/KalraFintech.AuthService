using BCrypt.Net;
using KalraFintech.AuthService.DTOs;
using KalraFintech.AuthService.Models;
using KalraFintech.AuthService.Repositories;
using KalraFintech.AuthService.Utils;

namespace KalraFintech.AuthService.Services
{
    public class AuthenticationService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtUtils _jwtUtils;


        public AuthenticationService(UserRepository userRepository, JwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        // Register a new user
        public async Task<User> RegisterAsync(string username, string password, Role role, int? orgId = null)
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

            return await _userRepository.CreateUserAsync(user);
        }

        // Validate login
        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null; // Invalid credentials

            // Generate JWT
            var token = _jwtUtils.GenerateToken(user.Id, user.Username, user.Role);

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
