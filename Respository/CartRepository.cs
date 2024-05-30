using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class CartRepository : GenericRepository<Cart,Guid>,ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}