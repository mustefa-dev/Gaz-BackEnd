using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class Provider: BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LicenseNumber { get; set; }
        public UserRole? Role { get; set; }
        public Guid? stationId { get; set; }
        public Station? Station { get; set; }
      
        public ICollection<Document>? Documents { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
