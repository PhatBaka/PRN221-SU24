using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class AddWarranty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WarrantyId",
                table: "OrderDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Warranty",
                columns: table => new
                {
                    WarrantyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warranty", x => x.WarrantyId);
                    table.ForeignKey(
                        name: "FK_Warranty_OrderDetail_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "OrderDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarrantyRequest",
                columns: table => new
                {
                    WarrantyRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: "newid()"),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarrantyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantyRequest", x => x.WarrantyRequestId);
                    table.ForeignKey(
                        name: "FK_WarrantyRequest_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarrantyRequest_Warranty_WarrantyId",
                        column: x => x.WarrantyId,
                        principalTable: "Warranty",
                        principalColumn: "WarrantyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warranty_OrderDetailId",
                table: "Warranty",
                column: "OrderDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyRequest_AccountId",
                table: "WarrantyRequest",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantyRequest_WarrantyId",
                table: "WarrantyRequest",
                column: "WarrantyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarrantyRequest");

            migrationBuilder.DropTable(
                name: "Warranty");

            migrationBuilder.DropColumn(
                name: "WarrantyId",
                table: "OrderDetail");
        }
    }
}
