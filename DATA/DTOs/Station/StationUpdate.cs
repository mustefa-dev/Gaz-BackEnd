namespace Gaz_BackEnd.DATA.DTOs.Station
{
    public class StationUpdate
    {
        public string? Name { get; set; }
        public int? ProductionRate { get; set; }
        public Guid? GovernorateId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? AppUserId { get; set; }
    }
}
