﻿// <auto-generated />
using System;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessObjects.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240722072853_Initdatabase4")]
    partial class Initdatabase4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObjects.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjectStatus")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.ToTable("Account", (string)null);

                    b.HasData(
                        new
                        {
                            AccountId = 1,
                            CreatedDate = new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5106),
                            Email = "adminA@mail.com",
                            FullName = "Admin A",
                            ObjectStatus = 0,
                            Password = "123",
                            PhoneNumber = "1234567890",
                            Role = 3
                        },
                        new
                        {
                            AccountId = 2,
                            CreatedDate = new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5118),
                            Email = "managerA@mail.com",
                            FullName = "Manager A",
                            ObjectStatus = 0,
                            Password = "123",
                            PhoneNumber = "0987654321",
                            Role = 1
                        },
                        new
                        {
                            AccountId = 3,
                            CreatedDate = new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5120),
                            Email = "staffA@mail.com",
                            FullName = "Staff A",
                            ObjectStatus = 0,
                            Password = "123",
                            PhoneNumber = "1122334455",
                            Role = 0
                        },
                        new
                        {
                            AccountId = 4,
                            CreatedDate = new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5121),
                            Email = "staffB@mail.com",
                            FullName = "Staff B",
                            ObjectStatus = 0,
                            Password = "123",
                            PhoneNumber = "5566778899",
                            Role = 0
                        });
                });

            modelBuilder.Entity("BusinessObjects.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Jewelry Type"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Rings"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Necklaces"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Earrings"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Bracelets"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryName = "Pendants"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryName = "Brooches"
                        });
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.Property<int>("JewelryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JewelryId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte[]>("JewelryImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("JewelryName")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("LaborPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("WorkPrice");

                    b.Property<double>("MarkupPercentage")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("StatusSale")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalWeight")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Weight");

                    b.HasKey("JewelryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Jewelry", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.JewelryMaterial", b =>
                {
                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<double>("JewelryWeight")
                        .HasColumnType("float");

                    b.HasKey("JewelryId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("JewelryMaterial", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"), 1L, 1);

                    b.Property<decimal>("BidPrice")
                        .HasColumnType("money");

                    b.Property<int>("Clarity")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("GemCertificate")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsMetail")
                        .HasColumnType("bit");

                    b.Property<double>("MaterialCost")
                        .HasColumnType("float");

                    b.Property<byte[]>("MaterialImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaterialStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OfferPrice")
                        .HasColumnType("money");

                    b.Property<double>("Purity")
                        .HasColumnType("float");

                    b.Property<string>("Sharp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("StockQuantity")
                        .HasColumnType("decimal");

                    b.HasKey("MaterialId");

                    b.ToTable("Material", (string)null);

                    b.HasData(
                        new
                        {
                            MaterialId = 1,
                            BidPrice = 2000000m,
                            Clarity = 10,
                            Color = "Yellow",
                            Description = "Pure gold with 99.99% purity",
                            IsMetail = true,
                            MaterialCost = 2000000.0,
                            MaterialName = "Gold 24K",
                            OfferPrice = 2500000m,
                            Purity = 99.989997863769531,
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 2,
                            BidPrice = 25000m,
                            Clarity = 10,
                            Color = "Silver",
                            Description = "Sterling silver with 92.5% purity",
                            IsMetail = true,
                            MaterialCost = 25000.0,
                            MaterialName = "Silver 925",
                            OfferPrice = 35000m,
                            Purity = 92.5,
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 3,
                            BidPrice = 3000000m,
                            Clarity = 10,
                            Color = "White",
                            Description = "High purity platinum",
                            IsMetail = true,
                            MaterialCost = 3000000.0,
                            MaterialName = "Platinum",
                            OfferPrice = 3500000m,
                            Purity = 95.0,
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 4,
                            BidPrice = 2200000m,
                            Clarity = 10,
                            Color = "Silver",
                            Description = "High purity palladium",
                            IsMetail = true,
                            MaterialCost = 2200000.0,
                            MaterialName = "Palladium",
                            OfferPrice = 2700000m,
                            Purity = 95.0,
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 5,
                            BidPrice = 0m,
                            Clarity = 1,
                            Color = "Colorless",
                            Description = "High quality diamond with excellent clarity",
                            IsMetail = false,
                            MaterialCost = 5000000.0,
                            MaterialName = "Diamond",
                            OfferPrice = 0m,
                            Purity = 100.0,
                            Sharp = "Round Brilliant",
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 6,
                            BidPrice = 0m,
                            Clarity = 3,
                            Color = "Red",
                            Description = "High quality ruby with vivid red color",
                            IsMetail = false,
                            MaterialCost = 3000000.0,
                            MaterialName = "Ruby",
                            OfferPrice = 0m,
                            Purity = 100.0,
                            Sharp = "Oval",
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 7,
                            BidPrice = 0m,
                            Clarity = 4,
                            Color = "Blue",
                            Description = "High quality sapphire with deep blue color",
                            IsMetail = false,
                            MaterialCost = 2500000.0,
                            MaterialName = "Sapphire",
                            OfferPrice = 0m,
                            Purity = 100.0,
                            Sharp = "Cushion",
                            StockQuantity = 0m
                        },
                        new
                        {
                            MaterialId = 8,
                            BidPrice = 0m,
                            Clarity = 5,
                            Color = "Green",
                            Description = "High quality emerald with vivid green color",
                            IsMetail = false,
                            MaterialCost = 4000000.0,
                            MaterialName = "Emerald",
                            OfferPrice = 0m,
                            Purity = 100.0,
                            Sharp = "Emerald Cut",
                            StockQuantity = 0m
                        });
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<decimal>("DiscountPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("PromotionDetailId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("PromotionId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("JewelryId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PromotionDetailId");

                    b.HasIndex("PromotionId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionId"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PromotionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PromotionId");

                    b.ToTable("Promotion", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.PromotionDetail", b =>
                {
                    b.Property<int>("PromotionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionDetailId"), 1L, 1);

                    b.Property<decimal>("DiscountPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("PromotionId")
                        .HasColumnType("int");

                    b.HasKey("PromotionDetailId");

                    b.HasIndex("JewelryId");

                    b.HasIndex("PromotionId");

                    b.ToTable("PromotionDetail", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Warranty", b =>
                {
                    b.Property<int>("WarrantyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyId"), 1L, 1);

                    b.Property<DateTime>("ActiveDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PeriodUnitmeasure")
                        .HasColumnType("int");

                    b.Property<double>("WarrantyPeriod")
                        .HasColumnType("float");

                    b.Property<int>("WarrantyStatus")
                        .HasColumnType("int");

                    b.HasKey("WarrantyId");

                    b.HasIndex("JewelryId");

                    b.HasIndex("OrderId");

                    b.ToTable("Warranties", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.WarrantyHistory", b =>
                {
                    b.Property<int>("WarrantyHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyHistoryId"), 1L, 1);

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReceivedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequireDescription")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResultReport")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WarrantyId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("WarrantyHistoryId");

                    b.HasIndex("WarrantyId");

                    b.ToTable("WarrantyHistories", (string)null);
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.HasOne("BusinessObjects.Category", "Category")
                        .WithMany("Jewelries")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BusinessObjects.JewelryMaterial", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany("JewelryMaterials")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Material", "Material")
                        .WithMany("JewelryMaterials")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.HasOne("BusinessObjects.Account", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany("OrderDetails")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Material", "Material")
                        .WithMany("OrderDetails")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.PromotionDetail", "PromotionDetail")
                        .WithMany("OrderDetail")
                        .HasForeignKey("PromotionDetailId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Promotion", null)
                        .WithMany("OrderDetail")
                        .HasForeignKey("PromotionId");

                    b.Navigation("Jewelry");

                    b.Navigation("Material");

                    b.Navigation("Order");

                    b.Navigation("PromotionDetail");
                });

            modelBuilder.Entity("BusinessObjects.PromotionDetail", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany("PromotionDetails")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Promotion", "Promotion")
                        .WithMany("PromotionDetails")
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("BusinessObjects.Warranty", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany("Warranties")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Order", "Order")
                        .WithMany("Warranties")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyHistory", b =>
                {
                    b.HasOne("BusinessObjects.Warranty", "Warranty")
                        .WithMany("WarrantyHistories")
                        .HasForeignKey("WarrantyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warranty");
                });

            modelBuilder.Entity("BusinessObjects.Account", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.Category", b =>
                {
                    b.Navigation("Jewelries");
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.Navigation("JewelryMaterials");

                    b.Navigation("OrderDetails");

                    b.Navigation("PromotionDetails");

                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.Navigation("JewelryMaterials");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("BusinessObjects.Promotion", b =>
                {
                    b.Navigation("OrderDetail");

                    b.Navigation("PromotionDetails");
                });

            modelBuilder.Entity("BusinessObjects.PromotionDetail", b =>
                {
                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("BusinessObjects.Warranty", b =>
                {
                    b.Navigation("WarrantyHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
