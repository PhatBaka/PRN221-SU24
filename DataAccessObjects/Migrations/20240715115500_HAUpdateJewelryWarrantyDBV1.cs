using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class HAUpdateJewelryWarrantyDBV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Jewelry_JewelryId",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "WarrantyHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "WarrantyHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "WarrantyHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "WarrantyHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RequireDescription",
                table: "WarrantyHistories",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResultReport",
                table: "WarrantyHistories",
                type: "nvarchar(max)",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "WarrantyHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDate",
                table: "Warranties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Warranties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PeriodUnitmeasure",
                table: "Warranties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WarrantyStatus",
                table: "Warranties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "CreatedDate", "Email", "FullName", "ObjectStatus", "Password", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1260), "customer1@example.com", "Customer One", 0, "123", "1234567890", 2 },
                    { 2, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1273), "customer2@example.com", "Customer Two", 0, "123", "0987654321", 2 },
                    { 3, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1274), "customer3@example.com", "Customer Three", 0, "123", "1122334455", 2 },
                    { 4, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1275), "customer4@example.com", "Customer Four", 0, "123", "5566778899", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Jewelry Type");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "Rings");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "Necklaces");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CategoryName",
                value: "Earrings");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CategoryName",
                value: "Bracelets");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "CategoryName",
                value: "Pendants");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 7, "Brooches" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "OrderType" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1288), 0 },
                    { 2, 2, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1290), 0 },
                    { 3, 3, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1290), 0 },
                    { 4, 4, new DateTime(2024, 7, 15, 18, 55, 0, 461, DateTimeKind.Local).AddTicks(1291), 0 }
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
                name: "IX_Warranties_JewelryId",
                table: "Warranties",
                column: "JewelryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Jewelry_JewelryId",
                table: "Warranties",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Jewelry_JewelryId",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetail",
                keyColumn: "OrderDetailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "RequireDescription",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "ResultReport",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "status",
                table: "WarrantyHistories");

            migrationBuilder.DropColumn(
                name: "ActiveDate",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "PeriodUnitmeasure",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "WarrantyStatus",
                table: "Warranties");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryName",
                value: "Rings");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryName",
                value: "Necklaces");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryName",
                value: "Earrings");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CategoryName",
                value: "Bracelets");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CategoryName",
                value: "Pendants");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 6,
                column: "CategoryName",
                value: "Brooches");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties",
                column: "JewelryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Jewelry_JewelryId",
                table: "Warranties",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
