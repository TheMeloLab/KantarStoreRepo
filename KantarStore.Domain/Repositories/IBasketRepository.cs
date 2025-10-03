using KantarStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<IEnumerable<Basket>> GetUserBasketHistory(Guid userId);
        Task<Basket> GetUserBasket(Guid userId);
        Task<bool> Checkout(Guid id);
        Task<bool> UpdateAsync(Basket basket);
    }
}
