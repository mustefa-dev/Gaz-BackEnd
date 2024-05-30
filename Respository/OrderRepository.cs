using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class OrderRepository : GenericRepository<Order,Guid>,IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}