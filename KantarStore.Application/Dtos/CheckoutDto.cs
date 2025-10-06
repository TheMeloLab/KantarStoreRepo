using KantarStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Dtos
{
    public class CheckoutDto
    {
        public Guid UserId { get; set; }
    }
}
