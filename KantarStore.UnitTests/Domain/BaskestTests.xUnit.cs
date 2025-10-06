using System;
using System.Linq;
using KantarStore.Domain.Entities;
using Xunit;

namespace KantarStore.UnitTests.Domain
{
    public class BasketTests
    {
        private static Basket CreateBasket()
        {
            return new Basket(Guid.NewGuid(), new User("Test", "test@kantar.com", "hashedpwd"), status: 1);
        }

        private static Product CreateProduct(string name, decimal price, Voucher? voucher = null)
        {
            var product = new Product(Guid.NewGuid(), name, "Test description", price, 10);
            product.Voucher = voucher;
            return product;
        }

        [Fact]
        public void Ctor_EmptyId_Throws()
        {
            var user = new User("Alice", "alice@test.com", "hash123");

            var ex = Assert.Throws<ArgumentException>(() => new Basket(Guid.Empty, user, status: 1));
            Assert.Contains("ID cannot be empty", ex.Message);
            Assert.Equal("id", ex.ParamName);
        }

        [Fact]
        public void AddItems_NewProduct_AddsBasketItem()
        {
            var basket = CreateBasket();
            var prod = CreateProduct("Bananas", 10m);

            basket.AddItems(prod, 3);

            Assert.Single(basket.BasketItems);
            var item = basket.BasketItems.Single();
            Assert.Same(prod, item.Product);
            Assert.Equal(3, item.Quantity);
        }

        [Fact]
        public void RecalculateTotals_NoVouchers_SumsUnitPriceTimesQuantity()
        {
            var basket = CreateBasket();
            var p1 = CreateProduct("Chips", 10m);
            var p2 = CreateProduct("Rice", 2.50m);

            basket.AddItems(p1, 3); // 30
            basket.AddItems(p2, 4); // 10

            basket.RecalculateTotals();

            Assert.Equal(30m, basket.BasketItems.Single(i => i.Product.Id == p1.Id).Price);
            Assert.Equal(10m, basket.BasketItems.Single(i => i.Product.Id == p2.Id).Price);
            Assert.Equal(40m, basket.BasketTotal);
        }

        [Fact]
        public void RecalculateTotals_DiscountOnSameProduct()
        {
            var basket = CreateBasket();
            var p1 = CreateProduct("Apples", 1.00m);

            var voucher = new Voucher(Guid.Empty)
            {
                VoucherDescription = "10% discount",
                VoucherConfig = 2, // PercentageDiscountOnSameProduct
                PercentageDiscountOnSameProduct = 10 // 10% discount
            };
            p1.Voucher = voucher;

            basket.AddItems(p1, 1);

            basket.RecalculateTotals();

            Assert.Equal(0.90m, basket.BasketItems.Single(i => i.Product.Id == p1.Id).Price);
            Assert.Equal(0.90m, basket.BasketTotal);
        }

        [Fact]
        public void RecalculateTotals_MultiBuyPercentageDiscountDifferentProduct()
        {
            var basket = CreateBasket();
            var p1 = CreateProduct("Soup", 0.65m);
            var p2 = CreateProduct("Bread", 0.80m);

            var voucher = new Voucher(Guid.Empty)
            {
                VoucherDescription = "Buy 2 tins and get a loaf of bread for half price",
                VoucherConfig = 3, // MultiBuyPercentageDiscountDifferentProduct
                MultiBuyPercentageDiscountDifferentProduct_ProductId = p2.Id,
                MultiBuyPercentageDiscountDifferentProduct_Percentage = 50,
                MultiBuyPercentageDiscountDifferentProduct_Quantity = 2
            };
            p1.Voucher = voucher;

            basket.AddItems(p1, 2);
            basket.AddItems(p2, 1);

            basket.RecalculateTotals();

            Assert.Equal(1.70m, basket.BasketTotal);
        }
    }
}
