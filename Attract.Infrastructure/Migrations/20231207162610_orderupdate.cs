using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attract.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Attract",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                schema: "Attract",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Attract",
                table: "Order",
                newName: "CustomerName");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                schema: "Attract",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                schema: "Attract",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                schema: "Attract",
                table: "Order",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                schema: "Attract",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                schema: "Attract",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
