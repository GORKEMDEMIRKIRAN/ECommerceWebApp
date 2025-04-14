
//============================
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
//============================

// model için
using Ecommerce.Core.Entities;
// data içindeki datacontext
using Ecommerce.Data;
using System.Globalization;
// txt dosyaları

//============================


namespace Ecommerce.Data.SeedData
{
    public static class SeedDatabase
    {


        //======================================================
        public class CategoryConfig:IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.HasData(SeedCategories);
            }
        }
        //======================================================
        public class ProductConfig : IEntityTypeConfiguration<Product>
        {
            private readonly List<Product> _productList;

            public ProductConfig(List<Product> productList)
            {
                _productList = productList;
            }

            public void Configure(EntityTypeBuilder<Product> builder)
            {
                builder.HasData(_productList);
            }
        }
        //======================================================
        public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
        {
            private readonly List<ProductCategory> _productCategory;

            public ProductCategoryConfig(List<ProductCategory> productCategory)
            {
                _productCategory = productCategory;
            }

            public void Configure(EntityTypeBuilder<ProductCategory> builder)
            {
                builder.HasData(_productCategory);
            }
        }
        //======================================================
        public static (List<Product>, List<ProductCategory>) ProductData()
        {
            List<Product> productList = new List<Product>(); 
            List<ProductCategory> productCategoryList = new List<ProductCategory>();

            // Doğru dizini almak için AppDomain.CurrentDomain.BaseDirectory kullanıyoruz
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // DataFiles klasörünün konumunu Data projesinin dizinine göre ayarlıyoruz
            string dataFilesDirectory = Path.Combine(baseDirectory, "..", "..", "..", "..", "Ecommerce.Data", "SeedData", "DataFiles");

            string computerfilePath = Path.Combine(dataFilesDirectory, "computer.txt");
            string fridgefilePath = Path.Combine(dataFilesDirectory, "fridge.txt");
            string phonefilePath = Path.Combine(dataFilesDirectory, "phone.txt");
            string soundsystemfilePath = Path.Combine(dataFilesDirectory, "soundsystem.txt");
            string tabletfilePath = Path.Combine(dataFilesDirectory, "tablet.txt");

            Console.WriteLine(computerfilePath);
            Console.WriteLine(fridgefilePath);
            Console.WriteLine(phonefilePath);
            Console.WriteLine(soundsystemfilePath);
            Console.WriteLine(tabletfilePath);
            
            string[] computerRows=File.ReadAllLines(computerfilePath);
            string[] fridgeRows=File.ReadAllLines(fridgefilePath);
            string[] phoneRows=File.ReadAllLines(phonefilePath);
            string[] soundsystemRows=File.ReadAllLines(soundsystemfilePath);
            string[] tabletRows=File.ReadAllLines(tabletfilePath);

            var computerDistance=computerRows.Length;
            var fridgeDistance=fridgeRows.Length;
            var phoneDistance=phoneRows.Length;
            var soundSystemDistance=soundsystemRows.Length;
            var tableDistance=tabletRows.Length;

            var totalDistance=computerDistance+fridgeDistance+phoneDistance+soundSystemDistance+tableDistance;

            List<string[]> allrows =  new List<string[]>() {computerRows,fridgeRows,phoneRows,soundsystemRows,tabletRows};

            var count=1;
            foreach(string[] onerows in allrows)
            {

                foreach(string row in onerows)
                {
                    try
                    {
                        string[] columns=row.Split("|"); // "|" ile ayırma
                        if(columns.Length>=6)
                        {
                            //===============================
                            Product product = new Product()
                            {
                                ProductId=count,
                                Name=columns[1],
                                Url=columns[2],
                                Price=PriceRange(columns[3]),
                                Description=columns[4],
                                ImageUrl=columns[5],
                                IsApproved=true,
                                IsHome=true
                            };
                            productList.Add(product);
                            if(count<=computerDistance)
                            {
                                ProductCategory[] SeedProductcategory1={
                                    new ProductCategory(){ProductId=count,CategoryId=1},
                                    new ProductCategory(){ProductId=count,CategoryId=24}
                                };
                                productCategoryList.AddRange(SeedProductcategory1);
                            }else if(count>computerDistance && count<=fridgeDistance+computerDistance){
                                ProductCategory[] SeedProductcategory2={
                                    new ProductCategory(){ProductId=count,CategoryId=1},
                                    new ProductCategory(){ProductId=count,CategoryId=32}
                                };
                                productCategoryList.AddRange(SeedProductcategory2);
                            }else if(count>fridgeDistance+computerDistance && count<=phoneDistance+fridgeDistance+computerDistance){
                                ProductCategory[] SeedProductcategory3={
                                    new ProductCategory(){ProductId=count,CategoryId=1},
                                    new ProductCategory(){ProductId=count,CategoryId=27}
                                };
                                productCategoryList.AddRange(SeedProductcategory3);
                            }else if(count>phoneDistance+fridgeDistance+computerDistance && count<=soundSystemDistance+phoneDistance+fridgeDistance+computerDistance){
                                ProductCategory[] SeedProductcategory4={
                                    new ProductCategory(){ProductId=count,CategoryId=1},
                                    new ProductCategory(){ProductId=count,CategoryId=33}
                                };
                                productCategoryList.AddRange(SeedProductcategory4);
                            }else if(count>soundSystemDistance+phoneDistance+fridgeDistance+computerDistance && count<=totalDistance){
                                ProductCategory[] SeedProductcategory5={
                                    new ProductCategory(){ProductId=count,CategoryId=1},
                                    new ProductCategory(){ProductId=count,CategoryId=25}
                                };
                                productCategoryList.AddRange(SeedProductcategory5);
                            }
                            //===============================
                            count+=1;
                        }
                        else{
                            Console.WriteLine($"Geçersiz satir: {row}, Beklenen sütun sayisi: 6");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Satir işlenirken hata oluştu: {row},Hata: {ex.Message}");
                    }
                }
            }

            return (productList, productCategoryList);
        }
        //======================================================
        // B u fonksiyon tl silip float dönüştürüyor.
        public static decimal PriceRange(string priceString)
        {
            // "tl" ayırma
            string priceNumericString=priceString.Replace("TL","").Trim();
            // 24.564,45 bu txt örneği
            // noktayı kaldıralım ve "," yerine nokta verelim.
            priceNumericString=priceNumericString.Replace(".","");
            priceNumericString=priceNumericString.Replace(",",".");
            // sonucunda 24565.45 olarak database geçiyor decimal olarak database aktarıyoruz.
            //decimal dönüştürme
            decimal priceDecimal=decimal.Parse(priceNumericString,CultureInfo.InvariantCulture);
            return priceDecimal;
        }
        
        
        
        //======================================================
        // Ana Kategoriler
        private static Category[] SeedCategories = {
            // ana kategoriler
            new Category(){CategoryId=1,Name="Elektronik"},
            new Category(){CategoryId=2,Name="Moda"},
            new Category(){CategoryId=3,Name="Ev"},
            new Category(){CategoryId=4,Name="Yaşam"},
            new Category(){CategoryId=5,Name="Kırtasiye"},
            new Category(){CategoryId=6,Name="Ofis"},
            new Category(){CategoryId=7,Name="Oto"},
            new Category(){CategoryId=8,Name="Bahçe"},
            new Category(){CategoryId=9,Name="Yapı"},
            new Category(){CategoryId=10,Name="Market"},
            new Category(){CategoryId=11,Name="Anne"},
            new Category(){CategoryId=12,Name="Bebek"},
            new Category(){CategoryId=13,Name="Oyuncak"},
            new Category(){CategoryId=14,Name="Spor"},
            new Category(){CategoryId=15,Name="Outdoor"},
            new Category(){CategoryId=16,Name="Kozmetik"},
            new Category(){CategoryId=17,Name="Kişisel Bakım"},
            new Category(){CategoryId=18,Name="Süper Market"},
            new Category(){CategoryId=19,Name="Pet Shop"},
            new Category(){CategoryId=20,Name="Kitap"},
            new Category(){CategoryId=21,Name="Müzik"},
            new Category(){CategoryId=22,Name="Film"},
            new Category(){CategoryId=23,Name="Hobi"},

            new Category(){CategoryId=24,Name="Bilgisayar"},
            new Category(){CategoryId=25,Name="Tablet"},
            new Category(){CategoryId=26,Name="Yazıcı"},
            new Category(){CategoryId=27,Name="Telefon"},
            new Category(){CategoryId=28,Name="Telefon Aksesuarları"},
            new Category(){CategoryId=29,Name="Beyaz Eşya"},
            new Category(){CategoryId=30,Name="Klima"},
            new Category(){CategoryId=31,Name="Kamera"},
            new Category(){CategoryId=32,Name="Buzdolabı"},
            new Category(){CategoryId=33,Name="Ses sistemi"}
        };


    }
}
