using KantarStore.Domain.Entities;
using KantarStore.Domain.Repositories;
using KantarStore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Repositories
{
    internal class ProductsRepository(KantarStoreDBContext dBContext) 
        : IProductsRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await dBContext.Products
                .AsNoTracking()
                .Include(p => p.Voucher)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await dBContext.Products
            .AsNoTracking()                  
            .Include(p => p.Voucher)       
            .SingleOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<Guid> AddNewProduct(Product product)
        {
            await dBContext.Products.AddAsync(product);
            await dBContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
