using BackEndStructuer.DATA.DTOs;

namespace Gaz_BackEnd.DATA.DTOs.City
{
    public class CityFilter:BaseFilter
    {
        public string? Name { get; set; }
        public Guid? DistrictId { get; set; }
    }
}
