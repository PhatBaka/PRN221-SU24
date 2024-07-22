using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class Initdatabase3 : Migration
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    GemCertificate = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    StockQuantity = table.Column<decimal>(type: "decimal", nullable: false),
                    MaterialStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    PromotionDetailId = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_OrderDetail_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId");
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
                    { 1, new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4945), "adminA@mail.com", "Admin A", 0, "123", "1234567890", 3 },
                    { 2, new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4967), "managerA@mail.com", "Manager A", 0, "123", "0987654321", 1 },
                    { 3, new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4969), "staffA@mail.com", "Staff A", 0, "123", "1122334455", 0 },
                    { 4, new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4970), "staffB@mail.com", "Staff B", 0, "123", "5566778899", 0 }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialId", "BidPrice", "Clarity", "Color", "Description", "GemCertificate", "IsMetail", "MaterialCost", "MaterialImage", "MaterialName", "MaterialStatus", "OfferPrice", "Purity", "Sharp", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 2000000m, 10, "Yellow", "Pure gold with 99.99% purity", null, true, 2000000.0, null, "Gold 24K", null, 2500000m, 99.989997863769531, null, 0m },
                    { 2, 25000m, 10, "Silver", "Sterling silver with 92.5% purity", null, true, 25000.0, null, "Silver 925", null, 35000m, 92.5, null, 0m },
                    { 3, 3000000m, 10, "White", "High purity platinum", null, true, 3000000.0, null, "Platinum", null, 3500000m, 95.0, null, 0m },
                    { 4, 2200000m, 10, "Silver", "High purity palladium", null, true, 2200000.0, null, "Palladium", null, 2700000m, 95.0, null, 0m },
                    { 5, 0m, 1, "Colorless", "High quality diamond with excellent clarity", null, false, 5000000.0, null, "Diamond", null, 0m, 100.0, "Round Brilliant", 0m },
                    { 6, 0m, 3, "Red", "High quality ruby with vivid red color", null, false, 3000000.0, null, "Ruby", null, 0m, 100.0, "Oval", 0m },
                    { 7, 0m, 4, "Blue", "High quality sapphire with deep blue color", null, false, 2500000.0, null, "Sapphire", null, 0m, 100.0, "Cushion", 0m },
                    { 8, 0m, 5, "Green", "High quality emerald with vivid green color", null, false, 4000000.0, null, "Emerald", null, 0m, 100.0, "Emerald Cut", 0m }
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
                name: "IX_OrderDetail_MaterialId",
                table: "OrderDetail",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_PromotionDetailId",
                table: "OrderDetail",
                column: "PromotionDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_PromotionId",
                table: "OrderDetail",
                column: "PromotionId");

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
