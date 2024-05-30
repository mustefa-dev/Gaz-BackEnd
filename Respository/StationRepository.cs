using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class StationRepository:GenericRepository<Station,Guid>,IStationRepository
    {

        private readonly DataContext _context;

        public StationRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
