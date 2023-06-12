using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V7EditUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("7b572c63-8308-4c30-912d-8c9a94f34c10"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("0d6dcb2f-e7ae-4630-aa58-719cb2fc1b62"));

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "User");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "User");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("0ae06378-3022-4e4e-b80a-ff012f822b68"), null, new DateTime(2023, 6, 12, 21, 38, 56, 541, DateTimeKind.Local).AddTicks(8936), new DateTime(2023, 6, 12, 21, 38, 56, 541, DateTimeKind.Local).AddTicks(8951), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("02955ea6-f3df-4cd7-bce6-7b5877ac5143"), "Do Kho", null, new DateTime(2023, 6, 12, 21, 38, 56, 542, DateTimeKind.Local).AddTicks(853), null, null, false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("0ae06378-3022-4e4e-b80a-ff012f822b68"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("02955ea6-f3df-4cd7-bce6-7b5877ac5143"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sex",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("7b572c63-8308-4c30-912d-8c9a94f34c10"), null, new DateTime(2023, 6, 12, 13, 23, 25, 197, DateTimeKind.Local).AddTicks(9365), new DateTime(2023, 6, 12, 13, 23, 25, 197, DateTimeKind.Local).AddTicks(9381), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("0d6dcb2f-e7ae-4630-aa58-719cb2fc1b62"), "Do Kho", null, new DateTime(2023, 6, 12, 13, 23, 25, 198, DateTimeKind.Local).AddTicks(1357), null, null, false, null, null });
        }
    }
}
