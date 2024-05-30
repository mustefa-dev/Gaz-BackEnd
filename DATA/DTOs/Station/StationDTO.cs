namespace Gaz_BackEnd.DATA.DTOs.Station
{
    public class StationDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int? ProductionRate { get; set; }
        public Guid? GovernorateId { get; set; }
        public string? GovernorateName { get; set; }
        public Guid? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public Guid? CityId { get; set; }
        public string? CityName { get; set; }
        public Guid? AppUserId { get; set; }
        public string? UserName { get; set; }
    }
}
