using AutoMapper;
using BackEndStructuer.DATA;
using BackEndStructuer.DATA.DTOs;
using BackEndStructuer.Entities;
using BackEndStructuer.Helpers.OneSignal;
using BackEndStructuer.Entities;
using BackEndStructuer.Repository;
using Gaz_BackEnd.DATA;
using Gaz_BackEnd.Entities;

namespace BackEndStructuer.Services
{
    
    public interface IArticleServices
    {
        
        Task<(Article? article, string? error)> add(ArticleForm articleForm);
        Task<(List<Article> articles,int? totalCount,string? error)> GetAll(int _pageNumber = 0);
        Task<(Article? article,string?error)> update(ArticleUpdate articleUpdate, int id);
        Task<(Article? article, string?)> Delete(int id);
        
    }
    public class ArticleService : IArticleServices
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ArticleService(IMapper mapper, IRepositoryWrapper repositoryWrapper,IConfiguration configuration,DataContext dataContext)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<(Article? article, string? error)> add(ArticleForm articleForm)
        {
            var article = _mapper.Map<Article>(articleForm);
            var result = await _repositoryWrapper.Article.Add(article);
            var notification = new Notifications
            {
                Title = articleForm.Title,
                Description = articleForm.Description
            };
            
            return result == null ? (null, "article could not add") : (article, null);
            
           
        }
        public async Task<(List<Article> articles, int? totalCount, string? error)> GetAll(int _pageNumber = 0)
        {
            var (articles, totalCount) = await _repositoryWrapper.Article.GetAll(_pageNumber);

            return (articles, totalCount, null);
        }
        public async Task<(Article? article, string? error)> update(ArticleUpdate articleUpdate, int id)
        {
            var article = await _repositoryWrapper.Article.GetById(id);
            if (article == null)
            {
                return (null, "article not found");
            }
            article = _mapper.Map(articleUpdate, article);
            var response = await _repositoryWrapper.Article.Update(article);
            return response == null ? (null, "article could not be updated") : (article, null);
        }
        public async Task<(Article? article, string?)> Delete(int id)
        {
            var article = await _repositoryWrapper.Article.GetById(id);
            if (article == null) return (null, "article not found");
            var result = await _repositoryWrapper.Article.Delete(id);
            return result == null ? (null, "article could not be deleted") : (result, null);
        }
    }
}