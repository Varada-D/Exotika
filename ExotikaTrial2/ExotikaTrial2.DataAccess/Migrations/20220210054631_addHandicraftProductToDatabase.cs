using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExotikaTrial2.DataAccess.Migrations
{
    public partial class addHandicraftProductToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    HandicraftDealerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_HandicraftDealers_HandicraftDealerId",
                        column: x => x.HandicraftDealerId,
                        principalTable: "HandicraftDealers",
                        principalColumn: "HandicraftDealerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_HandicraftDealerId",
                table: "Products",
                column: "HandicraftDealerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
