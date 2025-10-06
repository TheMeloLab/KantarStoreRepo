using System;
using System.Linq;
using NUnit.Framework;
using KantarStore.Domain.Entities;
using System.Net.Http.Headers;

namespace KantarStore.UnitTests.Domain
{
    [TestFixture]
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

        [Test]
        public void Ctor_EmptyId_Throws()
        {
            var user = new User("Alice", "alice@test.com", "hash123");

            var ex = Assert.Throws<ArgumentException>(() => new Basket(Guid.Empty, user, status: 1));
            StringAssert.Contains("ID cannot be empty", ex!.Message);
            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void AddItems_NewProduct_AddsBasketItem()
        {
            var basket = CreateBasket();
            var prod = CreateProduct("Bananas", 10m);

            basket.AddItems(prod, 3);

            Assert.That(basket.BasketItems, Has.Count.EqualTo(1));
            var item = basket.BasketItems.Single();
            Assert.That(item.Product, Is.SameAs(prod));
            Assert.That(item.Quantity, Is.EqualTo(3));
        }

        [Test]
        public void RecalculateTotals_NoVouchers_SumsUnitPriceTimesQuantity()
        {
            var basket = CreateBasket();
            var p1 = CreateProduct("Chips", 10m);
            var p2 = CreateProduct("Rice", 2.50m);

            basket.AddItems(p1, 3); // 30
            basket.AddItems(p2, 4); // 10

            basket.RecalculateTotals();

            Assert.That(basket.BasketItems.Single(i => i.Product.Id == p1.Id).Price, Is.EqualTo(30m));
            Assert.That(basket.BasketItems.Single(i => i.Product.Id == p2.Id).Price, Is.EqualTo(10m));
            Assert.That(basket.BasketTotal, Is.EqualTo(40m));
        }

        [Test]
        public void RecalculateTotals_DiscountOndSameProduct()
        {
            var basket = CreateBasket();
            var p1 = CreateProduct("Apples", 1.00m);
            var voucher = new Voucher(Guid.Empty);
            voucher.VoucherDescription = "10% discount";
            voucher.VoucherConfig = 2;//PercentageDiscountOnSameProduct
            voucher.PercentageDiscountOnSameProduct = 10;//10% discount
            p1.Voucher = voucher; 

            basket.AddItems(p1, 1); // 30

            basket.RecalculateTotals();

            Assert.That(basket.BasketItems.Single(i => i.Product.Id == p1.Id).Price, Is.EqualTo(0.90m));
            Assert.That(basket.BasketTotal, Is.EqualTo(0.90m));
        }

        [Test]
        public void RecalculateTotals_MultiBuyPercentageDiscountDifferentProduct()
        {
            var basket = CreateBasket();
            var p1 = CreateProduct("Soup", 0.65m);
            var p2 = CreateProduct("Bread", 0.80m);

            var voucher = new Voucher(Guid.Empty);
            voucher.VoucherDescription = "Buy 2 tins and get a loaf of bread for half price";
            voucher.VoucherConfig = 3;//MultiBuyPercentageDiscountDifferentProduct
            voucher.MultiBuyPercentageDiscountDifferentProduct_ProductId = p2.Id;//10% discount
            voucher.MultiBuyPercentageDiscountDifferentProduct_Percentage = 50;
            voucher.MultiBuyPercentageDiscountDifferentProduct_Quantity = 2;

            p1.Voucher = voucher;

            basket.AddItems(p1, 2);
            basket.AddItems(p2, 1);
            basket.RecalculateTotals();

            Assert.That(basket.BasketTotal, Is.EqualTo(1.70m));
        }
    }
}
