﻿using Microsoft.EntityFrameworkCore;

namespace Redis.Api.Moldes
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                               new Product
                               {
                                   Id = "1",
                                   Name = "Product 1",
                                   Price = 10
                               },
                                              new Product
                                              {
                                                  Id = "2",
                                                  Name = "Product 2",
                                                  Price = 20
                                              },
                                                             new Product
                                                             {
                                                                 Id = "3",
                                                                 Name = "Product 3",
                                                                 Price = 30
                                                             },
                                                                            new Product
                                                                            {
                                                                                Id = "4",
                                                                                Name = "Product 4",
                                                                                Price = 40
                                                                            },
                                                                                           new Product
                                                                                           {
                                                                                               Id = "5",
                                                                                               Name = "Product 5",
                                                                                               Price = 50
                                                                                           }
                                                                        ) ;
            base.OnModelCreating(modelBuilder);
        }
    }
}
