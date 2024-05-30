using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.User
{
    public class UserForm
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
        public string? Role { get; set; }
    }
}
