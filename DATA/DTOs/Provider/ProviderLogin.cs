namespace Gaz_BackEnd.DATA.DTOs.Provider
{
    public class ProviderLogin
    {
        public string PhoneNumber { get; set; }
        public int? OtpCode { get; set; }
        public string? Key { get; set; }
    }
}
