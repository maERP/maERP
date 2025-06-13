using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class addNCtx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "NCtx",
                table: "AiModel",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c2856e7-bad9-4038-962f-40e63cbc9c43", new DateTime(2025, 6, 13, 9, 7, 0, 940, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 6, 13, 9, 7, 0, 940, DateTimeKind.Utc).AddTicks(1890), "AQAAAAIAAYagAAAAEBm9kT2N7EummeSw1u5i2mB7qvthFKI1kiB9AiFUM7rPaw7p9z4xiwvd4IQlgKGHYA==", "14ab88da-4d5b-45ff-84a7-6075c9286bde" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43dac90a-3ef2-40e3-80d2-122ca178e239", new DateTime(2025, 6, 13, 9, 7, 0, 972, DateTimeKind.Utc).AddTicks(770), new DateTime(2025, 6, 13, 9, 7, 0, 972, DateTimeKind.Utc).AddTicks(770), "AQAAAAIAAYagAAAAEJSfT1OnvE8z7Uovl56zv/gGUe/dXmFQCCQ0ugvmBxjBno0Qd5sGyV9J63D8S14QzA==", "5bdb9653-c565-441e-8ab6-ce65ebb83504" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(2890), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(6810), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(6820) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(9620) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(9720) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(9720) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 6, 13, 9, 7, 1, 5, DateTimeKind.Utc).AddTicks(5210) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NCtx",
                table: "AiModel");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "813bb788-3047-4525-9387-4a8194914eb0", new DateTime(2025, 5, 10, 9, 25, 0, 28, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 5, 10, 9, 25, 0, 28, DateTimeKind.Utc).AddTicks(1680), "AQAAAAIAAYagAAAAEL3f2ym+TUKeibxHFtWFkTW3+qFqN7dNj3qJwfeZbWtU9aHCVkJTuVy6Bfk3ViRZfA==", "cfe6c916-1b1b-413d-9c58-9d4780d7fc1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ef5c79f-b571-49fb-9ba6-1eef80f1f939", new DateTime(2025, 5, 10, 9, 25, 0, 63, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 5, 10, 9, 25, 0, 63, DateTimeKind.Utc).AddTicks(4260), "AQAAAAIAAYagAAAAEKk8xwt/fSOyADbv55TTuzsVWjaUTeDHKlNc6e6UF1tllPTAsSVkcdkOV1pZwekT+Q==", "608db0d1-3623-47a8-a8d8-3ba55eb0e906" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(1970), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(1970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(9400), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(9400) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(9500), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(9500) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(9500), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(9500) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 5, 10, 9, 25, 0, 99, DateTimeKind.Utc).AddTicks(4960) });
        }
    }
}
