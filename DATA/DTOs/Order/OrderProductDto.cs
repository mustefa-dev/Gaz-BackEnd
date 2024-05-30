using Gaz_BackEnd.DATA.DTOs.Products;
using File = Gaz_BackEnd.Entities.File;

namespace Gaz_BackEnd.DATA.DTOs.Order
{
    public class OrderProductDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string? Image { get; set; }
        
    }
    
   
}