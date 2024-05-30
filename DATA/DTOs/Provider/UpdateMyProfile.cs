namespace Gaz_BackEnd.DATA.DTOs.Provider
{
    public class UpdateMyProfile
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public ICollection<Guid>? FileID { get; set; }
    }
}
