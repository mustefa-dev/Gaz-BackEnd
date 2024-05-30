using Gaz_BackEnd.DATA.DTOs.BaseDto;
using Gaz_BackEnd.DATA.DTOs.Order;

namespace Gaz_BackEnd.DATA.DTOs.cart
{
    public class CartDto : BaseDto<Guid>
    {
        public List<CartProductDto> CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}