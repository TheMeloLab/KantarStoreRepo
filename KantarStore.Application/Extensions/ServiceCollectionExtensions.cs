using KantarStore.Application.Products;
using KantarStore.Application.Services.Baskets;
using Microsoft.Extensions.DependencyInjection;

namespace KantarStore.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        }
    }
}
