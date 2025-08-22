using CaseStudy.Domian.Entites;
using System.Linq.Expressions;

namespace CaseStudy.Domian.Interfaces
{
    public interface IProductRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreatAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);

    }
}
