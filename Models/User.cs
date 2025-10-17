using System.ComponentModel.DataAnnotations;

namespace KalraFintech.AuthService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; }
        public int? OrgId { get; set; }
    }
}
