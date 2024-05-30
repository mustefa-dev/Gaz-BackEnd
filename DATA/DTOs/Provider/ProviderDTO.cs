namespace Gaz_BackEnd.DATA.DTOs.Provider
{
    public class ProviderDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public Guid? stationId { get; set; }
        public string? StationName { get; set; }
        public string? Token { get; set; }
        public double? Rating { get; set; }
        public ICollection<DocunentDTO>? Documents { get; set; }
    }
    public class DocunentDTO
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
    }
    
}