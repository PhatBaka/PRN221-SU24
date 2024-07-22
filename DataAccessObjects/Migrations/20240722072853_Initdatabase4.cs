using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class Initdatabase4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4945));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4967));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4969));

            migrationBuilder.UpdateData(
                table: "Account",
                keyColumn: "AccountId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 22, 14, 24, 32, 581, DateTimeKind.Local).AddTicks(4970));
        }
    }
}
