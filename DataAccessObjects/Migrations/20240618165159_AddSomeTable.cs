using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class AddSomeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jewelry_Category_CategoryId",
                table: "Jewelry");

            migrationBuilder.DropForeignKey(
                name: "FK_Jewelry_Promotion_PromotionId",
                table: "Jewelry");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Promotion_PromotionId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "WarrantyHistory");

            migrationBuilder.DropTable(
                name: "Warranty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_Jewelry_CategoryId",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "DiscountStatus",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Dicount",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "StatusSale",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Order",
                newName: "CounterId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PromotionId",
                table: "Order",
                newName: "IX_Order_CounterId");

            migrationBuilder.RenameColumn(
                name: "JewelryWeight",
                table: "JewelryMaterial",
                newName: "MetalWeight");

            migrationBuilder.RenameColumn(
                name: "PromotionId",
                table: "Jewelry",
                newName: "CounterId");

            migrationBuilder.RenameIndex(
                name: "IX_Jewelry_PromotionId",
                table: "Jewelry",
                newName: "IX_Jewelry_CounterId");

            migrationBuilder.AlterColumn<string>(
                name: "PromotionName",
                table: "Promotion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PromotionCode",
                table: "Promotion",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountValue",
                table: "Promotion",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AcceptedPrice",
                table: "Promotion",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Promotion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Promotion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionType",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetail",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "OrderDetail",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "OrderDetail",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "DiscountValue",
                table: "OrderDetail",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "WarrantyOrderId",
                table: "OrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "OrderType",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaterialCost",
                table: "Material",
                type: "money",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "CounterId",
                table: "Material",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialQuantity",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MaterialWeight",
                table: "Material",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "Material",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "WorkPrice",
                table: "Jewelry",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "Jewelry",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "JewelryImage",
                table: "Jewelry",
                type: "image",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Jewelry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ObjectStatus",
                table: "Jewelry",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Account",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ObjectStatus",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Account",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "OrderDetailId");

            migrationBuilder.CreateTable(
                name: "Counter",
                columns: table => new
                {
                    CounterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CounterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Revenue = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counter", x => x.CounterId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalPoint = table.Column<double>(type: "float", nullable: false),
                    ObjectStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "JewelryPromotion",
                columns: table => new
                {
                    JewelriesJewelryId = table.Column<int>(type: "int", nullable: false),
                    PromotionsPromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JewelryPromotion", x => new { x.JewelriesJewelryId, x.PromotionsPromotionId });
                    table.ForeignKey(
                        name: "FK_JewelryPromotion_Jewelry_JewelriesJewelryId",
                        column: x => x.JewelriesJewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JewelryPromotion_Promotion_PromotionsPromotionId",
                        column: x => x.PromotionsPromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailPromotion",
                columns: table => new
                {
                    OrderDetailsOrderDetailId = table.Column<int>(type: "int", nullable: false),
                    PromotionsPromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailPromotion", x => new { x.OrderDetailsOrderDetailId, x.PromotionsPromotionId });
                    table.ForeignKey(
                        name: "FK_OrderDetailPromotion_OrderDetail_OrderDetailsOrderDetailId",
                        column: x => x.OrderDetailsOrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "OrderDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailPromotion_Promotion_PromotionsPromotionId",
                        column: x => x.PromotionsPromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPromotion",
                columns: table => new
                {
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false),
                    PromotionsPromotionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPromotion", x => new { x.OrdersOrderId, x.PromotionsPromotionId });
                    table.ForeignKey(
                        name: "FK_OrderPromotion_Order_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPromotion_Promotion_PromotionsPromotionId",
                        column: x => x.PromotionsPromotionId,
                        principalTable: "Promotion",
                        principalColumn: "PromotionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyJewelry",
                columns: table => new
                {
                    WarrantyJewelryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarrantyMonths = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JewelryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyJewelry", x => x.WarrantyJewelryId);
                    table.ForeignKey(
                        name: "FK_WarrantyJewelry_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyOrder",
                columns: table => new
                {
                    WarrantyOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarrantyPeriod = table.Column<double>(type: "float", nullable: false),
                    JewelryId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    WarrantyRequestId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyOrder", x => x.WarrantyOrderId);
                    table.ForeignKey(
                        name: "FK_WarrantyOrder_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarrantyOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarrantyOrder_OrderDetail_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "OrderDetailId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCounter",
                columns: table => new
                {
                    AccountsAccountId = table.Column<int>(type: "int", nullable: false),
                    CountersCounterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCounter", x => new { x.AccountsAccountId, x.CountersCounterId });
                    table.ForeignKey(
                        name: "FK_AccountCounter_Account_AccountsAccountId",
                        column: x => x.AccountsAccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCounter_Counter_CountersCounterId",
                        column: x => x.CountersCounterId,
                        principalTable: "Counter",
                        principalColumn: "CounterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyRequest",
                columns: table => new
                {
                    WarrantyRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarrantyOrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyRequest", x => x.WarrantyRequestId);
                    table.ForeignKey(
                        name: "FK_WarrantyRequest_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarrantyRequest_WarrantyOrder_WarrantyOrderId",
                        column: x => x.WarrantyOrderId,
                        principalTable: "WarrantyOrder",
                        principalColumn: "WarrantyOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_AccountId",
                table: "Promotion",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_CustomerId",
                table: "Promotion",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_CounterId",
                table: "Material",
                column: "CounterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCounter_CountersCounterId",
                table: "AccountCounter",
                column: "CountersCounterId");

            migrationBuilder.CreateIndex(
                name: "IX_JewelryPromotion_PromotionsPromotionId",
                table: "JewelryPromotion",
                column: "PromotionsPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailPromotion_PromotionsPromotionId",
                table: "OrderDetailPromotion",
                column: "PromotionsPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPromotion_PromotionsPromotionId",
                table: "OrderPromotion",
                column: "PromotionsPromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyJewelry_JewelryId",
                table: "WarrantyJewelry",
                column: "JewelryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyOrder_JewelryId",
                table: "WarrantyOrder",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyOrder_OrderDetailId",
                table: "WarrantyOrder",
                column: "OrderDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyOrder_OrderId",
                table: "WarrantyOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyRequest_CustomerId",
                table: "WarrantyRequest",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyRequest_WarrantyOrderId",
                table: "WarrantyRequest",
                column: "WarrantyOrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jewelry_Counter_CounterId",
                table: "Jewelry",
                column: "CounterId",
                principalTable: "Counter",
                principalColumn: "CounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Counter_CounterId",
                table: "Material",
                column: "CounterId",
                principalTable: "Counter",
                principalColumn: "CounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_AccountId",
                table: "Order",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Counter_CounterId",
                table: "Order",
                column: "CounterId",
                principalTable: "Counter",
                principalColumn: "CounterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Account_AccountId",
                table: "Promotion",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotion_Customers_CustomerId",
                table: "Promotion",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jewelry_Counter_CounterId",
                table: "Jewelry");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Counter_CounterId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_AccountId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Counter_CounterId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customers_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Account_AccountId",
                table: "Promotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Promotion_Customers_CustomerId",
                table: "Promotion");

            migrationBuilder.DropTable(
                name: "AccountCounter");

            migrationBuilder.DropTable(
                name: "JewelryPromotion");

            migrationBuilder.DropTable(
                name: "OrderDetailPromotion");

            migrationBuilder.DropTable(
                name: "OrderPromotion");

            migrationBuilder.DropTable(
                name: "WarrantyJewelry");

            migrationBuilder.DropTable(
                name: "WarrantyRequest");

            migrationBuilder.DropTable(
                name: "Counter");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "WarrantyOrder");

            migrationBuilder.DropIndex(
                name: "IX_Promotion_AccountId",
                table: "Promotion");

            migrationBuilder.DropIndex(
                name: "IX_Promotion_CustomerId",
                table: "Promotion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_Order_AccountId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Material_CounterId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "PromotionType",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "DiscountValue",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "WarrantyOrderId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CounterId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "MaterialQuantity",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "MaterialWeight",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "ObjectStatus",
                table: "Jewelry");

            migrationBuilder.RenameColumn(
                name: "CounterId",
                table: "Order",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CounterId",
                table: "Order",
                newName: "IX_Order_PromotionId");

            migrationBuilder.RenameColumn(
                name: "MetalWeight",
                table: "JewelryMaterial",
                newName: "JewelryWeight");

            migrationBuilder.RenameColumn(
                name: "CounterId",
                table: "Jewelry",
                newName: "PromotionId");

            migrationBuilder.RenameIndex(
                name: "IX_Jewelry_CounterId",
                table: "Jewelry",
                newName: "IX_Jewelry_PromotionId");

            migrationBuilder.AlterColumn<string>(
                name: "PromotionName",
                table: "Promotion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PromotionCode",
                table: "Promotion",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountValue",
                table: "Promotion",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "AcceptedPrice",
                table: "Promotion",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<int>(
                name: "DiscountStatus",
                table: "Promotion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalPrice",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<decimal>(
                name: "Dicount",
                table: "OrderDetail",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "OrderType",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "MaterialCost",
                table: "Material",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "WorkPrice",
                table: "Jewelry",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Jewelry",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<byte[]>(
                name: "JewelryImage",
                table: "Jewelry",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "image",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Jewelry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusSale",
                table: "Jewelry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Account",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Account",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ObjectStatus",
                table: "Account",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Account",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Account",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                columns: new[] { "OrderId", "JewelryId" });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Warranty",
                columns: table => new
                {
                    WarrantyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JewelryId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    WarrantyPeriod = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranty", x => x.WarrantyId);
                    table.ForeignKey(
                        name: "FK_Warranty_Jewelry_JewelryId",
                        column: x => x.JewelryId,
                        principalTable: "Jewelry",
                        principalColumn: "JewelryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warranty_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyHistory",
                columns: table => new
                {
                    WarrantyHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarrantyId = table.Column<int>(type: "int", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyHistory", x => x.WarrantyHistoryId);
                    table.ForeignKey(
                        name: "FK_WarrantyHistory_Warranty_WarrantyId",
                        column: x => x.WarrantyId,
                        principalTable: "Warranty",
                        principalColumn: "WarrantyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jewelry_CategoryId",
                table: "Jewelry",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranty_JewelryId",
                table: "Warranty",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranty_OrderId",
                table: "Warranty",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyHistory_WarrantyId",
                table: "WarrantyHistory",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jewelry_Category_CategoryId",
                table: "Jewelry",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jewelry_Promotion_PromotionId",
                table: "Jewelry",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Promotion_PromotionId",
                table: "Order",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "PromotionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
