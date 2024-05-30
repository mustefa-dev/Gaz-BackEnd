using BackEndStructuer.DATA.DTOs;

namespace Gaz_BackEnd.DATA.DTOs.Provider
{
    public class ProviderFilter: BaseFilter
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public Guid? stationId { get; set; }
    }
}
