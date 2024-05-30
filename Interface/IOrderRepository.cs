using BackEndStructuer.Interface;
using Gaz_BackEnd.Entities;

namespace Gaz_BackEnd.Interface
{
    public interface IOrderRepository : IGenericRepository<Order,Guid>
    {
        
    }
}