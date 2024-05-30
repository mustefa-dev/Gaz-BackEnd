using Gaz_BackEnd.DATA.DTOs.City;
using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.Distric
{
    public class DistrictDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<CityDTO>? Cities { get; set; }

    }
}
