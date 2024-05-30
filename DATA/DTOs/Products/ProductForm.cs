using File = Gaz_BackEnd.Entities.File;
using Type = Gaz_BackEnd.Entities.Type;

namespace Gaz_BackEnd.DATA.DTOs.Products;

public class ProductForm{
    public string Name { get; set; }
    public Type Type { get; set; }
    public int Price { get; set; }
    public Guid FileId { get; set; }
    public string Description { get; set; }
    public string Weight { get; set; }
}