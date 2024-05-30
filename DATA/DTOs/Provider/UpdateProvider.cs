namespace Gaz_BackEnd.DATA.DTOs.Provider
{
    public class UpdateProvider
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public Guid? stationId { get; set; }
        public ICollection<Guid>? FileID { get; set; }
    }
}
