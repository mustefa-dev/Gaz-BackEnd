namespace Gaz_BackEnd.DATA.DTOs.Products;

public  class ProductUpdateDto{
    public string? Name { get; set; }
    public int? Price { get; set; }
    public string? Description { get; set; }
    public Guid? FileId { get; set; }
    public string?Weight { get; set; }
}