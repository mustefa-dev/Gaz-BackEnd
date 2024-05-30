using Gaz_BackEnd.Entities;

namespace Gaz_BackEnd.DATA.DTOs.Address
{
    public class AddressForm
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public double? Latidute { get; set; }
        public double? Longitude { get; set; }
        public bool? IsMain { get; set; }
        public Guid GovernorateId { get; set; }
        public Guid DistrictId { get; set; }
        public Guid CityId { get; set; }
    }
}
