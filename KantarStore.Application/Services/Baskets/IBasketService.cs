using KantarStore.Domain.Entities;

namespace KantarStore.Application.Baskets
{
    public interface IBasketService
    {
        Task<IEnumerable<Basket>> GetUserBasketHistory(Guid userId);
        Task<Basket> GetUserBasket(Guid userId);
        Task<Basket> AddToBasket(Guid id);
        Task<Basket> RemoveFromBasket(Guid id);
        Task<Basket> Checkout(Guid id);
    }
}