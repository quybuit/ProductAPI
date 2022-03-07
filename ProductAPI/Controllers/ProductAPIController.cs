using Microsoft.AspNetCore.Mvc;
using ProductAPI.Core;
using ProductAPI.FakeDbConText;
using ProductAPI.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        IRepository<Product> _repository= new ProductRepository(ApplicationFakeDbContext.GetDbContext());
       
        [HttpGet]
        public Task<List<Product>> GetProducts()
        {
            return _repository.GetAllAsync();
        }
        [HttpGet("{productId}")]
        public Task<Product> GetProductId(int productId)
        {
            return _repository.GetByIdAsync(productId);
        }

        [HttpPost("{product}")]
        public Task<Product> AddProduct(Product product)
        {
            return _repository.AddAsync(product);
        }

        [HttpDelete]
        public Task DeleteProduct(int productId)
        {
            return _repository.DeleteAsync(productId);
        }
    }
}
