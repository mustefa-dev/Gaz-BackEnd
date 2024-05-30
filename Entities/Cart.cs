using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class Cart : BaseEntity<Guid>
    {
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public List<CartProduct>? CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}