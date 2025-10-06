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

            //if there are no items added we can just return
            if (BasketItems == null || BasketItems.Count == 0)
            {
                BasketTotal = 0;
                return;
            }

            //we need to calculate each item every time, because it can be afftected by any voucher
            foreach (var basketItem in BasketItems)
            {
                //only one voucher can affect a product at a time - they fdo not acmulate
                bool usedVoucher = false;
                basketItem.Price = 0;
                basketItem.Discount = 0;

                if (basketItem.Product.Voucher != null)
                {
                    decimal percentage = 0;
                    decimal discount = 0;
                    decimal unitPrice = 0;

                    int option = (int)basketItem.Product.Voucher.VoucherConfig;

                    //option 2 is a product self related product - direct discount
                    switch (option)
                    {
                        case 2:  //PercentageDiscountOnSameProduct
                            percentage = (decimal)(basketItem.Product.Voucher.PercentageDiscountOnSameProduct / 100.0);
                            discount = basketItem.Product.Price * percentage;
                            unitPrice = basketItem.Product.Price - discount;
                            basketItem.Price = unitPrice * basketItem.Quantity;
                            basketItem.Discount = discount * basketItem.Quantity;
                            usedVoucher = true;
                            break;

                        case 3: // MultiBuyPercentageDiscountDifferentProduct

                            //Implemented by CheckVouchersForCurrentProduct

                            break;
                    }
                }

                //if there are no self related vouches, we need to check if there any on other products taht can affect the current price
                if (!CheckVouchersForCurrentProduct(basketItem) && !usedVoucher)
                    //if not do a simple calcutation
                    basketItem.Price = basketItem.Product.Price * basketItem.Quantity;
            }
            BasketTotal = BasketItems.Sum(p => p.Price);
        }

        private bool CheckVouchersForCurrentProduct(BasketItem basketItem)
        {
            bool applyOtherVoucher = false;
            decimal percentage = 0;
            decimal discount = 0;
            decimal unitPrice = 0;

            BasketItem? originBasketItem = BasketItems
                .Where(p => p.Product?.Voucher?.MultiBuyPercentageDiscountDifferentProduct_ProductId == basketItem.Product.Id)
                .FirstOrDefault();

            //if the quantity for the multibuy is achieved on the basket
            if (originBasketItem != null && originBasketItem.Quantity >= originBasketItem.Product?.Voucher?.MultiBuyPercentageDiscountDifferentProduct_Quantity)
            {
                applyOtherVoucher = true;

                percentage = (decimal)(originBasketItem.Product?.Voucher?.MultiBuyPercentageDiscountDifferentProduct_Percentage / 100.0);
                discount = basketItem.Product.Price * percentage;
                unitPrice = basketItem.Product.Price - discount;

                //calcute the number of times that we can use the discount on the multibuy
                int? numberOfDiscounts = originBasketItem.Quantity / originBasketItem.Product?.Voucher?.MultiBuyPercentageDiscountDifferentProduct_Quantity;

                //imagine that we have 4 cans of soup and three loafs of bread, 2 of them will have 50%
                for (int i = 0; i < numberOfDiscounts; i++)
                {
                    basketItem.Price += unitPrice;
                    basketItem.Discount += discount;
                }

                //the third one will have the normal price
                for (int i = 0; i < basketItem.Quantity - numberOfDiscounts; i++)
                {
                    basketItem.Price += basketItem.Product.Price;
                }
            }
            return applyOtherVoucher;
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
        public void RemoveItems(Product prod)
        {
            var basketItem = BasketItems.SingleOrDefault(i => i.Product.Id == prod.Id);

            if (basketItem != null)
            {
                BasketItems.Remove(basketItem);
            }
        }
    }
  }
