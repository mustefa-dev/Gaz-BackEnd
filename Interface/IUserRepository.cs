using System.Linq.Expressions;
using BackEndStructuer.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace BackEndStructuer.Interface
{
    public interface IUserRepository : IGenericRepository<AppUser,Guid>
    {
        Task<AppUser> CreateUser(AppUser user);
        
        Task<(List<AppUser> data, int totalCount)> GetUsers(
        int pageNumber = 0
        );

     
        Task<(List<AppUser> data, int totalCount)> GetUsers(Expression<Func<AppUser, bool>> predicate,
        Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include,
        int pageNumber = 0, int pageSize = 10
        );
        
        Task<AppUser> UpdateUser(AppUser user);
        Task<AppUser> DeleteUser(AppUser user);
    }
}