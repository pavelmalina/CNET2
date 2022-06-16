using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class companyAddr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddrId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddrId",
                table: "Companies",
                column: "AddrId",
                unique: true,
                filter: "[AddrId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddrId",
                table: "Companies",
                column: "AddrId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddrId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddrId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AddrId",
                table: "Companies");
        }
    }
}
