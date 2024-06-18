using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Jewelry_JewelryId",
                table: "Warranties");

            migrationBuilder.DropForeignKey(
                name: "FK_Warranties_Order_OrderId",
                table: "Warranties");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistories_Warranties_WarrantyId",
                table: "WarrantyHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarrantyHistories",
                table: "WarrantyHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warranties",
                table: "Warranties");

            migrationBuilder.RenameTable(
                name: "WarrantyHistories",
                newName: "WarrantyHistory");

            migrationBuilder.RenameTable(
                name: "Warranties",
                newName: "Warranty");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyHistories_WarrantyId",
                table: "WarrantyHistory",
                newName: "IX_WarrantyHistory_WarrantyId");

            migrationBuilder.RenameIndex(
                name: "IX_Warranties_OrderId",
                table: "Warranty",
                newName: "IX_Warranty_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Warranties_JewelryId",
                table: "Warranty",
                newName: "IX_Warranty_JewelryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarrantyHistory",
                table: "WarrantyHistory",
                column: "WarrantyHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warranty",
                table: "Warranty",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranty_Jewelry_JewelryId",
                table: "Warranty",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warranty_Order_OrderId",
                table: "Warranty",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistory_Warranty_WarrantyId",
                table: "WarrantyHistory",
                column: "WarrantyId",
                principalTable: "Warranty",
                principalColumn: "WarrantyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warranty_Jewelry_JewelryId",
                table: "Warranty");

            migrationBuilder.DropForeignKey(
                name: "FK_Warranty_Order_OrderId",
                table: "Warranty");

            migrationBuilder.DropForeignKey(
                name: "FK_WarrantyHistory_Warranty_WarrantyId",
                table: "WarrantyHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarrantyHistory",
                table: "WarrantyHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warranty",
                table: "Warranty");

            migrationBuilder.RenameTable(
                name: "WarrantyHistory",
                newName: "WarrantyHistories");

            migrationBuilder.RenameTable(
                name: "Warranty",
                newName: "Warranties");

            migrationBuilder.RenameIndex(
                name: "IX_WarrantyHistory_WarrantyId",
                table: "WarrantyHistories",
                newName: "IX_WarrantyHistories_WarrantyId");

            migrationBuilder.RenameIndex(
                name: "IX_Warranty_OrderId",
                table: "Warranties",
                newName: "IX_Warranties_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Warranty_JewelryId",
                table: "Warranties",
                newName: "IX_Warranties_JewelryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarrantyHistories",
                table: "WarrantyHistories",
                column: "WarrantyHistoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warranties",
                table: "Warranties",
                column: "WarrantyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Jewelry_JewelryId",
                table: "Warranties",
                column: "JewelryId",
                principalTable: "Jewelry",
                principalColumn: "JewelryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Warranties_Order_OrderId",
                table: "Warranties",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarrantyHistories_Warranties_WarrantyId",
                table: "WarrantyHistories",
                column: "WarrantyId",
                principalTable: "Warranties",
                principalColumn: "WarrantyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
