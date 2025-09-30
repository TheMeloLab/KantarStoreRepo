using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KantarStore.Domain.Entities
{
    public class Product
    {
        protected Product()
        {
        }
        public Product(Guid id, string name, string description, decimal price, int stockQuantity) 
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.", nameof(name));

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));

            if (stockQuantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(stockQuantity));

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
