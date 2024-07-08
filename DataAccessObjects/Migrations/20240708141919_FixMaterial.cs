using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    public partial class FixMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Material",
                newName: "CertificateCode");

            migrationBuilder.AddColumn<string>(
                name: "GemType",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GemType",
                table: "Material");

            migrationBuilder.RenameColumn(
                name: "CertificateCode",
                table: "Material",
                newName: "Name");
        }
    }
}
