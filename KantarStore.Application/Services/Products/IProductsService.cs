using KantarStore.Application.Dtos;
using KantarStore.Domain.Entities;

namespace KantarStore.Application.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetById(Guid id);
        Task<Guid> AddProduct(NewProductDto newProduct);
    }
}