using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnyWareTask.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Amount", "CreatedAt", "CustomerName", "Product" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), 75.00m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah Samy", "Oil Filter" },
                    { new Guid("d3e4f5a6-1b2c-3d4e-5f6a-7b8c9d0e1f2a"), 150.50m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed Ali", "Brake Pads" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
