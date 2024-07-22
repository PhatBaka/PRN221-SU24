using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessObjects
{
    public class AppDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionDetail> PromotionDetails { get; set; }
        public DbSet<JewelryMaterial> JewelryMaterials { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<WarrantyHistory> WarrantyHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string is null");
                optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.UseSqlServer(connectionString);
				//optionsBuilder.UseSqlServer("Server=(local);Database=JewelryDB;Uid=sa;Pwd=hanh3533.;");
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.AccountId).ValueGeneratedOnAdd();
                // Define other properties for Account entity
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.CategoryId);
                entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
                // Define other properties for Category entity
            });

            modelBuilder.Entity<Jewelry>(entity =>
            {
                entity.ToTable("Jewelry");
                entity.HasKey(e => e.JewelryId);
                entity.Property(e => e.JewelryId).ValueGeneratedOnAdd();
                entity.Property(e => e.JewelryName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
                entity.Property(e => e.TotalWeight).HasColumnType("decimal(18,2)").HasColumnName("Weight");
                entity.Property(e => e.LaborPrice).HasColumnType("decimal(18,2)").HasColumnName("WorkPrice");
                entity.Property(e => e.Quantity).HasColumnType("int");
                entity.Property(e => e.MarkupPercentage).HasColumnType("float");
                entity.Property(e => e.CategoryId).HasColumnType("int");
                entity.Property(e => e.JewelryImage).HasColumnType("varbinary(max)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Jewelries)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.JewelryMaterials)
                    .WithOne(p => p.Jewelry)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.OrderDetails)
                    .WithOne(p => p.Jewelry)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.PromotionDetails)
                    .WithOne(p => p.Jewelry)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Warranties)
                    .WithOne(p => p.Jewelry)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<JewelryMaterial>(entity =>
            {
                entity.ToTable("JewelryMaterial");
                entity.HasKey(e => new { e.JewelryId, e.MaterialId });
                entity.Property(e => e.JewelryWeight).HasColumnType("float");

                entity.HasOne(d => d.Jewelry)
                    .WithMany(p => p.JewelryMaterials)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.JewelryMaterials)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");
                entity.HasKey(e => e.MaterialId);
                entity.Property(e => e.MaterialId).ValueGeneratedOnAdd();
                entity.Property(e => e.IsMetail).HasColumnType("bit");
                entity.Property(e => e.MaterialCost).HasColumnType("float");
                entity.Property(e => e.MaterialName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId).ValueGeneratedOnAdd();
                entity.Property(e => e.OrderDate).HasColumnType("datetime2").IsRequired();
                entity.Property(e => e.OrderType).HasColumnType("int").IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.OrderDetails)
                    .WithOne(p => p.Order)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Warranties)
                    .WithOne(p => p.Order)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(e => e.OrderDetailId);

                entity.Property(e => e.OrderId).HasColumnType("int");
                entity.Property(e => e.JewelryId).HasColumnType("int");
                entity.Property(e => e.PromotionDetailId).HasColumnType("int");
                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Quantity).HasColumnType("int").IsRequired();
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Jewelry)
                    .WithMany(d => d.OrderDetails)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PromotionDetail)
                    .WithOne(d => d.OrderDetail)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");
                entity.HasKey(e => e.PromotionId);
                entity.Property(e => e.PromotionId).ValueGeneratedOnAdd();
                entity.Property(e => e.PromotionName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.StartDate).HasColumnType("datetime2").IsRequired();
                entity.Property(e => e.EndDate).HasColumnType("datetime2").IsRequired();

                entity.HasMany(d => d.PromotionDetails)
                    .WithOne(p => p.Promotion)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PromotionDetail>(entity =>
            {
                entity.ToTable("PromotionDetail");
                entity.HasKey(e => e.PromotionDetailId);

                entity.Property(e => e.PromotionDetailId).ValueGeneratedOnAdd();
                entity.Property(e => e.JewelryId).HasColumnType("int").IsRequired();
                entity.Property(e => e.DiscountPercent).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.PromotionDetails)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Jewelry)
                    .WithMany(d => d.PromotionDetails)
                    .HasForeignKey(d => d.JewelryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.OrderDetail)
                    .WithOne(d => d.PromotionDetail)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Warranty>(entity =>
            {
                entity.ToTable("Warranties");
                entity.HasKey(e => e.WarrantyId);
                entity.Property(e => e.WarrantyId).ValueGeneratedOnAdd();
                entity.Property(e => e.JewelryId).HasColumnType("int").IsRequired();
                entity.Property(e => e.OrderId).HasColumnType("int").IsRequired();
                entity.Property(e => e.WarrantyPeriod).HasColumnType("float").IsRequired();

                entity.HasOne(d => d.Jewelry)
                    .WithMany(p => p.Warranties)
					.HasForeignKey(d => d.JewelryId)
					.OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Warranties)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WarrantyHistory>(entity =>
            {
                entity.ToTable("WarrantyHistories");
                entity.HasKey(e => e.WarrantyHistoryId);
                entity.Property(e => e.WarrantyHistoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.WarrantyId).HasColumnType("int").IsRequired();
                entity.Property(e => e.ReceivedDate).HasColumnType("datetime2");
                entity.Property(e => e.ReturnDate).HasColumnType("datetime2");

                entity.HasOne(d => d.Warranty)
                    .WithMany(p => p.WarrantyHistories)
                    .HasForeignKey(d => d.WarrantyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedData();
        }
    }
}
