using Microsoft.EntityFrameworkCore;
using ProductAPI.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly ProductContext _applicationContext;
        public RepositoryBase(ProductContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public Task<T> AddAsync(T entity)
        {
            _applicationContext.Set<T>().AddAsync(entity);
            _applicationContext.SaveChanges();
            return Task.FromResult(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _applicationContext.Set<T>().Remove(entity);
            _applicationContext.SaveChanges();
            return Task.FromResult(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _applicationContext.Set<T>().FindAsync(id);
            _applicationContext.Remove(entity);
            _applicationContext.SaveChanges();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _applicationContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _applicationContext.Set<T>().FindAsync(id);
        }
    }
}
