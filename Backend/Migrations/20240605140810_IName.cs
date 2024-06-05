using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class IName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_AspNetUsers_UserId",
                table: "Incomes");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Incomes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Incomes",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Incomes");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Incomes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_AspNetUsers_UserId",
                table: "Incomes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
