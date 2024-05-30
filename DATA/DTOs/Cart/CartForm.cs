using Gaz_BackEnd.DATA.DTOs.Order;

namespace Gaz_BackEnd.DATA.DTOs.cart
{
    public class CartForm
    {
        public List<OrderProductFormDto> CartProducts { get; set; } = new List<OrderProductFormDto>();
    }
}