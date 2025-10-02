using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Entities
{
    public class Voucher
    {
        protected Voucher()
        {

        }

        public Voucher(Guid id)
        {
            Id = id;
        }

        public enum VoucherType
        {
            PercentageDiscountOnDifferentProduct = 1,
            PercentageDiscountOnSameProduct = 2,
            MultiBuyPercentageDiscountDifferentProduct = 3,
            MultiBuyPercentageDiscountSameProduct = 4,
            MultiBuyOfferSameProduct = 5,
            MultiBuyOfferDiffentProduct = 6
        } 

        [Key]
        public Guid Id { get; set; }
        public string VoucherDescription { get; set; } = default!;
        public int VoucherConfig { get; set; }
        public Guid? PercentageDiscountOnDifferentProduct_ProductId { get; set; }
        public int? PercentageDiscountOnDifferentProduct_Percentage { get; set; }
        public int? PercentageDiscountOnSameProduct { get; set; }
        public Guid? MultiBuyPercentageDiscountDifferentProduct_ProductId { get; set; }
        public int? MultiBuyPercentageDiscountDifferentProduct_Quantity { get; set; }
        public int? MultiBuyPercentageDiscountDifferentProduct_Percentage { get; set; }
        public int? MultiBuyPercentageDiscountSameProduct_Percentage { get; set; }
        public int? MultiBuyPercentageDiscountSameProduct_Quantity { get; set; }
        public int? MultiBuyOfferSameProduct_Quantity { get; set; }
        public Guid? MultiBuyOfferDiffentProduct_ProductId { get; set; }
    }
}
