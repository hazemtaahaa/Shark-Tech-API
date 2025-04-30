using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shark_Tech.DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("183c46f3-f8c9-4430-81b7-edf88859661c"), "Devices and gadgets", "Electronics" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "Quantity", "UpdatedAt" },
                values: new object[] { new Guid("36441c12-7d4d-4e00-945c-adce4a78db4d"), new Guid("183c46f3-f8c9-4430-81b7-edf88859661c"), new DateTime(2025, 4, 30, 9, 36, 1, 302, DateTimeKind.Local).AddTicks(7291), "This is a sample product description.", "Sample Product", 99.99m, 10, new DateTime(2025, 4, 30, 9, 36, 1, 302, DateTimeKind.Local).AddTicks(7295) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("183c46f3-f8c9-4430-81b7-edf88859661c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("36441c12-7d4d-4e00-945c-adce4a78db4d"));
        }
    }
}
