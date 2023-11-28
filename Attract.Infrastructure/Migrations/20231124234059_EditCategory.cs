using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "Attract",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "Attract",
                table: "Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Attract",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifyBy",
                schema: "Attract",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyOn",
                schema: "Attract",
                table: "Category",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Attract",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "Attract",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Attract",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                schema: "Attract",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ModifyOn",
                schema: "Attract",
                table: "Category");
        }
    }
}
