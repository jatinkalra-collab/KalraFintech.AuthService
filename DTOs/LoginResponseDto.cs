public class LoginResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Role { get; set; }
    public int? OrgId { get; set; }
    public string Token { get; set; } // JWT
}


