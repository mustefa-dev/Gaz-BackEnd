using BackEndStructuer.DATA.DTOs;

namespace Gaz_BackEnd.DATA.DTOs.User
{
    public class UserFilter:BaseFilter
    {
        public string? Name { get; set; }
        public string? Role { get; set; }
    }
}
