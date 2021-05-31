using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ordermicroservice.Model;

namespace ordermicroservice.Database
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<ordermicroservice.Model.Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Order>().HasData(
                new Model.Order
                {
                    OrderID =1,
                    ProductID = 1,
                    UserID = 1,
                    PriceAtPointInTime = 10000,
                    OccuredAt =  DateTime.Now,
                    Quantity = 1,
                    Total = 10000
                },
                new Model.Order
                {
                    OrderID=2,
                    ProductID = 2,
                    UserID = 2,
                    PriceAtPointInTime = 20000,
                    OccuredAt = DateTime.Now,
                    Quantity = 1,
                    Total = 20000
                }
                );
        }
    }
}
