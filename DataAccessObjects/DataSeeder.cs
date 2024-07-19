//using BusinessObjects;
//using BusinessObjects.Enums;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessObjects
//{
//    public static class DataSeeder
//    {
//        public static void SeedData(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.CategoriesSeedData();
//            modelBuilder.MaterialDataSeed();
//            modelBuilder.JewelriesDataSeed();
//            modelBuilder.JewelryMaterialDataSeed();
//            modelBuilder.AccountDataSeed();
//            modelBuilder.OrderDataSeed();
//            modelBuilder.OrderDetailDataSeed();
//        }
//        public static void CategoriesSeedData(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Category>().HasData(
//                // Jewelry Types
//                new Category { CategoryId = 1, CategoryName = "Jewelry Type" },
//                new Category { CategoryId = 2, CategoryName = "Rings" },
//                new Category { CategoryId = 3, CategoryName = "Necklaces" },
//                new Category { CategoryId = 4, CategoryName = "Earrings" },
//                new Category { CategoryId = 5, CategoryName = "Bracelets" },
//                new Category { CategoryId = 6, CategoryName = "Pendants" },
//                new Category { CategoryId = 7, CategoryName = "Brooches" }
//                );
//        }

//        public static void JewelriesDataSeed(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Jewelry>().HasData(

//                new Jewelry
//                {
//                    JewelryId = 1,
//                    JewelryName = "Gold Pendant Necklace",
//                    Description = "Beautiful pendant necklace crafted in 14k gold with intricate design.",
//                    TotalWeight = 250,
//                    LaborPrice = 4500000,
//                    Quantity = 1,
//                    MarkupPercentage = 0.18,
//                    CategoryId = 2,
//                    StatusSale = BusinessObjects.Enums.StatusSale.IN_STOCK,

//                },
//                new Jewelry
//                {
//                    JewelryId = 2,
//                    JewelryName = "Sterling Silver Hoop Earrings",
//                    Description = "Classic hoop earrings crafted in sterling silver for everyday elegance.",
//                    TotalWeight = 500,
//                    LaborPrice = 2500000,
//                    Quantity = 2,
//                    MarkupPercentage = 0.15,
//                    CategoryId = 3,
//                    StatusSale = BusinessObjects.Enums.StatusSale.IN_STOCK,

//                },
//                new Jewelry
//                {
//                    JewelryId = 3,
//                    JewelryName = "Diamond Tennis Bracelet",
//                    Description = "Luxurious diamond tennis bracelet set in 18k white gold.",
//                    TotalWeight = 100,
//                    LaborPrice = 1800000,
//                    Quantity = 3,
//                    MarkupPercentage = 0.12,
//                    CategoryId = 4,
//                    StatusSale = BusinessObjects.Enums.StatusSale.IN_STOCK,

//                },
//                new Jewelry
//                {

//                    JewelryId = 4,
//                    JewelryName = "Cultured Pearl Pendant",
//                    Description = "Elegant cultured pearl pendant with 18k rose gold setting.",
//                    TotalWeight = 150,
//                    LaborPrice = 3200000,
//                    Quantity = 1,
//                    MarkupPercentage = 0.2,
//                    CategoryId = 5,
//                    StatusSale = BusinessObjects.Enums.StatusSale.IN_STOCK,

//                });
//        }

//        public static void MaterialDataSeed(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Material>().HasData(

//                new Material
//                {
//                    MaterialId = 1,
//                    MaterialName = "Gold 24K",
//                    IsMetail = true,
//                    Purity = 99.99F,
//                    MaterialCost = 2000000, // assuming this is the cost per gram
//                    Description = "Pure gold with 99.99% purity",
//                    BidPrice = 2000000,
//                    OfferPrice = 2500000,
//                    Clarity = ClarityEnum.NONE,
//                    Color = "Yellow",
//                    Sharp = null,
//                    MaterialImage = null,
//                    GemCertificate = null,
//                },

//                new Material
//                {
//                    MaterialId = 2,
//                    MaterialName = "Silver 925",
//                    IsMetail = true,
//                    Purity = 92.5F,
//                    MaterialCost = 25000, // assuming this is the cost per gram
//                    Description = "Sterling silver with 92.5% purity",
//                    BidPrice = 25000,
//                    OfferPrice = 35000,
//                    Clarity = ClarityEnum.NONE,
//                    Color = "Silver",
//                    Sharp = null,
//                    MaterialImage = null,
//                    GemCertificate = null,
//                },

