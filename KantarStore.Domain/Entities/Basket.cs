using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Entities
{
    public class Basket
    {
        protected Basket() 
        {

        }

        public enum BasketStatus
        {
            Open = 1,
            Closed = 2
        }

        public Basket(Guid id, User user, int status): base()
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            if (user == null)
                throw new ArgumentException("User cannot be null.", nameof(user));

            Id = id;
            User = user;
            Status = 1;
        }

        [Key]
        public Guid Id { get; set; }
        public User User { get; set; }
        public int Status { get; set; }
        public decimal BasketTotal { get; set; } = 0;
        public List<BasketItem> BasketItems { get; set; } = new();

        public void RecalculateTotals()
        {
            if (BasketItems == null || BasketItems.Count == 0) 
                return;

            foreach (var basketItem in BasketItems)
            {
                if (basketItem.Product.Voucher != null)
                {
                    decimal percentage = 0;
                    decimal discount = 0;
                    decimal unitPrice = 0;

                    int option = (int)basketItem.Product.Voucher.VoucherConfig;

                    switch (option)
                    {
                        case 2:  //PercentageDiscountOnSameProduct
                            percentage = (decimal)(basketItem.Product.Voucher.PercentageDiscountOnSameProduct / 100.0);
                            discount = basketItem.Product.Price * percentage;
                            unitPrice = basketItem.Product.Price - discount;
                            basketItem.Price = unitPrice * basketItem.Quantity;
                            basketItem.Discount = discount * basketItem.Quantity;
                            break;

                        case 3: // MultiBuyPercentageDiscountDifferentProduct

                            if (basketItem.Quantity >= basketItem.Product.Voucher.MultiBuyPercentageDiscountDifferentProduct_Quantity)
                            {
                                BasketItem existingBasketItem = BasketItems.Where(p => p.Product.Id == basketItem.Product.Voucher.MultiBuyPercentageDiscountDifferentProduct_ProductId).FirstOrDefault();

                                if (existingBasketItem != null)
                                {
                                    percentage = (decimal)(basketItem.Product.Voucher.MultiBuyPercentageDiscountDifferentProduct_Percentage / 100.0);
                                    discount = existingBasketItem.Product.Price * percentage;
                                    unitPrice = existingBasketItem.Product.Price - discount;

                                    //how many time we can apply the discount (for 4 cans of soup we can apply the discount 2...)
                                    int numberOfDiscounts = basketItem.Quantity % 2;

                                    for (int i = 0; i < numberOfDiscounts; i++)
                                    {
                                        existingBasketItem.Price += unitPrice;
                                        existingBasketItem.Discount += discount;
                                    }

                                    for (int i = 0; i < basketItem.Quantity - numberOfDiscounts; i++)
                                    {
                                        existingBasketItem.Price += existingBasketItem.Product.Price;
                                    }
                                }
                            }

                            basketItem.Price = basketItem.Product.Price * basketItem.Quantity;

                            break;

                    }
                }
                else
                {
                    basketItem.Price = basketItem.Product.Price * basketItem.Quantity;
                }
            }

            BasketTotal = BasketItems.Sum(p => p.Price);
        }

        public void AddItems(Product prod,int quantity) 
        {
            var basketItem = BasketItems.SingleOrDefault(i => i.Product.Id == prod.Id);

            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                BasketItems.Add(new BasketItem(Guid.NewGuid(), Id, prod, quantity, prod.Price));
            }
        }
        public void RemoveItems(Product prod,int quantity)
        {
            var basketItem = BasketItems.SingleOrDefault(i => i.Product.Id == prod.Id);

            if (basketItem != null)
            {
                basketItem.Quantity -= quantity;

                if(basketItem.Quantity == 0)
                {
                    BasketItems.Remove(basketItem);
                }
            }
        }
    }
  }
