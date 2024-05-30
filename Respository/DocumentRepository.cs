using BackEndStructuer.DATA;
using BackEndStructuer.Repository;
using Gaz_BackEnd.Entities;
using Gaz_BackEnd.Interface;

namespace Gaz_BackEnd.Respository
{
    public class DocumentRepository:GenericRepository<Document,Guid>, IDocumentRepository
    {

        private readonly DataContext _context;

        public DocumentRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
