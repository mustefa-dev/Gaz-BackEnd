using Gaz_BackEnd.DATA.DTOs.Distric;
using System.Security.Cryptography;

namespace Gaz_BackEnd.DATA.DTOs.Governorates
{
    public class GovernoratesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection< DistrictDTO>? Districts { get; set; }
    }
}
