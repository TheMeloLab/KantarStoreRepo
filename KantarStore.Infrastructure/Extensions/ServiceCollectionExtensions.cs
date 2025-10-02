using KantarStore.Domain.Repositories;
using KantarStore.Infrastructure.Persistance;
using KantarStore.Infrastructure.Repositories;
using KantarStore.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions 
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var con = configuration.GetConnectionString("KantarStoreDb");
            services.AddDbContext<KantarStoreDBContext>(options => options.UseSqlServer(con));
            services.AddScoped<IProductSeeder, ProductSeeder>();
            services.AddScoped<IVoucherSeeder, VoucherSeeder>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }
    }
}
