using BackEndStructuer.DATA.DTOs;
using Gaz_BackEnd.Entities;

namespace Gaz_BackEnd.DATA.DTOs.Order
{
    public class OrderFilters : BaseFilter
    {
       public List<OrderStatus>? OrderStatuses { get; set; }
    }
}