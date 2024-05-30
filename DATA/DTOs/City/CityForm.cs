using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.City
{
    public class CityForm
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public Guid? DistrictId { get; set; }
    }
}
