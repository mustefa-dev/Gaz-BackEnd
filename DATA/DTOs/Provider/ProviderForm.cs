using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.Provider
{
    public class ProviderForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        [Required]
        public Guid stationId { get; set; }
        public ICollection<Guid>? FileID { get; set; }
    }
}
