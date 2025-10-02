using AutoMapper;
using KantarStore.Application.Dtos;
using KantarStore.Domain.Entities;
using KantarStore.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Products
{
    internal class ProductsService(IProductsRepository productsRepository,
        ILogger<ProductsService> logger,
        IMapper mapper) : IProductsService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            logger.LogInformation("Getting all the products list.");

            var products = await productsRepository.GetAllAsync();
            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDtos;
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            var product = await productsRepository.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
        public async Task<Guid> AddProduct(NewProductDto newProductDto)
        {
            var product = mapper.Map<Product>(newProductDto);
            var guid = await productsRepository.AddNewProduct(product);
            return product.Id;
        }
    }
}
