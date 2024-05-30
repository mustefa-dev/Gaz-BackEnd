using System.ComponentModel.DataAnnotations;

namespace Gaz_BackEnd.DATA.DTOs.Order
{
    public class OrderFormDto
    {
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        public string? Note { get; set; }
        public List<OrderProductFormDto> OrderProducts { get; set; } = new List<OrderProductFormDto>();
    }
    
    public class OrderProductFormDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]public int Quantity { get; set; } = 1;
    }
}