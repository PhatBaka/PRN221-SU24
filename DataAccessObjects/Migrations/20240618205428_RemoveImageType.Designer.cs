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
    [Migration("20240618205428_RemoveImageType")]
    partial class RemoveImageType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountCounter", b =>
                {
                    b.Property<int>("AccountsAccountId")
                        .HasColumnType("int");

                    b.Property<int>("CountersCounterId")
                        .HasColumnType("int");

                    b.HasKey("AccountsAccountId", "CountersCounterId");

                    b.HasIndex("CountersCounterId");

                    b.ToTable("AccountCounter");
                });

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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BusinessObjects.Counter", b =>
                {
                    b.Property<int>("CounterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CounterId"), 1L, 1);

                    b.Property<string>("CounterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("money");

                    b.HasKey("CounterId");

                    b.ToTable("Counter");
                });

            modelBuilder.Entity("BusinessObjects.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("TotalPoint")
                        .HasColumnType("float");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.Property<int>("JewelryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JewelryId"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CounterId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte[]>("JewelryImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("JewelryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("MarkupPercentage")
                        .HasColumnType("float");

                    b.Property<string>("ObjectStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<decimal>("WorkPrice")
                        .HasColumnType("money");

                    b.HasKey("JewelryId");

                    b.HasIndex("CounterId");

                    b.ToTable("Jewelry");
                });

            modelBuilder.Entity("BusinessObjects.JewelryMaterial", b =>
                {
                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<double>("MetalWeight")
                        .HasColumnType("float");

                    b.HasKey("JewelryId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("JewelryMaterial");
                });

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialId"), 1L, 1);

                    b.Property<int?>("CounterId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMetail")
                        .HasColumnType("bit");

                    b.Property<decimal>("MaterialCost")
                        .HasColumnType("money");

                    b.Property<byte[]>("MaterialImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("MaterialQuantity")
                        .HasColumnType("int");

                    b.Property<double>("MaterialWeight")
                        .HasColumnType("float");

                    b.Property<string>("UnitType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaterialId");

                    b.HasIndex("CounterId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("CounterId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("DiscountPrice")
                        .HasColumnType("float");

                    b.Property<double>("FinalPrice")
                        .HasColumnType("float");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CounterId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<decimal>("DiscountPrice")
                        .HasColumnType("money");

                    b.Property<double>("DiscountValue")
                        .HasColumnType("float");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("money");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.Property<int>("WarrantyOrderId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("JewelryId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("BusinessObjects.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionId"), 1L, 1);

                    b.Property<decimal>("AcceptedPrice")
                        .HasColumnType("money");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("DiscountValue")
                        .HasColumnType("money");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("PromotionCode")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PromotionName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PromotionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("PromotionId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyJewelry", b =>
                {
                    b.Property<int>("WarrantyJewelryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyJewelryId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("WarrantyMonths")
                        .HasColumnType("int");

                    b.HasKey("WarrantyJewelryId");

                    b.HasIndex("JewelryId")
                        .IsUnique();

                    b.ToTable("WarrantyJewelry");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyOrder", b =>
                {
                    b.Property<int>("WarrantyOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyOrderId"), 1L, 1);

                    b.Property<int>("JewelryId")
                        .HasColumnType("int");

                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("WarrantyPeriod")
                        .HasColumnType("float");

                    b.Property<int>("WarrantyRequestId")
                        .HasColumnType("int");

                    b.HasKey("WarrantyOrderId");

                    b.HasIndex("JewelryId");

                    b.HasIndex("OrderDetailId")
                        .IsUnique();

                    b.HasIndex("OrderId");

                    b.ToTable("WarrantyOrder");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyRequest", b =>
                {
                    b.Property<int>("WarrantyRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarrantyRequestId"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReceivedDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("WarrantyOrderId")
                        .HasColumnType("int");

                    b.Property<string>("WarrantyStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarrantyRequestId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("WarrantyOrderId")
                        .IsUnique();

                    b.ToTable("WarrantyRequest");
                });

            modelBuilder.Entity("JewelryPromotion", b =>
                {
                    b.Property<int>("JewelriesJewelryId")
                        .HasColumnType("int");

                    b.Property<int>("PromotionsPromotionId")
                        .HasColumnType("int");

                    b.HasKey("JewelriesJewelryId", "PromotionsPromotionId");

                    b.HasIndex("PromotionsPromotionId");

                    b.ToTable("JewelryPromotion");
                });

            modelBuilder.Entity("OrderDetailPromotion", b =>
                {
                    b.Property<int>("OrderDetailsOrderDetailId")
                        .HasColumnType("int");

                    b.Property<int>("PromotionsPromotionId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailsOrderDetailId", "PromotionsPromotionId");

                    b.HasIndex("PromotionsPromotionId");

                    b.ToTable("OrderDetailPromotion");
                });

            modelBuilder.Entity("OrderPromotion", b =>
                {
                    b.Property<int>("OrdersOrderId")
                        .HasColumnType("int");

                    b.Property<int>("PromotionsPromotionId")
                        .HasColumnType("int");

                    b.HasKey("OrdersOrderId", "PromotionsPromotionId");

                    b.HasIndex("PromotionsPromotionId");

                    b.ToTable("OrderPromotion");
                });

            modelBuilder.Entity("AccountCounter", b =>
                {
                    b.HasOne("BusinessObjects.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Counter", null)
                        .WithMany()
                        .HasForeignKey("CountersCounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.HasOne("BusinessObjects.Counter", null)
                        .WithMany("Jewelries")
                        .HasForeignKey("CounterId");
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

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.HasOne("BusinessObjects.Counter", null)
                        .WithMany("Materials")
                        .HasForeignKey("CounterId");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.HasOne("BusinessObjects.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Counter", "Counter")
                        .WithMany("Orders")
                        .HasForeignKey("CounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Counter");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany("OrderDetails")
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Promotion", b =>
                {
                    b.HasOne("BusinessObjects.Account", null)
                        .WithMany("Promotions")
                        .HasForeignKey("AccountId");

                    b.HasOne("BusinessObjects.Customer", null)
                        .WithMany("Promotions")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyJewelry", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithOne("WarrantyJewelry")
                        .HasForeignKey("BusinessObjects.WarrantyJewelry", "JewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyOrder", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany()
                        .HasForeignKey("JewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.OrderDetail", "OrderDetail")
                        .WithOne("WarrantyOrder")
                        .HasForeignKey("BusinessObjects.WarrantyOrder", "OrderDetailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");

                    b.Navigation("Order");

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyRequest", b =>
                {
                    b.HasOne("BusinessObjects.Customer", "Customer")
                        .WithMany("WarrantyRequests")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.WarrantyOrder", "WarrantyOrder")
                        .WithOne("WarrantyRequest")
                        .HasForeignKey("BusinessObjects.WarrantyRequest", "WarrantyOrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("WarrantyOrder");
                });

            modelBuilder.Entity("JewelryPromotion", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", null)
                        .WithMany()
                        .HasForeignKey("JewelriesJewelryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Promotion", null)
                        .WithMany()
                        .HasForeignKey("PromotionsPromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderDetailPromotion", b =>
                {
                    b.HasOne("BusinessObjects.OrderDetail", null)
                        .WithMany()
                        .HasForeignKey("OrderDetailsOrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Promotion", null)
                        .WithMany()
                        .HasForeignKey("PromotionsPromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderPromotion", b =>
                {
                    b.HasOne("BusinessObjects.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Promotion", null)
                        .WithMany()
                        .HasForeignKey("PromotionsPromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Account", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Promotions");
                });

            modelBuilder.Entity("BusinessObjects.Counter", b =>
                {
                    b.Navigation("Jewelries");

                    b.Navigation("Materials");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Promotions");

                    b.Navigation("WarrantyRequests");
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.Navigation("JewelryMaterials");

                    b.Navigation("OrderDetails");

                    b.Navigation("WarrantyJewelry");
                });

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.Navigation("JewelryMaterials");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.Navigation("WarrantyOrder");
                });

            modelBuilder.Entity("BusinessObjects.WarrantyOrder", b =>
                {
                    b.Navigation("WarrantyRequest");
                });
#pragma warning restore 612, 618
        }
    }
}
