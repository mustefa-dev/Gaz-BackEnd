using BackEndStructuer.DATA;
using BackEndStructuer.Interface;
using BackEndStructuer.Entities;
using Gaz_BackEnd.DATA;

namespace BackEndStructuer.Repository
{
    public class ArticleRepository : GenericRepository<Article,int>, IArticleRespository
    {
        
        private readonly DataContext _context;

        public ArticleRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}