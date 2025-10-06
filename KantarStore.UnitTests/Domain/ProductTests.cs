using KantarStore.Domain.Entities;
using System;
using NUnit.Framework;

namespace KantarStore.UnitTests.Domain
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void Ctor_EmptyId_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.Empty, "Name", "Desc", 10m, 1));

            StringAssert.Contains("ID cannot be empty", ex!.Message);

            Assert.That(ex.ParamName, Is.EqualTo("id"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Ctor_BlankName_Throws(string? badName)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.NewGuid(), badName!, "Desc", 10m, 1));

            StringAssert.Contains("Product name cannot be empty", ex!.Message);
            Assert.That(ex.ParamName, Is.EqualTo("name"));
        }

        [Test]
        public void Ctor_NegativePrice_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.NewGuid(), "Name", "Desc", -0.01m, 1));

            StringAssert.Contains("Price cannot be negative", ex!.Message);
            Assert.That(ex.ParamName, Is.EqualTo("price"));
        }

        [Test]
        public void Ctor_NegativeStock_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.NewGuid(), "Name", "Desc", 10m, -1));

            StringAssert.Contains("Stock quantity cannot be negative", ex!.Message);
            Assert.That(ex.ParamName, Is.EqualTo("stockQuantity"));
        }

        [Test]
        public void Ctor_ValidInputs_SetsAllProperties()
        {
            var id = Guid.NewGuid();
            const string name = "Coffee Mug";
            const string desc = "Ceramic 300ml";
            const decimal price = 12.99m;
            const int stock = 42;

            var product = new Product(id, name, desc, price, stock);

            Assert.That(product.Id, Is.EqualTo(id));
            Assert.That(product.Name, Is.EqualTo(name));
            Assert.That(product.Description, Is.EqualTo(desc));
            Assert.That(product.Price, Is.EqualTo(price));
            Assert.That(product.StockQuantity, Is.EqualTo(stock));
        }

        [Test]
        public void Ctor_AllowsZeroPrice_AndZeroStock()
        {
            var product = new Product(Guid.NewGuid(), "Free Sample", "No cost", 0m, 0);

            Assert.That(product.Price, Is.EqualTo(0m));
            Assert.That(product.StockQuantity, Is.EqualTo(0));
        }

        [Test]
        public void Ctor_AllowsEmptyDescription()
        {
            var product = new Product(Guid.NewGuid(), "Nametag", "", 1.00m, 5);

            Assert.That(product.Description, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Properties_AreMutable_AsDeclared()
        {
            var product = new Product(Guid.NewGuid(), "Pen", "Blue ink", 2.5m, 10);

            product.Name = "Pen - Fine Tip";
            product.Description = "Blue ink - fine tip";
            product.Price = 2.75m;
            product.StockQuantity = 12;

            Assert.Multiple(() =>
            {
                Assert.That(product.Name, Is.EqualTo("Pen - Fine Tip"));
                Assert.That(product.Description, Is.EqualTo("Blue ink - fine tip"));
                Assert.That(product.Price, Is.EqualTo(2.75m));
                Assert.That(product.StockQuantity, Is.EqualTo(12));
            });
        }
    }
}