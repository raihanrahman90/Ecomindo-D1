using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomindo_D1.Migrations
{
    public partial class addForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Restaurants_RestaurantidRestaurant",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_RestaurantidRestaurant",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RestaurantidRestaurant",
                table: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_idRestaurant",
                table: "Menu",
                column: "idRestaurant");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Restaurants_idRestaurant",
                table: "Menu",
                column: "idRestaurant",
                principalTable: "Restaurants",
                principalColumn: "idRestaurant",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Restaurants_idRestaurant",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_idRestaurant",
                table: "Menu");

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantidRestaurant",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
