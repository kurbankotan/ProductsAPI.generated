﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext:IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Product> Products { get; set; }

        public ProductsContext(DbContextOptions<ProductsContext> options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 1, ProductName = "IPhone 14", Price = 60000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 2, ProductName = "IPhone 15", Price = 70000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 3, ProductName = "IPhone 16", Price = 80000, IsActive = false });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 4, ProductName = "IPhone 17", Price = 90000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product() { ProductID = 5, ProductName = "IPhone 18", Price = 100000, IsActive = true });
        }







    }
}
