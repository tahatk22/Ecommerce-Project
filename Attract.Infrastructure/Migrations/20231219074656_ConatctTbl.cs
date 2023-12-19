using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConatctTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                schema: "Attract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "Attract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                schema: "Attract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductAvailableSizeId = table.Column<int>(type: "int", nullable: true),
                    ProductColorId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductAvailableSizeAvailableSizeId = table.Column<int>(type: "int", nullable: true),
                    ProductAvailableSizeProductId = table.Column<int>(type: "int", nullable: true),
                    ProductColorColorId = table.Column<int>(type: "int", nullable: true),
                    ProductColorProductId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProduct_Cart_CartId",
                        column: x => x.CartId,
                        principalSchema: "Attract",
                        principalTable: "Cart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartProduct_ProductAvailableSize_ProductAvailableSizeProductId_ProductAvailableSizeAvailableSizeId",
                        columns: x => new { x.ProductAvailableSizeProductId, x.ProductAvailableSizeAvailableSizeId },
                        principalSchema: "Attract",
                        principalTable: "ProductAvailableSize",
                        principalColumns: new[] { "ProductId", "AvailableSizeId" });
                    table.ForeignKey(
                        name: "FK_CartProduct_ProductColor_ProductColorProductId_ProductColorColorId",
                        columns: x => new { x.ProductColorProductId, x.ProductColorColorId },
                        principalSchema: "Attract",
                        principalTable: "ProductColor",
                        principalColumns: new[] { "ProductId", "ColorId" });
                    table.ForeignKey(
                        name: "FK_CartProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Attract",
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                schema: "Attract",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_CartId",
                schema: "Attract",
                table: "CartProduct",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductAvailableSizeProductId_ProductAvailableSizeAvailableSizeId",
                schema: "Attract",
                table: "CartProduct",
                columns: new[] { "ProductAvailableSizeProductId", "ProductAvailableSizeAvailableSizeId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductColorProductId_ProductColorColorId",
                schema: "Attract",
                table: "CartProduct",
                columns: new[] { "ProductColorProductId", "ProductColorColorId" });

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_ProductId",
                schema: "Attract",
                table: "CartProduct",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct",
                schema: "Attract");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "Attract");

            migrationBuilder.DropTable(
                name: "Cart",
                schema: "Attract");
        }
    }
}
