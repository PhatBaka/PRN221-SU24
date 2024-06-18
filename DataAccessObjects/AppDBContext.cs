using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessObjects
{
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jewelry>()
                .HasOne(j => j.WarrantyJewelry)
                .WithOne(wj => wj.Jewelry)
                .HasForeignKey<WarrantyJewelry>(wj => wj.JewelryId);

            modelBuilder.Entity<JewelryMaterial>()
                .HasKey(jm => new { jm.JewelryId, jm.MaterialId });

            modelBuilder.Entity<JewelryMaterial>()
                .HasOne(jm => jm.Jewelry)
                .WithMany(j => j.JewelryMaterials)
                .HasForeignKey(jm => jm.JewelryId);

            modelBuilder.Entity<JewelryMaterial>()
                .HasOne(jm => jm.Material)
                .WithMany(m => m.JewelryMaterials)
                .HasForeignKey(jm => jm.MaterialId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.WarrantyOrder)
                .WithOne(wo => wo.OrderDetail)
                .HasForeignKey<WarrantyOrder>(wo => wo.OrderDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WarrantyOrder>()
                .HasOne(wo => wo.WarrantyRequest)
                .WithOne(wr => wr.WarrantyOrder)
                .HasForeignKey<WarrantyRequest>(wr => wr.WarrantyOrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Counter>? Counters { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Jewelry>? Jewelries { get; set; }
        public DbSet<JewelryMaterial>? JewelryMaterials { get; set; }
        public DbSet<Material>? Materials { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Promotion>? Promotions { get; set; }
        public DbSet<WarrantyJewelry>? WarrantyJewelries { get; set; }
        public DbSet<WarrantyOrder>? WarrantyOrders { get; set; }
        public DbSet<WarrantyRequest>? WarrantyRequests { get; set; }
    }
}
