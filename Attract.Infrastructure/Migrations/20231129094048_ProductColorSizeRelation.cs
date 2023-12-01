using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductColorSizeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailableSize_Product_ProductId",
                schema: "Attract",
                table: "AvailableSize");

            migrationBuilder.DropForeignKey(
                name: "FK_Color_Product_ProductId",
                schema: "Attract",
                table: "Color");

            migrationBuilder.DropIndex(
                name: "IX_Color_ProductId",
                schema: "Attract",
                table: "Color");

            migrationBuilder.DropIndex(
                name: "IX_AvailableSize_ProductId",
                schema: "Attract",
                table: "AvailableSize");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Attract",
                table: "Color");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "Attract",
                table: "AvailableSize");

            migrationBuilder.CreateTable(
                name: "ProductAvailableSize",
                schema: "Attract",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AvailableSizeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAvailableSize", x => new { x.ProductId, x.AvailableSizeId });
                    table.ForeignKey(
                        name: "FK_ProductAvailableSize_AvailableSize_AvailableSizeId",
                        column: x => x.AvailableSizeId,
                        principalSchema: "Attract",
                        principalTable: "AvailableSize",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAvailableSize_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Attract",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColor",
                schema: "Attract",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColor", x => new { x.ProductId, x.ColorId });
                    table.ForeignKey(
                        name: "FK_ProductColor_Color_ColorId",
                        column: x => x.ColorId,
                        principalSchema: "Attract",
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColor_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Attract",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAvailableSize_AvailableSizeId",
                schema: "Attract",
                table: "ProductAvailableSize",
                column: "AvailableSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ColorId",
                schema: "Attract",
                table: "ProductColor",
                column: "ColorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAvailableSize",
                schema: "Attract");

            migrationBuilder.DropTable(
                name: "ProductColor",
                schema: "Attract");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "Attract",
                table: "Color",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "Attract",
                table: "AvailableSize",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Color_ProductId",
                schema: "Attract",
                table: "Color",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableSize_ProductId",
                schema: "Attract",
                table: "AvailableSize",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailableSize_Product_ProductId",
                schema: "Attract",
                table: "AvailableSize",
                column: "ProductId",
                principalSchema: "Attract",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Color_Product_ProductId",
                schema: "Attract",
                table: "Color",
                column: "ProductId",
                principalSchema: "Attract",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
