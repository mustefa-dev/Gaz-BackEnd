using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.Distric
{
    public class DistrictUpdate
    {
        public string? Name { get; set; }
        public Guid? GovernorateId { get; set; }
    }
}
