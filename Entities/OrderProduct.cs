using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class OrderProduct : BaseEntity<Guid>
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}