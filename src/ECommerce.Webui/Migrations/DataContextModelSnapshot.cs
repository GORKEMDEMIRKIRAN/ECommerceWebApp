﻿// <auto-generated />
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECommerce.Webui.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Ecommerce.Core.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Elektronik"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Moda"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Ev"
                        },
                        new
                        {
                            CategoryId = 4,
                            Name = "Yaşam"
                        },
                        new
                        {
                            CategoryId = 5,
                            Name = "Kırtasiye"
                        },
                        new
                        {
                            CategoryId = 6,
                            Name = "Ofis"
                        },
                        new
                        {
                            CategoryId = 7,
                            Name = "Oto"
                        },
                        new
                        {
                            CategoryId = 8,
                            Name = "Bahçe"
                        },
                        new
                        {
                            CategoryId = 9,
                            Name = "Yapı"
                        },
                        new
                        {
                            CategoryId = 10,
                            Name = "Market"
                        },
                        new
                        {
                            CategoryId = 11,
                            Name = "Anne"
                        },
                        new
                        {
                            CategoryId = 12,
                            Name = "Bebek"
                        },
                        new
                        {
                            CategoryId = 13,
                            Name = "Oyuncak"
                        },
                        new
                        {
                            CategoryId = 14,
                            Name = "Spor"
                        },
                        new
                        {
                            CategoryId = 15,
                            Name = "Outdoor"
                        },
                        new
                        {
                            CategoryId = 16,
                            Name = "Kozmetik"
                        },
                        new
                        {
                            CategoryId = 17,
                            Name = "Kişisel Bakım"
                        },
                        new
                        {
                            CategoryId = 18,
                            Name = "Süper Market"
                        },
                        new
                        {
                            CategoryId = 19,
                            Name = "Pet Shop"
                        },
                        new
                        {
                            CategoryId = 20,
                            Name = "Kitap"
                        },
                        new
                        {
                            CategoryId = 21,
                            Name = "Müzik"
                        },
                        new
                        {
                            CategoryId = 22,
                            Name = "Film"
                        },
                        new
                        {
                            CategoryId = 23,
                            Name = "Hobi"
                        },
                        new
                        {
                            CategoryId = 24,
                            Name = "Bilgisayar"
                        },
                        new
                        {
                            CategoryId = 25,
                            Name = "Tablet"
                        },
                        new
                        {
                            CategoryId = 26,
                            Name = "Yazıcı"
                        },
                        new
                        {
                            CategoryId = 27,
                            Name = "Telefon"
                        },
                        new
                        {
                            CategoryId = 28,
                            Name = "Telefon Aksesuarları"
                        },
                        new
                        {
                            CategoryId = 29,
                            Name = "Beyaz Eşya"
                        },
                        new
                        {
                            CategoryId = 30,
                            Name = "Klima"
                        },
                        new
                        {
                            CategoryId = 31,
                            Name = "Kamera"
                        },
                        new
                        {
                            CategoryId = 32,
                            Name = "Buzdolabı"
                        },
                        new
                        {
                            CategoryId = 33,
                            Name = "Ses sistemi"
                        });
                });

            modelBuilder.Entity("Ecommerce.Core.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHome")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Ecommerce.Core.Entities.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("Ecommerce.Core.Entities.ProductCategory", b =>
                {
                    b.HasOne("Ecommerce.Core.Entities.Category", "category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Core.Entities.Product", "product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Ecommerce.Core.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("Ecommerce.Core.Entities.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
