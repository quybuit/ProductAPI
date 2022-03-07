using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Generic Repository to work with entities, another repository should be inherit this interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int ProductId);
    }
}
