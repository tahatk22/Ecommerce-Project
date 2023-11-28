using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Product_ProductId",
                schema: "Attract",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                schema: "Attract",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "AvailableSize",
                schema: "Attract",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Colors",
                schema: "Attract",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Image",
                schema: "Attract",
                newName: "ProductImage",
                newSchema: "Attract");

            migrationBuilder.RenameIndex(
                name: "IX_Image_ProductId",
                schema: "Attract",
                table: "ProductImage",
                newName: "IX_ProductImage_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImage",
                schema: "Attract",
                table: "ProductImage",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AvailableSize",
                schema: "Attract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableSize_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Attract",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                schema: "Attract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyBy = table.Column<int>(type: "int", nullable: true),
                    ModifyOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Attract",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableSize_ProductId",
                schema: "Attract",
                table: "AvailableSize",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ProductId",
                schema: "Attract",
                table: "Color",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                schema: "Attract",
                table: "ProductImage",
                column: "ProductId",
                principalSchema: "Attract",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product_ProductId",
                schema: "Attract",
                table: "ProductImage");

            migrationBuilder.DropTable(
                name: "AvailableSize",
                schema: "Attract");

            migrationBuilder.DropTable(
                name: "Color",
                schema: "Attract");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImage",
                schema: "Attract",
                table: "ProductImage");

            migrationBuilder.RenameTable(
                name: "ProductImage",
                schema: "Attract",
                newName: "Image",
                newSchema: "Attract");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImage_ProductId",
                schema: "Attract",
                table: "Image",
                newName: "IX_Image_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "AvailableSize",
                schema: "Attract",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Colors",
                schema: "Attract",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                schema: "Attract",
                table: "Image",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Product_ProductId",
                schema: "Attract",
                table: "Image",
                column: "ProductId",
                principalSchema: "Attract",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
