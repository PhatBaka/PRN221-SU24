using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class initdatav5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "MaterialId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 23, 9, 30, 49, 741, DateTimeKind.Local).AddTicks(8704));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 23, 9, 30, 49, 741, DateTimeKind.Local).AddTicks(8716));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 23, 9, 30, 49, 741, DateTimeKind.Local).AddTicks(8718));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 23, 9, 30, 49, 741, DateTimeKind.Local).AddTicks(8719));

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialId",
                keyValue: 1,
                column: "MaterialName",
                value: "Gold");

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialId",
                keyValue: 2,
                column: "MaterialName",
                value: "Silver");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5106));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5120));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 28, 53, 388, DateTimeKind.Local).AddTicks(5121));

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialId",
                keyValue: 1,
                column: "MaterialName",
                value: "Gold 24K");

            migrationBuilder.UpdateData(
                table: "Material",
                keyColumn: "MaterialId",
                keyValue: 2,
                column: "MaterialName",
                value: "Silver 925");

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialId", "BidPrice", "Clarity", "Color", "Description", "GemCertificate", "IsMetail", "MaterialCost", "MaterialImage", "MaterialName", "MaterialStatus", "OfferPrice", "Purity", "Sharp", "StockQuantity" },
                values: new object[] { 3, 3000000m, 10, "White", "High purity platinum", null, true, 3000000.0, null, "Platinum", null, 3500000m, 95.0, null, 0m });
        }
    }
}
