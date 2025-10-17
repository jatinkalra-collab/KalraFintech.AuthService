﻿namespace KalraFintech.AuthService.DTOs
{
    public class RegisterRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "OrgAdmin"; // default OrgAdmin
        public int? OrgId { get; set; }
    }
}
