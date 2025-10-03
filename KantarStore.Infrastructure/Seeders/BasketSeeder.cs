using KantarStore.Domain.Entities;
using KantarStore.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Seeders
{
    internal class BasketSeeder(KantarStoreDBContext dBContext) : IBasketSeeder
    {
        public async Task Seed()
        {
            if (await dBContext.Database.CanConnectAsync())
            {
                if (!dBContext.Baskets.Any())
                {
                    var basket = GetBaskets();
                    dBContext.Baskets.AddRange(basket);
                    await dBContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Basket> GetBaskets()
        {
            List<Basket> baskets = new List<Basket>();

            var user1 = dBContext.Users.FirstOrDefault(item => item.Email.ToLower() == "pedrolopesmelo@gmail.com");

            Basket basket1 = new Basket(Guid.NewGuid(), user1, 2);
            basket1.Status = 2;
            basket1.BasketTotal = 2.10m;

            var bread = dBContext.Products.FirstOrDefault(item => item.Name == "Bread");
            var milk = dBContext.Products.FirstOrDefault(item => item.Name == "Milk");

            BasketItem item1 = new BasketItem(Guid.NewGuid(), basket1.Id, bread,1, 0.80m);
            BasketItem item2 = new BasketItem(Guid.NewGuid(), basket1.Id, milk, 1, 1.30m);

            basket1.BasketItems.Add(item1);
            basket1.BasketItems.Add(item2);

            Basket basket2 = new Basket(Guid.NewGuid(), user1, 2);
            basket2.Status = 2;
            basket2.BasketTotal = 1.55m;

            var soup = dBContext.Products.FirstOrDefault(item => item.Name == "Soup");
            var apples = dBContext.Products.FirstOrDefault(item => item.Name == "Apples");

            BasketItem item3 = new BasketItem(Guid.NewGuid(), basket2.Id, soup, 1, 0.65m);
            BasketItem item4 = new BasketItem(Guid.NewGuid(), basket2.Id, apples, 1, 0.90m);

            basket2.BasketItems.Add(item3);
            basket2.BasketItems.Add(item4);

            baskets.Add(basket1);
            baskets.Add(basket2);

            Basket basket3 = new Basket(Guid.NewGuid(), user1, 2);
            basket3.Status = 2;
            basket3.BasketTotal = 2.60m;

            BasketItem item5 = new BasketItem(Guid.NewGuid(), basket3.Id, soup, 2, 1.30m);
            BasketItem item6 = new BasketItem(Guid.NewGuid(), basket3.Id, apples, 1, 0.90m);
            BasketItem item7 = new BasketItem(Guid.NewGuid(), basket3.Id, bread, 1, 0.40m);

            basket3.BasketItems.Add(item5);
            basket3.BasketItems.Add(item6);
            basket3.BasketItems.Add(item7);

            baskets.Add(basket1);
            baskets.Add(basket2);
            baskets.Add(basket3);

            return baskets;
        }
    }
}
