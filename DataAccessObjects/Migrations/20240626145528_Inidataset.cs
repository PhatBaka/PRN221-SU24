using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class Inidataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JewelryMaterial",
                keyColumns: new[] { "JewelryId", "MaterialId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "JewelryMaterial",
                keyColumns: new[] { "JewelryId", "MaterialId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "JewelryMaterial",
                keyColumns: new[] { "JewelryId", "MaterialId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "JewelryMaterial",
                keyColumns: new[] { "JewelryId", "MaterialId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "JewelryMaterial",
                keyColumns: new[] { "JewelryId", "MaterialId" },
                keyValues: new object[] { 4, 6 });
        }
    }
}
