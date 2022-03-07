
using ProductAPI.Core;
using ProductAPI.Infrastructure.Context;

namespace ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        protected readonly ProductContext _productContext;
        public ProductRepository(ProductContext applicationContext):base(applicationContext)
        {
            _productContext = applicationContext;
        }
    }
}
