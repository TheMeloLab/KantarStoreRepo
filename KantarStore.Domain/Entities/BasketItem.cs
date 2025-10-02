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

        public BasketItem(Guid id, Guid productId, int quantity, decimal unitPrice)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            if (productId == Guid.Empty)
                throw new ArgumentException("Product ID cannot be empty.", nameof(productId));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.", nameof(quantity));

            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

            Id = id;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        [Key]
        public Guid Id { get; private set; }
        public Basket Basket { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
    }
}
