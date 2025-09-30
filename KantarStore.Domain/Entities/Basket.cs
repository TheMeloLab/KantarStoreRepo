using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Entities
{
    public class Basket
    {
        protected Basket() 
        { 
            
        }

        public Basket(Guid id, Guid userId): base()
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            Id = id;
            UserId = userId;
        }

        [Key]
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        private readonly List<BasketItem> _items = new();
    }
  }
