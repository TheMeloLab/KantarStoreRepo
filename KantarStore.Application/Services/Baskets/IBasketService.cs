using KantarStore.Application.Dtos;
using KantarStore.Domain.Entities;

namespace KantarStore.Application.Services.Baskets
{
    public interface IBasketService
    {
        Task<IEnumerable<Basket>> GetUserBasketHistory(Guid userId);
        Task<Basket> GetUserBasket(Guid userId);
        Task<Basket> AddToBasket(Guid userId, Guid productId, int quantity);
        Task<Basket> RemoveFromBasket(Guid userId, Guid productId, int quantity);
        Task<bool> Checkout(Guid userId);
    }
}