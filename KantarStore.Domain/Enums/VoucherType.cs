using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Domain.Enums
{
    public class Enumerations
    {
        public enum VoucherType
        {
            PercentageDiscountOnDifferentProduct = 1,
            PercentageDiscountOnSameProduct = 2,
            MultiBuyPercentageDiscountDifferentProduct = 3,
            MultiBuyPercentageDiscountSameProduct = 4,
            MultiBuyOfferSameProduct = 5,
            MultiBuyOfferDiffentProduct = 6
        }
    }
}
