using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomindo_D1.Migrations
{
    public partial class addRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "idMenu",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantidRestaurant",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Menu_RestaurantidRestaurant",
                table: "Menu",
                column: "RestaurantidRestaurant");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Restaurants_RestaurantidRestaurant",
                table: "Menu",
                column: "RestaurantidRestaurant",
                principalTable: "Restaurants",
                principalColumn: "idRestaurant",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Restaurants_RestaurantidRestaurant",
                table: "Menu");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Menu_RestaurantidRestaurant",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RestaurantidRestaurant",
                table: "Menu");

            migrationBuilder.AlterColumn<int>(
                name: "idMenu",
                table: "Menu",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
