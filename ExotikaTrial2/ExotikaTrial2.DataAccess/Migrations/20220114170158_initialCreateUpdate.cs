using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExotikaTrial2.DataAccess.Migrations
{
    public partial class initialCreateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "emailID",
                table: "Resorts",
                newName: "emailAddr");

            migrationBuilder.AddColumn<string>(
                name: "emailAddr",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emailAddr",
                table: "Tourists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emailAddr",
                table: "HandicraftDealers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emailAddr",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailAddr",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "emailAddr",
                table: "Tourists");

            migrationBuilder.DropColumn(
                name: "emailAddr",
                table: "HandicraftDealers");

            migrationBuilder.DropColumn(
                name: "emailAddr",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "emailAddr",
                table: "Resorts",
                newName: "emailID");
        }
    }
}
