using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Group_GroupId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductInMenu_DiscountProductId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_Menu_GroupId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "DiscountProductId",
                table: "OrderDetail",
                newName: "ProductMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_DiscountProductId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_ProductMenuId");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "Group",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Group",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Group_MenuId",
                table: "Group",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Menu_MenuId",
                table: "Group",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductInMenu_ProductMenuId",
                table: "OrderDetail",
                column: "ProductMenuId",
                principalTable: "ProductInMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Menu_MenuId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_ProductInMenu_ProductMenuId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_Group_MenuId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Group");

            migrationBuilder.RenameColumn(
                name: "ProductMenuId",
                table: "OrderDetail",
                newName: "DiscountProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_ProductMenuId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_DiscountProductId");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Menu",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Menu_GroupId",
                table: "Menu",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Group_GroupId",
                table: "Menu",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_ProductInMenu_DiscountProductId",
                table: "OrderDetail",
                column: "DiscountProductId",
                principalTable: "ProductInMenu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
