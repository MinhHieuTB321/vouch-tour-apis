using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "OrderDetail",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "OrderDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ShipAddress",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "ShipAddress",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OrderDetail",
                newName: "ProductName");
        }
    }
}
