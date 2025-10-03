using KantarStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KantarStore.Infrastructure.Persistance
{
    internal class KantarStoreDBContext : DbContext
    {
        public KantarStoreDBContext(DbContextOptions<KantarStoreDBContext> options) : 
            base(options) 
        {
            
        }

        internal DbSet<Product> Products { get; set; }

        internal DbSet<Basket> Baskets { get; set; }

        internal DbSet<BasketItem> BasketItems { get; set; }

        internal DbSet<Voucher> Vouchers { get; set; }

        internal DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<Basket>()
                .HasMany(b => b.BasketItems)
                .WithOne()
                .HasForeignKey(b => b.BasketId);
        }
    }
}