//                new Material
//                {
//                    MaterialId = 3,
//                    MaterialName = "Platinum",
//                    IsMetail = true,
//                    Purity = 95.0F,
//                    MaterialCost = 3000000, // assuming this is the cost per gram
//                    Description = "High purity platinum",
//                    BidPrice = 3000000,
//                    OfferPrice = 3500000,
//                    Clarity = ClarityEnum.NONE,
//                    Color = "White",
//                    Sharp = null,
//                    MaterialImage = null,
//                    GemCertificate = null,

//                },

//                new Material
//                {
//                    MaterialId = 4,
//                    MaterialName = "Palladium",
//                    IsMetail = true,
//                    Purity = 95.0F,
//                    MaterialCost = 2200000, // assuming this is the cost per gram
//                    Description = "High purity palladium",
//                    BidPrice = 2200000,
//                    OfferPrice = 2700000,
//                    Clarity = ClarityEnum.NONE,
//                    Color = "Silver",
//                    Sharp = null,
//                    MaterialImage = null,
//                    GemCertificate = null,
//                },

//                // Gemstone Types

//                new Material
//                {
//                    MaterialId = 5,
//                    MaterialName = "Diamond",
//                    IsMetail = false,
//                    Purity = 100.0F, // typically, purity isn't used for gemstones but assuming 100% here
//                    MaterialCost = 5000000, // cost per carat
//                    Description = "High quality diamond with excellent clarity",
//                    BidPrice = 0,
//                    OfferPrice = 0,
//                    Clarity = ClarityEnum.VVS1,
//                    Color = "Colorless",
//                    Sharp = "Round Brilliant",
//                    MaterialImage = null,
//                    GemCertificate = null,
//                },

//                new Material
//                {
//                    MaterialId = 6,
//                    MaterialName = "Ruby",
//                    IsMetail = false,
//                    Purity = 100.0F, // assuming 100% here
//                    MaterialCost = 3000000, // cost per carat
//                    Description = "High quality ruby with vivid red color",
//                    BidPrice = 0,
//                    OfferPrice = 0,
//                    Clarity = ClarityEnum.VS1,
//                    Color = "Red",
//                    Sharp = "Oval",
//                    MaterialImage = null,
//                    GemCertificate = null
//                },

//                new Material
//                {
//                    MaterialId = 7,
//                    MaterialName = "Sapphire",
//                    IsMetail = false,
//                    Purity = 100.0F, // assuming 100% here
//                    MaterialCost = 2500000, // cost per carat
//                    Description = "High quality sapphire with deep blue color",
//                    BidPrice = 0,
//                    OfferPrice = 0,
//                    Clarity = ClarityEnum.VS2,
//                    Color = "Blue",
//                    Sharp = "Cushion",
//                    MaterialImage = null,
//                    GemCertificate = null
//                },

//                new Material
//                {
//                    MaterialId = 8,
//                    MaterialName = "Emerald",
//                    IsMetail = false,
//                    Purity = 100.0F, // assuming 100% here
//                    MaterialCost = 4000000, // cost per carat
//                    Description = "High quality emerald with vivid green color",
//                    BidPrice = 0,
//                    OfferPrice = 0,
//                    Clarity = ClarityEnum.SI1,
//                    Color = "Green",
//                    Sharp = "Emerald Cut",
//                    MaterialImage = null,
//                    GemCertificate = null
//                });
//        }

//        public static void JewelryMaterialDataSeed(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<JewelryMaterial>().HasData(
//            new JewelryMaterial
//            {
//                JewelryId = 1,
//                MaterialId = 1, // Gold 24K
//                JewelryWeight = 200
//            },
//            new JewelryMaterial
//            {
//                JewelryId = 1,
//                MaterialId = 2, // Silver 925
//                JewelryWeight = 5
//            },
//            new JewelryMaterial
//            {
//                JewelryId = 2,
//                MaterialId = 2, // Silver 925
//                JewelryWeight = 500
//            },
//            new JewelryMaterial
//            {
//                JewelryId = 3,
//                MaterialId = 5, // Diamond
//                JewelryWeight = 10 // Assuming this is in carats
//            },
//            new JewelryMaterial
//            {
//                JewelryId = 4,
//                MaterialId = 6, // Ruby
//                JewelryWeight = 50 // Assuming this is in carats
//            });
//        }

