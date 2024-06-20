using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class FixMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Promotion_PromotionId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Warranties_WarrantyId1",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_Warranties_WarrantyId1",
                table: "Warranties");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_PromotionId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "WarrantyId1",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "PromotionId",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialName",
                table: "Material",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaterialCost",
                table: "Material",
                type: "money",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "BidPrice",
                table: "Material",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Clarity",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OfferPrice",
                table: "Material",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "Purity",
                table: "Material",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Sharp",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "JewelryImage",
                table: "Jewelry",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties",
                column: "JewelryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties");

            migrationBuilder.DropColumn(
                name: "BidPrice",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Clarity",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "OfferPrice",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Purity",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Sharp",
                table: "Material");

            migrationBuilder.AddColumn<int>(
                name: "WarrantyId1",
                table: "Warranties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PromotionId",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialName",
                table: "Material",
                type: "nvarchar(100)",
                maxLength: 100,
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

            migrationBuilder.AlterColumn<byte[]>(
                name: "JewelryImage",
                table: "Jewelry",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranties",
                column: "JewelryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_WarrantyId1",
                table: "Warranties",
                column: "WarrantyId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_PromotionId",
                table: "OrderDetail",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Promotion_PromotionId",
                table: "OrderDetail",
                column: "PromotionId",
                principalTable: "Promotion",
                principalColumn: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Warranties_WarrantyId1",
                table: "Warranties",
                column: "WarrantyId1",
                principalTable: "Warranties",
                principalColumn: "WarrantyId");
        }
    }
}
