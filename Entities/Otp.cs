using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class Otp : BaseEntity<Guid>
    {
        public string? PhoneNumber { get; set; }
        public int? OtpCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? Key { get; set; }
    }
}
