using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Attract",
                table: "Bill",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserId",
                schema: "Attract",
                table: "Bill",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_AspNetUsers_UserId",
                schema: "Attract",
                table: "Bill",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_AspNetUsers_UserId",
                schema: "Attract",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_UserId",
                schema: "Attract",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Attract",
                table: "Bill");
        }
    }
}
