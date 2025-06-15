using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
            where T : BaseEntity
    {
        #region Contructor

        protected readonly IDbContextFactory<UserContext> _contextFactory;
        public BaseRepository(IDbContextFactory<UserContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }

        #endregion

        #region GetAsync

        public virtual async Task<T> GetAsync(Guid uid)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Set<T>().FindAsync(uid);
        }
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Set<T>().FirstOrDefaultAsync(predicate);
        }

        #endregion

        #region GetAllAsync
        public virtual async Task<ResponsePagination<T>> GetPartAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            var entities = await db.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync();
            int total = await db.Set<T>().CountAsync();
            int pageCount = total / take;
            pageCount += total % take == 0 ? 0 : 1;
            var response = new ResponsePagination<T>()
            {
                TotalEntity = total,
                CurrentPage = skip / take + 1,
                Data = entities,
                PageCount = pageCount
            };
            return response;
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Set<T>().ToListAsync();
        }
        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            return await db.Set<T>().Where(predicate).ToListAsync();
        }

        #endregion

        #region Create
        public virtual async Task<Guid> CreateAsync(T item)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            await db.Set<T>().AddAsync(item);
            await db.SaveChangesAsync();
            return item.Uid;
        }
        #endregion

        #region Update
        public virtual async Task UpdateAsync(T item)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            db.Set<T>().Update(item);
            await db.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public virtual async Task DeleteAsync(Guid uid)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            T item = await db.Set<T>().FindAsync(uid);
            db.Set<T>().Remove(item);
            await db.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            using var db = await _contextFactory.CreateDbContextAsync();
            List<T> items = await db.Set<T>().Where(predicate).ToListAsync();
            foreach (T item in items)
            {
                db.Set<T>().Remove(item);
            }
            await db.SaveChangesAsync();
        }
        #endregion
    }
}
