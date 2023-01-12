using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class Literowka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Adresses_AdressId",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Restaurants",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_AdressId",
                table: "Restaurants",
                newName: "IX_Restaurants_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Adresses_AddressId",
                table: "Restaurants",
                column: "AddressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Adresses_AddressId",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Restaurants",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Restaurants_AddressId",
                table: "Restaurants",
                newName: "IX_Restaurants_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Adresses_AdressId",
                table: "Restaurants",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
