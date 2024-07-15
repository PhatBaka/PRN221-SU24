using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ObjectStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsMetail = table.Column<bool>(type: "bit", nullable: false),
                    MaterialCost = table.Column<double>(type: "float", nullable: false),
                    Clarity = table.Column<int>(type: "int", nullable: false),
                    Purity = table.Column<double>(type: "float", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sharp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BidPrice = table.Column<decimal>(type: "money", nullable: false),
                    OfferPrice = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    GemCertificate = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    PromotionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Account_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jewelry",
                columns: table => new
                {
                    JewelryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MarkupPercentage = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    JewelryImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StatusSale = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jewelry", x => x.JewelryId);
                    table.ForeignKey(
                        name: "FK_Jewelry_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JewelryMaterial",
                columns: table => new
                {
                    JewelryId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    JewelryWeight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryMaterial", x => new { x.JewelryId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_JewelryMaterial_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JewelryMaterial_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDetail",
                columns: table => new
                {
                    PromotionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelryId = table.Column<int>(type: "int", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDetail", x => x.PromotionDetailId);
                    table.ForeignKey(
                        name: "FK_PromotionDetail_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PromotionDetail_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warranties",
                columns: table => new
                {
                    WarrantyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarrantyPeriod = table.Column<double>(type: "float", nullable: false),
                    PeriodUnitmeasure = table.Column<int>(type: "int", nullable: false),
                    ActiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JewelryId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    WarrantyStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranties", x => x.WarrantyId);
                    table.ForeignKey(
                        name: "FK_Warranties_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warranties_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    JewelryId = table.Column<int>(type: "int", nullable: false),
                    PromotionDetailId = table.Column<int>(type: "int", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_PromotionDetail_PromotionDetailId",
                        column: x => x.PromotionDetailId,
                        principalTable: "PromotionDetail",
                        principalColumn: "PromotionDetailId");
                });

            migrationBuilder.CreateTable(
                name: "WarrantyHistories",
                columns: table => new
                {
                    WarrantyHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    RequireDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    ResultReport = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    WarrantyId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyHistories", x => x.WarrantyHistoryId);
                    table.ForeignKey(
                        name: "FK_WarrantyHistories_Warranties_WarrantyId",
                        column: x => x.WarrantyId,
                        principalTable: "Warranties",
                        principalColumn: "WarrantyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "CreatedDate", "Email", "FullName", "ObjectStatus", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8353), "customer1@example.com", "Customer One", 0, "123", "1234567890", 2 },
                    { 2, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8364), "customer2@example.com", "Customer Two", 0, "123", "0987654321", 2 },
                    { 3, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8366), "customer3@example.com", "Customer Three", 0, "123", "1122334455", 2 },
                    { 4, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8367), "customer4@example.com", "Customer Four", 0, "123", "5566778899", 2 }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Jewelry Type" },
                    { 2, "Rings" },
                    { 3, "Necklaces" },
                    { 4, "Earrings" },
                    { 5, "Bracelets" },
                    { 6, "Pendants" },
                    { 7, "Brooches" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialId", "BidPrice", "Clarity", "Color", "Description", "GemCertificate", "IsMetail", "MaterialCost", "MaterialImage", "MaterialName", "OfferPrice", "Purity", "Sharp" },
                values: new object[,]
                {
                    { 1, 2000000m, 10, "Yellow", "Pure gold with 99.99% purity", null, true, 2000000.0, null, "Gold 24K", 2500000m, 99.989997863769531, null },
                    { 2, 25000m, 10, "Silver", "Sterling silver with 92.5% purity", null, true, 25000.0, null, "Silver 925", 35000m, 92.5, null },
                    { 3, 3000000m, 10, "White", "High purity platinum", null, true, 3000000.0, null, "Platinum", 3500000m, 95.0, null },
                    { 4, 2200000m, 10, "Silver", "High purity palladium", null, true, 2200000.0, null, "Palladium", 2700000m, 95.0, null },
                    { 5, 0m, 1, "Colorless", "High quality diamond with excellent clarity", null, false, 5000000.0, null, "Diamond", 0m, 100.0, "Round Brilliant" },
                    { 6, 0m, 3, "Red", "High quality ruby with vivid red color", null, false, 3000000.0, null, "Ruby", 0m, 100.0, "Oval" },
                    { 7, 0m, 4, "Blue", "High quality sapphire with deep blue color", null, false, 2500000.0, null, "Sapphire", 0m, 100.0, "Cushion" },
                    { 8, 0m, 5, "Green", "High quality emerald with vivid green color", null, false, 4000000.0, null, "Emerald", 0m, 100.0, "Emerald Cut" }
                });

            migrationBuilder.InsertData(
                table: "Jewelry",
                columns: new[] { "JewelryId", "CategoryId", "Description", "JewelryImage", "JewelryName", "WorkPrice", "MarkupPercentage", "Quantity", "StatusSale", "Weight" },
                values: new object[,]
                {
                    { 1, 2, "Beautiful pendant necklace crafted in 14k gold with intricate design.", null, "Gold Pendant Necklace", 4500000m, 0.17999999999999999, 1, 0, 250m },
                    { 2, 3, "Classic hoop earrings crafted in sterling silver for everyday elegance.", null, "Sterling Silver Hoop Earrings", 2500000m, 0.14999999999999999, 2, 0, 500m },
                    { 3, 4, "Luxurious diamond tennis bracelet set in 18k white gold.", null, "Diamond Tennis Bracelet", 1800000m, 0.12, 3, 0, 100m },
                    { 4, 5, "Elegant cultured pearl pendant with 18k rose gold setting.", null, "Cultured Pearl Pendant", 3200000m, 0.20000000000000001, 1, 0, 150m }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "OrderType" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8382), 0 },
                    { 2, 2, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8384), 0 },
                    { 3, 3, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8384), 0 },
                    { 4, 4, new DateTime(2024, 7, 15, 21, 54, 42, 907, DateTimeKind.Local).AddTicks(8385), 0 }
                });

            migrationBuilder.InsertData(
                table: "JewelryMaterial",
                columns: new[] { "JewelryId", "MaterialId", "JewelryWeight" },
                values: new object[,]
                {
                    { 1, 1, 200.0 },
                    { 1, 2, 5.0 },
                    { 2, 2, 500.0 },
                    { 3, 5, 10.0 },
                    { 4, 6, 50.0 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetail",
                columns: new[] { "OrderDetailId", "DiscountPercent", "JewelryId", "OrderId", "PromotionDetailId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 10m, 1, 1, null, 2, 150m },
                    { 2, 5m, 2, 1, null, 1, 200m },
                    { 3, 15m, 1, 2, null, 3, 120m },
                    { 4, 20m, 3, 3, null, 1, 300m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jewelry_CategoryId",
                table: "Jewelry",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JewelryMaterial_MaterialId",
                table: "JewelryMaterial",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_JewelryId",
                table: "OrderDetail",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_PromotionDetailId",
                table: "OrderDetail",
                column: "PromotionDetailId",
                unique: true,
                filter: "[PromotionDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_JewelryId",
                table: "PromotionDetail",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PromotionId",
                table: "PromotionDetail",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_OrderId",
                table: "Warranties",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistories_WarrantyId",
                table: "WarrantyHistories",
                column: "WarrantyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JewelryMaterial");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "WarrantyHistories");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "PromotionDetail");

            migrationBuilder.DropTable(
                name: "Warranties");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "Jewelry");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
