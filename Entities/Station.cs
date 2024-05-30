using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class Station: BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public int? ProductionRate { get; set; }
        public Guid? GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }
        public Guid? DistrictId { get; set; }
        public District? District { get; set; }
        public Guid? CityId { get; set; }
        public City? City { get; set; }
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<Provider>? Providers { get; set; }

    }
}
