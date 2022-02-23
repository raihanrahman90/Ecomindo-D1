using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomindo_D1.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    idRestaurant = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    namaRestaurant = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.idRestaurant);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    idMenu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    namaMenu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hargaMenu = table.Column<int>(type: "int", nullable: false),
                    RestaurantidRestaurant = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.idMenu);
                    table.ForeignKey(
                        name: "FK_Menu_Restaurants_RestaurantidRestaurant",
                        column: x => x.RestaurantidRestaurant,
                        principalTable: "Restaurants",
                        principalColumn: "idRestaurant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_RestaurantidRestaurant",
                table: "Menu",
                column: "RestaurantidRestaurant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
