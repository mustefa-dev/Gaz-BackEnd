using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class OtpRepository : GenericRepository<Otp,Guid>,IOtpRepository
    {
        private readonly DataContext _context;

        public OtpRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
