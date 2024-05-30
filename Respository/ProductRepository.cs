using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository;

public class ProductRepository:GenericRepository<Product,Guid>,IProductRepository{
    private readonly DataContext _context;

    public ProductRepository(DataContext context) : base(context) {
        _context = context;
    }
    
}