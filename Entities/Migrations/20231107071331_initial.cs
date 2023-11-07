using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyOrders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrderDateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyOrders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "SellOrder",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    OrderDateAndTime = table.Column<DateTime>(type: "datetime2", maxLength: 20, nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrder", x => x.OrderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyOrders");

            migrationBuilder.DropTable(
                name: "SellOrder");
        }
    }
}
