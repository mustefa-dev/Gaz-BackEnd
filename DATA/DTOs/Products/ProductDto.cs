using BackEndStructuer.DATA.DTOs.Files;
using Gaz_BackEnd.DATA.DTOs.BaseDto;
using File = Gaz_BackEnd.Entities.File;
using Type = Gaz_BackEnd.Entities.Type;

namespace Gaz_BackEnd.DATA.DTOs.Products;

public class ProductDto : BaseDto<Guid>{
    public string Name { get; set; }
    public int Type { get; set; }
    public int Price { get; set; }
    public string Path { get; set; }

    public Guid FileId { get; set; }
    public string Description { get; set; }
    public string Weight { get; set; }
}