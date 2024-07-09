using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class Testttttttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaterialPrice",
                table: "Jewelry",
                newName: "TotalSellMaterialPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "BuyJewelryPrice",
                table: "Jewelry",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellJewelryPrice",
                table: "Jewelry",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBuyMaterialPrice",
                table: "Jewelry",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyJewelryPrice",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "SellJewelryPrice",
                table: "Jewelry");

            migrationBuilder.DropColumn(
                name: "TotalBuyMaterialPrice",
                table: "Jewelry");

            migrationBuilder.RenameColumn(
                name: "TotalSellMaterialPrice",
                table: "Jewelry",
                newName: "MaterialPrice");
        }
    }
}
