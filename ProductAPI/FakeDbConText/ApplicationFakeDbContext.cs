using Microsoft.EntityFrameworkCore;
using ProductAPI.Core;
using ProductAPI.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.FakeDbConText
{
    public static class ApplicationFakeDbContext
    {
        public static ProductContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .EnableSensitiveDataLogging(true)
                .UseInMemoryDatabase(databaseName: "productApi_db").Options;
            var context =  new ProductContext(options);

            context.Products.Add(new Product()
            {
                Name = "Macbook Air M1 - 8GB - 256",
                Description = "Apple Macbook with M1 chip - 256 GB storage - 8GB unified memory"
            });
            context.Products.Add(new Product()
            {
                Price = 1300.5M,
                Name = "SamSung Galaxy S20 Ultra 12GB - 128GB-W",
                Description = "SamSung Galaxy S20 Ultra 12GB - 128GB - White"
            });

            context.Products.Add(new Product()
            {
                Price = 2000.987M,
                Name = "Dell Latitude E5400 - 16GB - 512 - NoneOS",
                Description = "Laptop Dell Latitude E5400 - 16GB RAM - 512 Storage - None OS"
            });
            context.SaveChanges();
            return context;
        }
    }
}
