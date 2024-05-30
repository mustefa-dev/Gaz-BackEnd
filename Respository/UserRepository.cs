using System.Linq.Expressions;
using BackEndStructuer.DATA;
using BackEndStructuer.Entities;
using BackEndStructuer.Interface;
using BackEndStructuer.Utils;
using Gaz_BackEnd.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BackEndStructuer.Repository
{
    public class UserRepository : GenericRepository<AppUser,Guid>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;

        }
        public async Task<AppUser> CreateUser(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<(List<AppUser> data, int totalCount)> GetUsers(int _pageNumber)
        {
            var users = await _context.Users.Skip((_pageNumber - 1) * 10).Take(10).ToListAsync();
            var count = await _context.Users.CountAsync();
            return (users,  count);
        }
        public async Task<(List<AppUser> data, int totalCount)> GetUsers(Expression<Func<AppUser, bool>> predicate, Func<IQueryable<AppUser>, IIncludableQueryable<AppUser, object>> include, int pageNumber = 0,int pageSize=10)
        {
            var query = predicate == null
            ? _dbContext.Set<AppUser>()
            : _dbContext.Set<AppUser>()
            .Where(predicate);
            query = include != null ? include(query) : query;
            query = query.OrderByDescending(model => model.CreationDate);
            return (await (pageNumber == 0
            ? query.ToListAsync()
            : query.Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync()),
            await query.CountAsync());
        }
        public async Task<AppUser> UpdateUser(AppUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<AppUser> DeleteUser(AppUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}