using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class V6EditSupplierTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admin",
                keyColumn: "Id",
                keyValue: new Guid("dcdb3672-6533-48e5-8d60-cba3b79d02fa"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("c36739f5-add9-4e68-8a39-32fd564b7cee"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Supplier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("7b572c63-8308-4c30-912d-8c9a94f34c10"), null, new DateTime(2023, 6, 12, 13, 23, 25, 197, DateTimeKind.Local).AddTicks(9365), new DateTime(2023, 6, 12, 13, 23, 25, 197, DateTimeKind.Local).AddTicks(9381), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("0d6dcb2f-e7ae-4630-aa58-719cb2fc1b62"), "Do Kho", null, new DateTime(2023, 6, 12, 13, 23, 25, 198, DateTimeKind.Local).AddTicks(1357), null, null, false, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Email",
                table: "Supplier");

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("dcdb3672-6533-48e5-8d60-cba3b79d02fa"), null, new DateTime(2023, 6, 12, 0, 40, 30, 535, DateTimeKind.Local).AddTicks(7755), new DateTime(2023, 6, 12, 0, 40, 30, 535, DateTimeKind.Local).AddTicks(7766), null, null, "quangtm0152@gmail.com", false, null, null, "QuangDepTry", "0909014406", (byte)1, "Active" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate" },
                values: new object[] { new Guid("c36739f5-add9-4e68-8a39-32fd564b7cee"), "Do Kho", null, new DateTime(2023, 6, 12, 0, 40, 30, 535, DateTimeKind.Local).AddTicks(9601), null, null, false, null, null });
        }
    }
}
