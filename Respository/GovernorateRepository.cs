using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class GovernoratesRepository:GenericRepository<Governorate,Guid>,IGovernoratesRepository
    {

        private readonly DataContext _context;

        public GovernoratesRepository(DataContext context) : base(context)
        {
            _context = context;
        }
}
}
