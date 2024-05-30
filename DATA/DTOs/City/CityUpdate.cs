using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.City
{
    public class CityUpdate
    {
        public string? Name { get; set; }
        public Guid? DistrictId { get; set; }
    }
}
