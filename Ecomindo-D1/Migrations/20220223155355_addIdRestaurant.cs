using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomindo_D1.Migrations
{
    public partial class addIdRestaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "idRestaurant",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idRestaurant",
                table: "Menu");
        }
    }
}
