using BackEndStructuer.Entities;

namespace Gaz_BackEnd.DATA.DTOs
{
    public class TokenDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public UserRole? Role { get; set; }
    }
}
