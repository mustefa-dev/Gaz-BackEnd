using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class Address: BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? FullAddress { get; set; }
        public double? Latidute { get; set; }
        public double? Longitude { get; set; }
        public bool? IsMain { get; set; }
        public Guid? GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        public Guid? DistrictId { get; set; }
        public District? District { get; set; }
        public Guid? CityId { get; set; }
        public City? City { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

    }
}
