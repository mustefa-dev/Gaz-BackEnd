using Gaz_BackEnd.Entities;
using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.Distric
{
    public class DistrictForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid GovernorateId { get; set; }
    }
}
