using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using IRepository;
using Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Repository

{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context _db;
        internal DbSet<T> dbSet;
        public Repository(Context db)
        {
            _db = db;
            this.dbSet = db.Set<T>();
        }

        public async Task Create(T entity)
        {
            
            await dbSet.AddAsync(entity);
            
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