//        public static void AccountDataSeed(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Account>().HasData(
//            new Account
//            {
//                AccountId = 1,
//                Email = "customer1@example.com",
//                Password = "123", // Ensure to hash passwords in a real application
//                PhoneNumber = "1234567890",
//                CreatedDate = DateTime.Now,
//                FullName = "Customer One",
//                Role = AccountRole.CUSTOMER,
//                ObjectStatus = ObjectStatus.ACTIVE
//            },
//            new Account
//            {
//                AccountId = 2,
//                Email = "customer2@example.com",
//                Password = "123", // Ensure to hash passwords in a real application
//                PhoneNumber = "0987654321",
//                CreatedDate = DateTime.Now,
//                FullName = "Customer Two",
//                Role = AccountRole.CUSTOMER,
//                ObjectStatus = ObjectStatus.ACTIVE
//            },
//            new Account
//            {
//                AccountId = 3,
//                Email = "customer3@example.com",
//                Password = "123", // Ensure to hash passwords in a real application
//                PhoneNumber = "1122334455",
//                CreatedDate = DateTime.Now,
//                FullName = "Customer Three",
//                Role = AccountRole.CUSTOMER,
//                ObjectStatus = ObjectStatus.ACTIVE
//            },
//            new Account
//            {
//                AccountId = 4,
//                Email = "customer4@example.com",
//                Password = "123", // Ensure to hash passwords in a real application
//                PhoneNumber = "5566778899",
//                CreatedDate = DateTime.Now,
//                FullName = "Customer Four",
//                Role = AccountRole.CUSTOMER,
//                ObjectStatus = ObjectStatus.ACTIVE
//            }
//        );
//        }

//        public static void OrderDataSeed(this ModelBuilder modelBuilder)
//        {
//            // Add data seeding for Order
//            modelBuilder.Entity<Order>().HasData(
//                new Order
//                {
//                    OrderId = 1,
//                    OrderDate = DateTime.Now,
//                    OrderType = OrderEnum.NEW,
//                    CustomerId = 1
//                },
//                new Order
//                {
//                    OrderId = 2,
//                    OrderDate = DateTime.Now,
//                    OrderType = OrderEnum.NEW,
//                    CustomerId = 2
//                },
//                new Order
//                {
//                    OrderId = 3,
//                    OrderDate = DateTime.Now,
//                    OrderType = OrderEnum.NEW,
//                    CustomerId = 3
//                },
//                new Order
//                {
//                    OrderId = 4,
//                    OrderDate = DateTime.Now,
//                    OrderType = OrderEnum.NEW,
//                    CustomerId = 4
//                }
//            );
//        }

//        public static void OrderDetailDataSeed(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<OrderDetail>().HasData(
//                 new OrderDetail
//                 {
//                     OrderDetailId = 1,
//                     OrderId = 1,
//                     JewelryId = 1,
//                     PromotionDetailId = null,
//                     DiscountPercent = 10.0,
//                     Quantity = 2,
//                     UnitPrice = 150.0
//                 },
//              new OrderDetail
//              {
//                  OrderDetailId = 2,
//                  OrderId = 1,
//                  JewelryId = 2,
//                  PromotionDetailId = null,
//                  DiscountPercent = 5.0,
//                  Quantity = 1,
//                  UnitPrice = 200.0
//              },
//              new OrderDetail
//              {
//                  OrderDetailId = 3,
//                  OrderId = 2,
//                  JewelryId = 1,
//                  PromotionDetailId = null,
//                  DiscountPercent = 15.0,
//                  Quantity = 3,
//                  UnitPrice = 120.0
//              },
//              new OrderDetail
//              {
//                  OrderDetailId = 4,
//                  OrderId = 3,
//                  JewelryId = 3,
//                  PromotionDetailId = null,
//                  DiscountPercent = 20.0,
//                  Quantity = 1,
//                  UnitPrice = 300.0
//              }
//           );
//        }
//    }
//}
