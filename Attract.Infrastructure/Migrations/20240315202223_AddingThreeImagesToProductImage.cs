using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingThreeImagesToProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName4",
                schema: "Attract",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName5",
                schema: "Attract",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName6",
                schema: "Attract",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName4",
                schema: "Attract",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ImageFileName5",
                schema: "Attract",
                table: "ProductImage");

            migrationBuilder.DropColumn(
                name: "ImageFileName6",
                schema: "Attract",
                table: "ProductImage");
        }
    }
}
