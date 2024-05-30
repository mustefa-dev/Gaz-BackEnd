using BackEndStructuer.DATA.DTOs.User;
using Gaz_BackEnd.DATA.DTOs.Address;
using Gaz_BackEnd.DATA.DTOs.BaseDto;
using Gaz_BackEnd.DATA.DTOs.Provider;

namespace Gaz_BackEnd.DATA.DTOs.Order
{
    public class OrderDto : BaseDto<Guid>
    {
        public string? OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public string? Note { get; set; }
        public AddressDTO? Address { get; set; }
        public List<OrderProductDto>? OrderProducts { get; set; }
        public UserDto? Client { get; set; }
        public double? Rating { get; set; }
        public DateTime? DateOfAccepted { get; set; }
        public DateTime? DateOfCanceled { get; set; }
        public DateTime? DateOfDelivered { get; set; }
        public ProviderDTO? Provider { get; set; }
    }
}