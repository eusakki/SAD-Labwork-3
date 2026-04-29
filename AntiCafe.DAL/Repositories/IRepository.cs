using System.Linq.Expressions;

namespace AntiCafe.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
