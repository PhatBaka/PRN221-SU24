using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class FixWarranty : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
