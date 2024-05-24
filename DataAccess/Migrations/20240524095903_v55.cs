using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFoods_Foods_GameId",
                table: "UserFoods");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "UserFoods",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFoods_GameId",
                table: "UserFoods",
                newName: "IX_UserFoods_FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFoods_Foods_FoodId",
                table: "UserFoods",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFoods_Foods_FoodId",
                table: "UserFoods");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "UserFoods",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFoods_FoodId",
                table: "UserFoods",
                newName: "IX_UserFoods_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFoods_Foods_GameId",
                table: "UserFoods",
                column: "GameId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
