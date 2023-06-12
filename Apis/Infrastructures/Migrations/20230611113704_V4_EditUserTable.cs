using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V4EditUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("967c6597-56ae-4924-ac99-e0d708c95b1d"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("300e2cdc-b027-40e5-b66f-c2902d4a20c9"));

            migrationBuilder.AlterColumn<bool>(
                name: "Sex",
                table: "User",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "User",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("ca2356fd-e7ea-48f7-a2c3-cb817ee4ecfe"), null, new DateTime(2023, 6, 11, 18, 37, 4, 295, DateTimeKind.Local).AddTicks(552), new DateTime(2023, 6, 11, 18, 37, 4, 295, DateTimeKind.Local).AddTicks(564), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("a6650cc2-0dd5-4c7d-89fd-f9bc1b84194c"), "Do Kho", null, new DateTime(2023, 6, 11, 18, 37, 4, 295, DateTimeKind.Local).AddTicks(2747), null, null, false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("ca2356fd-e7ea-48f7-a2c3-cb817ee4ecfe"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a6650cc2-0dd5-4c7d-89fd-f9bc1b84194c"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "User");

            migrationBuilder.AlterColumn<bool>(
                name: "Sex",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("967c6597-56ae-4924-ac99-e0d708c95b1d"), null, new DateTime(2023, 6, 6, 22, 21, 40, 309, DateTimeKind.Local).AddTicks(3474), new DateTime(2023, 6, 6, 22, 21, 40, 309, DateTimeKind.Local).AddTicks(3488), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("300e2cdc-b027-40e5-b66f-c2902d4a20c9"), "Do Kho", null, new DateTime(2023, 6, 6, 22, 21, 40, 309, DateTimeKind.Local).AddTicks(5275), null, null, false, null, null });
        }
    }
}
