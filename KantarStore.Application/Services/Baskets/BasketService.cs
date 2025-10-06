using AutoMapper;
using KantarStore.Application.Dtos;
using KantarStore.Domain.Entities;
using KantarStore.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace KantarStore.Application.Services.Baskets
{
    public class BasketService(
        IBasketRepository basketRepository,
        IProductsRepository productRepository,
        ILogger<BasketService> logger,
        IMapper mapper) : IBasketService
    {
        public async Task<Basket> AddToBasket(Guid userId,Guid productId,int quantity)
        {    
            // 1. Get current basket for user
            var basket = await basketRepository.GetUserBasket(userId);

            // 2. Get new product
            var prod = await productRepository.GetByIdAsync(productId);

            // 3. Add or update the item
            basket.AddItems(prod, quantity);

            // 4. Recalculate totals
            basket.RecalculateTotals();

            // 5. Persist changes
            await basketRepository.UpdateAsync(basket);

            // 6. Return updated basket 
            return basket;
        }

        public async Task<bool> Checkout(Guid userId)
        {
            var val = await basketRepository.Checkout(userId);
            //var basketDto = mapper.Map<BasketDto>(basket);
            return val;
        }

        public async Task<Basket> GetUserBasket(Guid userId)
        {
            logger.LogInformation("Getting user basket history.");

            var basket = await basketRepository.GetUserBasket(userId);
            //var basketDto = mapper.Map<BasketDto>(basket);
            return basket;
        }

        public async Task<IEnumerable<Basket>> GetUserBasketHistory(Guid userId)
        {
            logger.LogInformation("Getting user basket history.");

            var basketHistory = await basketRepository.GetUserBasketHistory(userId);
            //var basketHistoryDtos = mapper.Map<IEnumerable<BasketDto>>(basketHistory);
            //return basketHistoryDtos;
            return basketHistory;
        }
        public async Task<Basket> RemoveFromBasket(Guid userId, Guid productId)
        {
            // 1. Get current basket for user
            var basket = await basketRepository.GetUserBasket(userId);

            // 2. Get new product
            var prod = await productRepository.GetByIdAsync(productId);

            // 3. Add or update the item
            basket.RemoveItems(prod);

            // 4. Recalculate subtotal, discount, tax, total
            basket.RecalculateTotals();

            // 5. Persist changes
            await basketRepository.UpdateAsync(basket);

            // 6. Return updated basket 
            return basket;

        }
    }
}
