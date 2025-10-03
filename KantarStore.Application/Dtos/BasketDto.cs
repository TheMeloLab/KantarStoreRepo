using KantarStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Dtos
{
    public  class BasketDto
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
        public decimal BasketTotal { get; set; }

        public List<BasketItem> Items;
    }
}
