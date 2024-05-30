using BackEndStructuer.Entities;

namespace Gaz_BackEnd.Entities;

public class File: BaseEntity<Guid>{
     
    public string Path { get; set; }
    public Product? Product { get; set; }
    public ICollection<Document> Documents { get; set; }

}