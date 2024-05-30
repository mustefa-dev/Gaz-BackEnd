using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class AddressRepository:GenericRepository<Address,Guid>,IAddressRepository
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
