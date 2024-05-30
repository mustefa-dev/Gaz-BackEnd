using BackEndStructuer.DATA.DTOs;
using Type = Gaz_BackEnd.Entities.Type;

namespace Gaz_BackEnd.DATA.DTOs.Products
{
    public class ProductFilter:BaseFilter
    {
        public string? Name { get; set; }
        public Type? Type { get; set; }
    }
}