﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAPI.Models;

#nullable disable

namespace ProductsAPI.Migrations
{
    [DbContext(typeof(ProductsContext))]
    [Migration("20240826121042_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ProductsAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            IsActive = true,
                            Price = 60000m,
                            ProductName = "IPhone 14"
                        },
                        new
                        {
                            ProductID = 2,
                            IsActive = true,
                            Price = 70000m,
                            ProductName = "IPhone 15"
                        },
                        new
                        {
                            ProductID = 3,
                            IsActive = false,
                            Price = 80000m,
                            ProductName = "IPhone 16"
                        },
                        new
                        {
                            ProductID = 4,
                            IsActive = true,
                            Price = 90000m,
                            ProductName = "IPhone 17"
                        },
                        new
                        {
                            ProductID = 5,
                            IsActive = true,
                            Price = 100000m,
                            ProductName = "IPhone 18"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
