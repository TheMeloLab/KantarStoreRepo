using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Entities
{
    public class BasketItem
    {
        protected BasketItem() { }

        public BasketItem(Guid id, Guid basketId, Product product, int quantity, decimal unitPrice)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            if (basketId == Guid.Empty)
                throw new ArgumentException("Basket ID cannot be empty.", nameof(id));

            if (product == null)
                throw new ArgumentException("Product cannot be null.", nameof(product));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

            Id = id;
            Product = product;
            BasketId = basketId;
            Quantity = quantity;
            Price = unitPrice;
        }

        [Key]
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
