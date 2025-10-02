using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Application.Dtos
{
    public class VoucherDto
    {
        [Key]
        public Guid Id { get; set; }
        public string VoucherDescription { get; set; } = default!;
        public int VoucherConfig { get; set; }
        public int? PercentageDiscountOnSameProduct { get; set; }
        public Guid? MultiBuyPercentageDiscountDifferentProduct_ProductId { get; set; }
        public int? MultiBuyPercentageDiscountDifferentProduct_Quantity { get; set; }
        public int? MultiBuyPercentageDiscountDifferentProduct_Percentage { get; set; }
    }
}
