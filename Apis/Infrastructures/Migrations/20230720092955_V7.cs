using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductInMenu_ProductMenuId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "ProductMenuId",
                table: "OrderDetail",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_ProductMenuId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Product_ProductId",
                table: "OrderDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Product_ProductId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetail",
                newName: "ProductMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_ProductMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductInMenu_ProductMenuId",
                table: "OrderDetail",
                column: "ProductMenuId",
                principalTable: "ProductInMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
