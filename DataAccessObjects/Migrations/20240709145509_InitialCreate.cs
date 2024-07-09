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
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Jewelry",
                columns: table => new
                {
                    JewelryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    JewelryName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ManufacturingFees = table.Column<decimal>(type: "money", nullable: true),
                    JewelryType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalGemWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalMetalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaterialPrice = table.Column<decimal>(type: "money", nullable: false),
                    JewelryCategory = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    JewelryImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TotalSellGemPrice = table.Column<decimal>(type: "money", nullable: false),
                    TotalBuyGemPrice = table.Column<decimal>(type: "money", nullable: false),
                    TotalSellMetalPrice = table.Column<decimal>(type: "money", nullable: false),
                    TotalBuyMetalPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jewelry", x => x.JewelryId);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CertificateCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SellPrice = table.Column<decimal>(type: "money", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "money", nullable: false),
                    BidPrice = table.Column<decimal>(type: "money", nullable: false),
                    AskPrice = table.Column<decimal>(type: "money", nullable: false),
                    MaterialImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CertificateImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsMetal = table.Column<bool>(type: "bit", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Purity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Clarity = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Cut = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Shape = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "money", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "money", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    OrderType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JewelryMaterial",
                columns: table => new
                {
                    JewelriesJewelryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialsMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryMaterial", x => new { x.JewelriesJewelryId, x.MaterialsMaterialId });
                    table.ForeignKey(
                        name: "FK_JewelryMaterial_Jewelry_JewelriesJewelryId",
                        column: x => x.JewelriesJewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JewelryMaterial_Material_MaterialsMaterialId",
                        column: x => x.MaterialsMaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JewelryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "money", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "money", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JewelryMaterial_MaterialsMaterialId",
                table: "JewelryMaterial",
                column: "MaterialsMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JewelryMaterial");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Jewelry");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
