using BackEndStructuer.Entities;
using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.User
{
    public class RegisterForm
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string? Password { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string? Name { get; set; }

        public int? OtpCode { get; set; }
        public string? Key { get; set; }
    }
}