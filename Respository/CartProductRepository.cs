using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class CartProductRepository : GenericRepository<CartProduct,Guid>,ICartProduct
    {
        private readonly DataContext _context;

        public CartProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}