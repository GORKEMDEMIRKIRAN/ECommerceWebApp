


//========================
using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
//========================
using Ecommerce.Core.Entities;
using Ecommerce.Data.SeedData;

//========================

namespace Ecommerce.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Product> products {get;set;}
        public DbSet<Category> categories {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c=>c.CategoryId)
                .ValueGeneratedOnAdd();
            // modelBuilder.Entity<Product>()
            //     .Property(c=>c.ProductId)
            //     .ValueGeneratedOnAdd();
           
            modelBuilder.Entity<ProductCategory>()
                .HasKey(c=> new{ c.CategoryId, c.ProductId});

            // SeedDatabase.ComputerProductData() metodundan listeleri alıyoruz
            // var (productList, productCategoryList) = SeedDatabase.ComputerProductData();
            // modellere hazır veri ekliyoruz.
            modelBuilder.ApplyConfiguration(new SeedDatabase.CategoryConfig());
            // modelBuilder.ApplyConfiguration(new SeedDatabase.ProductConfig(productList));
            // modelBuilder.ApplyConfiguration(new SeedDatabase.ProductCategoryConfig(productCategoryList));
        }

        public void SeedData()
        {
            var (productList, productCategoryList) = SeedDatabase.ProductData();
            // Veritabanına ekleme işlemlerini burada yapın
            this.AddRange(productList);
            this.AddRange(productCategoryList);
            this.SaveChanges();
        }
    }
}
