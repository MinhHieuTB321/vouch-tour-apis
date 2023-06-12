using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V5EditTourGuideTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("ca2356fd-e7ea-48f7-a2c3-cb817ee4ecfe"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a6650cc2-0dd5-4c7d-89fd-f9bc1b84194c"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TourGuide",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("dcdb3672-6533-48e5-8d60-cba3b79d02fa"), null, new DateTime(2023, 6, 12, 0, 40, 30, 535, DateTimeKind.Local).AddTicks(7755), new DateTime(2023, 6, 12, 0, 40, 30, 535, DateTimeKind.Local).AddTicks(7766), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("c36739f5-add9-4e68-8a39-32fd564b7cee"), "Do Kho", null, new DateTime(2023, 6, 12, 0, 40, 30, 535, DateTimeKind.Local).AddTicks(9601), null, null, false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("dcdb3672-6533-48e5-8d60-cba3b79d02fa"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("c36739f5-add9-4e68-8a39-32fd564b7cee"));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TourGuide",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Active");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("ca2356fd-e7ea-48f7-a2c3-cb817ee4ecfe"), null, new DateTime(2023, 6, 11, 18, 37, 4, 295, DateTimeKind.Local).AddTicks(552), new DateTime(2023, 6, 11, 18, 37, 4, 295, DateTimeKind.Local).AddTicks(564), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("a6650cc2-0dd5-4c7d-89fd-f9bc1b84194c"), "Do Kho", null, new DateTime(2023, 6, 11, 18, 37, 4, 295, DateTimeKind.Local).AddTicks(2747), null, null, false, null, null });
        }
    }
}
