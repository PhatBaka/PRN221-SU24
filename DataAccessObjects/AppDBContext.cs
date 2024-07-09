using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessObjects
{
    public class AppDBContext : DbContext
    {
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Jewelry>? Jewelries { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        public DbSet<Material>? Materials { get; set; }

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
            base.OnModelCreating(modelBuilder);

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
        }
    }
}
