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
    [Migration("20240709155726_Testttttttt")]
    partial class Testttttttt
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
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BusinessObjects.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.Property<Guid?>("JewelryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("BuyJewelryPrice")
                        .HasColumnType("money");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("JewelryCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("JewelryImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("JewelryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JewelryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ManufacturingFees")
                        .HasColumnType("money");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SellJewelryPrice")
                        .HasColumnType("money");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalBuyGemPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalBuyMaterialPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalBuyMetalPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalGemWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalMetalWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalSellGemPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalSellMaterialPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalSellMetalPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalWeight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("JewelryId");

                    b.ToTable("Jewelry");
                });

            modelBuilder.Entity("BusinessObjects.JewelryMaterial", b =>
                {
                    b.Property<Guid>("JewelryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("JewelryId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("JewelryMaterial");
                });

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.Property<Guid>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AskPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("BidPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("BuyPrice")
                        .HasColumnType("money");

                    b.Property<string>("CertificateCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("CertificateImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Clarity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMetal")
                        .HasColumnType("bit");

                    b.Property<byte[]>("MaterialImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Purity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SellPrice")
                        .HasColumnType("money");

                    b.Property<string>("Shape")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MaterialId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("DiscountPrice")
                        .HasColumnType("money");

                    b.Property<decimal?>("FinalPrice")
                        .HasColumnType("money");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("money");

                    b.HasKey("OrderId");

                    b.HasIndex("AccountId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("DiscountPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("DiscountValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("money");

                    b.Property<Guid?>("JewelryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("JewelryId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail");
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
                    b.HasOne("BusinessObjects.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BusinessObjects.OrderDetail", b =>
                {
                    b.HasOne("BusinessObjects.Jewelry", "Jewelry")
                        .WithMany("OrderDetails")
                        .HasForeignKey("JewelryId");

                    b.HasOne("BusinessObjects.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jewelry");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BusinessObjects.Account", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BusinessObjects.Jewelry", b =>
                {
                    b.Navigation("JewelryMaterials");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BusinessObjects.Material", b =>
                {
                    b.Navigation("JewelryMaterials");
                });

            modelBuilder.Entity("BusinessObjects.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
