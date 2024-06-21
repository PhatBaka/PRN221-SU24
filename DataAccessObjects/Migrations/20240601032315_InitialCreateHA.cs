//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace DataAccessObjects.Migrations
//{
//    public partial class InitialCreateHA : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_OrderDetail_Promotion_PromotionId",
//                table: "OrderDetail");

//            migrationBuilder.AlterColumn<int>(
//                name: "PromotionId",
//                table: "OrderDetail",
//                type: "int",
//                nullable: false,
//                defaultValue: 0,
//                oldClrType: typeof(int),
//                oldType: "int",
//                oldNullable: true);

//            migrationBuilder.AddForeignKey(
//                name: "FK_OrderDetail_Promotion_PromotionId",
//                table: "OrderDetail",
//                column: "PromotionId",
//                principalTable: "Promotion",
//                principalColumn: "PromotionId",
//                onDelete: ReferentialAction.Cascade);
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_OrderDetail_Promotion_PromotionId",
//                table: "OrderDetail");

//            migrationBuilder.AlterColumn<int>(
//                name: "PromotionId",
//                table: "OrderDetail",
//                type: "int",
//                nullable: true,
//                oldClrType: typeof(int),
//                oldType: "int");

//            migrationBuilder.AddForeignKey(
//                name: "FK_OrderDetail_Promotion_PromotionId",
//                table: "OrderDetail",
//                column: "PromotionId",
//                principalTable: "Promotion",
//                principalColumn: "PromotionId");
//        }
//    }
//}
