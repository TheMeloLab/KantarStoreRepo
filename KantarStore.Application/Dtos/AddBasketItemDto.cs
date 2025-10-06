using KantarStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Dtos
{
    public class AddBasketItemDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get;  set; }
      
    }
}
