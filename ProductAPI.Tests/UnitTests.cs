using Microsoft.EntityFrameworkCore;
using ProductAPI.Core;
using ProductAPI.Infrastructure.Context;
using ProductAPI.Infrastructure.Repositories;
using System;
using Xunit;

namespace ProductAPI.Tests
{
    public class UnitTests
    {
        private ProductContext productContext;

        public UnitTests()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(databaseName: "productApi_db").Options;

            productContext = new ProductContext(options);

            productContext.Products.Add(new Product()
            {
                Name = "Macbook Air M1 - 8GB - 256",
                Description = "Apple Macbook with M1 chip - 256 GB storage - 8GB unified memory"
            });

            productContext.SaveChanges();
        }
        [Fact]
        public async void GetAllProduct()
        {
            var productRepository = new ProductRepository(productContext);
            var products = await productRepository.GetAllAsync();
            Assert.True(products.Count > 0);
        }

        [Fact]
        public async void GetProductById()
        {
            var productRepository = new ProductRepository(productContext);
            var products = await productRepository.GetByIdAsync(1);
            Assert.Equal(1,products.Id);
        }


        [Fact]
        public async void AddProduct()
        {
            var productRepository = new ProductRepository(productContext);
            var product = new Product()
            {
                Name = "Test Product",
                Description = "Product for testing purpose",
                Price = 25.6M
            };
             await productRepository.AddAsync(product);
            var productList = await productRepository.GetAllAsync();
            Assert.NotNull(productList.Find(x => x.Name == "Test Product"));
        }

        [Fact]
        public async void RemoveProduct()
        {
            var productRepository = new ProductRepository(productContext);
            var product = new Product()
            {
                Name = "Test Product",
                Description = "Product for testing purpose",
                Price = 25.6M
            };
            await productRepository.AddAsync(product);

            await productRepository.DeleteAsync(product);

            var productList = await productRepository.GetAllAsync();
            Assert.Null(productList.Find(x => x.Name == "Test Product"));
        }

        [Fact]
        public async void RemoveUnknownProduct()
        {
            try
            {
                var productRepository = new ProductRepository(productContext);
                var product = new Product()
                {
                    Name = "Unknown Product",
                    Description = "Product for testing purpose",
                    Price = 25.6M
                };

                await productRepository.DeleteAsync(product);
            }
            catch(Exception ex)
            {
                     Assert.Equal("Attempted to update or delete an entity that does not exist in the store.",ex.Message);
            }
        }


        [Fact]
        public async void Invalid_AddDoubliplicateProduct()
        {
            var productRepository = new ProductRepository(productContext);
            var product1 = new Product()
            {
                Id = 10,
                Name = "Test duplicated product",
                Description = "Product for testing purpose",
                Price = 25.6M
            };
            var product2 = new Product()
            {
                Id = 10,
                Name = "Test Product",
                Description = "Product for testing purpose",
                Price = 25.6M
            };
            var product3 = new Product()
            {
                Id = 10,
                Name = "Test Product",
                Description = "Product for testing purpose",
                Price = 25.6M
            };
            await productRepository.AddAsync(product1);
            await productRepository.AddAsync(product2);
            await productRepository.AddAsync(product3);
            var products = await productRepository.GetAllAsync();

            Assert.Single(products.FindAll(x => x.Id == 10));
        }
    }
}
