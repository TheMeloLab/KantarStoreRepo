using KantarStore.Domain.Entities;
using KantarStore.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Seeders
{
    internal class ProductSeeder(KantarStoreDBContext dBContext) : IProductSeeder
    {
        public async Task Seed()
        {
            if (await dBContext.Database.CanConnectAsync())
            {
                if (!dBContext.Products.Any())
                {
                    var products = GetProducts();

                    dBContext.Products.AddRange(products);

                    await dBContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product(Guid.NewGuid(), "Soup", "Amazing Vegetable Soup", 0.65m, 10));
            products.Add(new Product(Guid.NewGuid(), "Bread", "Bread with gluten", 0.80m, 100));
            products.Add(new Product(Guid.NewGuid(), "Milk", "Milk from the mountains", 1.30m, 50));
            products.Add(new Product(Guid.NewGuid(), "Apples", "Aplle Bag", 1.00m, 30));

            return products;
        }
    }
}
