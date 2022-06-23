using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace deal.data.Models
{
    public class Context : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryRecord> DeliveryRecords { get; set; }
        public DbSet<Deliveryman> Deliverymens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public Context(DbContextOptions<Context> options) : base(options) {
           
        }

    }
}
