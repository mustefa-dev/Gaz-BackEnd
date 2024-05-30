using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities
{
    public class CartProduct : BaseEntity<Guid>
    {
        public Guid CartId { get; set; }
        public Cart? Cart { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
      
    }
}