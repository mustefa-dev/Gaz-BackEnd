using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class ProviderRepository:GenericRepository<Provider,Guid>,IProviderRepository
    {

        private readonly DataContext _context;

        public ProviderRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
