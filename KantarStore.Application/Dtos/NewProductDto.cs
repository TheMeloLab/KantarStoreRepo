using KantarStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Dtos
{
    public class NewProductDto
    {
        public Guid Id { get; set; }

        [StringLength(100,MinimumLength = 3)]
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        [Required(ErrorMessage="Please enter a valid price.")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
