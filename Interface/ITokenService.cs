using BackEndStructuer.Entities;
using Gaz_BackEnd.DATA.DTOs;

namespace e_parliament.Interface
{
    public interface ITokenService
    {
        string CreateToken(TokenDTO user);
    }
}