using BackEndStructuer.Entities;

namespace BackEndStructuer.DATA.DTOs.User
{
    public class UpdateUserForm
    {
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        public string? Role { get; set; }

    }
}