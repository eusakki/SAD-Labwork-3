using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AntiCafe.DAL.Data;

namespace AntiCafe.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AntiCafeDbContext context;
        protected readonly DbSet<T> dbSet;

        public Repository(AntiCafeDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
    }
}
