using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileName",
                schema: "Attract",
                table: "ProductImage",
                newName: "ImageFileName3");

            migrationBuilder.RenameColumn(
                name: "ImageColorHexa",
                schema: "Attract",
                table: "ProductImage",
                newName: "ImageFileName2");

            migrationBuilder.RenameColumn(
                name: "ImageColor",
                schema: "Attract",
                table: "ProductImage",
                newName: "ImageFileName1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageFileName3",
                schema: "Attract",
                table: "ProductImage",
                newName: "ImageFileName");

            migrationBuilder.RenameColumn(
                name: "ImageFileName2",
                schema: "Attract",
                table: "ProductImage",
                newName: "ImageColorHexa");

            migrationBuilder.RenameColumn(
                name: "ImageFileName1",
                schema: "Attract",
                table: "ProductImage",
                newName: "ImageColor");
        }
    }
}
