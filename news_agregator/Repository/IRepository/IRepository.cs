


using System.Linq.Expressions;

namespace IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T?> Get(Expression<Func<T, bool>>? filter = null);
        void Create(T entity);
        void Remove(T entity);
        Task Save();
    }
}

