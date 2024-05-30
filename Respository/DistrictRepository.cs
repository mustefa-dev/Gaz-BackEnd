using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class DistrictRepository:GenericRepository<District,Guid>,IDistrictRepository
    {
        private readonly DataContext _context;

        public DistrictRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
