using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sales_EcommerceModel.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_EcommerceRepository.Context
{
    public class Sales_EcommerceDbContext:IdentityDbContext<ApplicationUser>
    {
        public Sales_EcommerceDbContext(DbContextOptions options) : base(options) { }
        public DbSet<CustomerBilling> CustomerBilling {  get; set; }
        public DbSet<Favourite> Favorite { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<SalesReport> SalesReport { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionType> TransactionType { get; set; }
        public DbSet<Jurisdiction> Jurisdiction { get; set; }
        public DbSet<TriggerStatus> TriggerStatus { get; set; }
        public DbSet<StickyFilter> StickyFilter { get; set; }
        public DbSet<MTC> MTC { get; set; }

    }
}
