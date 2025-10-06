using KantarStore.Domain.Entities;
using KantarStore.Domain.Repositories;
using KantarStore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace KantarStore.Infrastructure.Repositories
{
    internal class BasketRepository(KantarStoreDBContext dBContext) 
        : IBasketRepository
    {

        public async Task<bool> Checkout(Guid userId)
        {
            var basket = await dBContext.Baskets
            .Include(b => b.BasketItems)
            .FirstOrDefaultAsync(b => b.User.Id == userId && b.Status == (int)Basket.BasketStatus.Open);

            basket.Status = 2;

            var res = await dBContext.SaveChangesAsync();
            return res != -1;
        }

        public async Task<Basket> GetUserBasket(Guid userId)
        {
            var basket = await dBContext.Baskets
             .Include (b => b.User)
             .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)            
                    .ThenInclude(p => p.Voucher)
             .FirstOrDefaultAsync(b => b.User.Id == userId && b.Status == (int)Basket.BasketStatus.Open);

            if(basket == null)
            {
                User u = await dBContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
                Basket newUserBasket = new Basket(Guid.NewGuid(),u ,1);
                await dBContext.Baskets.AddAsync(newUserBasket);
                await dBContext.SaveChangesAsync();
                return newUserBasket;
            }
            return basket;
        }

        public async Task<IEnumerable<Basket>> GetUserBasketHistory(Guid userId)
        {
            var baskets = await dBContext.Baskets
              .Where(b => b.User.Id == userId && b.Status == 2)
              .Include(b => b.BasketItems)
              .ToListAsync();

            return baskets;
        }

        public async Task<bool> UpdateAsync(Basket basket)
        {
            foreach (BasketItem item in basket.BasketItems)
            {
                dBContext.Entry(item.Product).State = EntityState.Unchanged;

                var existingItem = dBContext.BasketItems.FirstOrDefault(b => b.Id == item.Id);

                if (existingItem != null)
                {
                    existingItem.Price = item.Price;
                    existingItem.Quantity = item.Quantity;

                    if(existingItem.Quantity == 0)
                        dBContext.BasketItems.Remove(existingItem);
                }
                else
                {
                    await dBContext.BasketItems.AddAsync(item);
                }
            }
            var res = await dBContext.SaveChangesAsync();
            return res != -1;
        }
    }
}
