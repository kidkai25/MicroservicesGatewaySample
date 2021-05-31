using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using productmicroservice.Model;

namespace productmicroservice.Database
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<productmicroservice.Model.Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Product>().HasData(
                new Model.Product
                {
                    ProductId = 1,
                    ProductName = "Laptop"
                },
                new Model.Product
                {
                    ProductId = 2,
                    ProductName = "Headphone"
                }
                );
        }
    }
}
