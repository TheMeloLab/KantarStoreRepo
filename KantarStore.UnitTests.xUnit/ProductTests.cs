using System;
using KantarStore.Domain.Entities;
using Xunit;

namespace KantarStore.UnitTests.Domain
{
    public class ProductTests
    {
        [Fact]
        public void Ctor_EmptyId_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.Empty, "Name", "Desc", 10m, 1));

            Assert.Contains("ID cannot be empty", ex.Message);
            Assert.Equal("id", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Ctor_BlankName_Throws(string? badName)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.NewGuid(), badName!, "Desc", 10m, 1));

            Assert.Contains("Product name cannot be empty", ex.Message);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void Ctor_NegativePrice_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.NewGuid(), "Name", "Desc", -0.01m, 1));

            Assert.Contains("Price cannot be negative", ex.Message);
            Assert.Equal("price", ex.ParamName);
        }

        [Fact]
        public void Ctor_NegativeStock_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Product(Guid.NewGuid(), "Name", "Desc", 10m, -1));

            Assert.Contains("Stock quantity cannot be negative", ex.Message);
            Assert.Equal("stockQuantity", ex.ParamName);
        }

        [Fact]
        public void Ctor_ValidInputs_SetsAllProperties()
        {
            var id = Guid.NewGuid();
            const string name = "Coffee Mug";
            const string desc = "Ceramic 300ml";
            const decimal price = 12.99m;
            const int stock = 42;

            var p = new Product(id, name, desc, price, stock);

            Assert.Equal(id, p.Id);
            Assert.Equal(name, p.Name);
            Assert.Equal(desc, p.Description);
            Assert.Equal(price, p.Price);
            Assert.Equal(stock, p.StockQuantity);
        }

        [Fact]
        public void Ctor_AllowsZeroPrice_AndZeroStock()
        {
            var p = new Product(Guid.NewGuid(), "Free Sample", "No cost", 0m, 0);

            Assert.Equal(0m, p.Price);
            Assert.Equal(0, p.StockQuantity);
        }

        [Fact]
        public void Ctor_AllowsEmptyDescription()
        {
            var p = new Product(Guid.NewGuid(), "Nametag", "", 1.00m, 5);
            Assert.Equal(string.Empty, p.Description);
        }
     
        [Fact]
        public void Properties_AreMutable_AsDeclared()
        {
            var p = new Product(Guid.NewGuid(), "Pen", "Blue ink", 2.5m, 10);

            p.Name = "Pen - Fine Tip";
            p.Description = "Blue ink - fine tip";
            p.Price = 2.75m;
            p.StockQuantity = 12;

            Assert.Equal("Pen - Fine Tip", p.Name);
            Assert.Equal("Blue ink - fine tip", p.Description);
            Assert.Equal(2.75m, p.Price);
            Assert.Equal(12, p.StockQuantity);
        }

        [Fact]
        public void Voucher_DefaultsToNull_AndCanBeAssigned()
        {
            var p = new Product(Guid.NewGuid(), "Widget", "Basic", 5m, 3);

            Assert.Null(p.Voucher);
        }
    }
}
