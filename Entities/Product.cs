using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities;

public class Product : BaseEntity<Guid>{
    public string? Name { get; set; }
    public Type? Type { get; set; }
    public int? Price { get; set; }
    public File? File { get; set; }

    public Guid? FileId { get; set; }
    public string? Description { get; set; }
    public string? Weight { get; set; }
}

public enum Type{
    Cylinders = 1,
    Accessories = 2,
    FireExtinguisher = 3,
}