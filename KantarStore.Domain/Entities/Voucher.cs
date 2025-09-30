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
        public enum VoucherType
        {
            PercentageDiscountOnDifferentProduct,
            PercentageDiscountOnSameProduct,
            MultiBuyPercentageDiscountDifferentProduct,
            MultiBuyPercentageDiscountSameProduct,
            MultiBuyOfferSameProduct,
            MultiBuyOfferDiffentProduct
        }

        [Key]
        public Guid Id { get; set; }
        public VoucherType voucherType { get; set; }


    }
}
