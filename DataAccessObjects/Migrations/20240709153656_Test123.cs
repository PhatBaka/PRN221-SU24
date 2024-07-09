using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class Test123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JewelryMaterial_Jewelry_JewelriesJewelryId",
                table: "JewelryMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_JewelryMaterial_Material_MaterialsMaterialId",
                table: "JewelryMaterial");

            migrationBuilder.RenameColumn(
                name: "MaterialsMaterialId",
                table: "JewelryMaterial",
                newName: "MaterialId");

            migrationBuilder.RenameColumn(
                name: "JewelriesJewelryId",
                table: "JewelryMaterial",
                newName: "JewelryId");

            migrationBuilder.RenameIndex(
                name: "IX_JewelryMaterial_MaterialsMaterialId",
                table: "JewelryMaterial",
                newName: "IX_JewelryMaterial_MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryMaterial_Jewelry_JewelryId",
                table: "JewelryMaterial",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryMaterial_Material_MaterialId",
                table: "JewelryMaterial",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JewelryMaterial_Jewelry_JewelryId",
                table: "JewelryMaterial");

            migrationBuilder.DropForeignKey(
                name: "FK_JewelryMaterial_Material_MaterialId",
                table: "JewelryMaterial");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "JewelryMaterial",
                newName: "MaterialsMaterialId");

            migrationBuilder.RenameColumn(
                name: "JewelryId",
                table: "JewelryMaterial",
                newName: "JewelriesJewelryId");

            migrationBuilder.RenameIndex(
                name: "IX_JewelryMaterial_MaterialId",
                table: "JewelryMaterial",
                newName: "IX_JewelryMaterial_MaterialsMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryMaterial_Jewelry_JewelriesJewelryId",
                table: "JewelryMaterial",
                column: "JewelriesJewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JewelryMaterial_Material_MaterialsMaterialId",
                table: "JewelryMaterial",
                column: "MaterialsMaterialId",
                principalTable: "Material",
                principalColumn: "MaterialId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
