using KantarStore.Domain.Entities;
using KantarStore.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Seeders
{
    internal class VoucherSeeder(KantarStoreDBContext dBContext) : IVoucherSeeder
    {
        public async Task Seed()
        {
            if (await dBContext.Database.CanConnectAsync())
            {
                if (!dBContext.Vouchers.Any())
                {
                    var coll = GetVouchers();

                    dBContext.Vouchers.AddRange(coll);

                    await dBContext.SaveChangesAsync();

                    //update products
                    var voucher1 = dBContext.Vouchers.FirstOrDefault(item => item.VoucherDescription == "10% discount");
                    var voucher2 = dBContext.Vouchers.FirstOrDefault(item => item.VoucherDescription == "Buy 2 tins and get a loaf of bread for half price");
                    var apples = dBContext.Products.FirstOrDefault(item => item.Name == "Apples");
                    var soup = dBContext.Products.FirstOrDefault(item => item.Name == "Soup");

                    if (apples != null)
                    {
                        apples.Voucher = voucher1;
                    }

                    if (soup != null)
                    {
                        soup.Voucher = voucher2;
                    }

                    dBContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Voucher> GetVouchers()
        {
            List<Voucher> vouchers = new List<Voucher>();

            Voucher v1 = new Voucher(Guid.NewGuid());
            v1.VoucherConfig = 2;
            v1.VoucherDescription = "10% discount";
            v1.PercentageDiscountOnSameProduct = 10;

            Product? product = dBContext.Products.FirstOrDefault(p => p.Name == "Bread");

            Voucher v2 = new Voucher(Guid.NewGuid());
            v2.VoucherConfig = 3;
            v2.VoucherDescription = "Buy 2 tins and get a loaf of bread for half price";
            v2.MultiBuyPercentageDiscountDifferentProduct_Percentage = 50;
            v2.MultiBuyPercentageDiscountDifferentProduct_ProductId = product.Id;
            v2.MultiBuyPercentageDiscountDifferentProduct_Quantity = 2;

            vouchers.Add(v1);
            vouchers.Add(v2);

            return vouchers;
        }
    }
}
