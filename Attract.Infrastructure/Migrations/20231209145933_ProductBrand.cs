using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                schema: "Attract",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                schema: "Attract",
                table: "Product");
        }
    }
}
