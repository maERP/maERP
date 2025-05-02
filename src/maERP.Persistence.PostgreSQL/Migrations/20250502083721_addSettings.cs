using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class addSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d32fe67f-64e6-4cfb-89fa-fadd50b11bbf", new DateTime(2025, 5, 2, 8, 37, 21, 110, DateTimeKind.Utc).AddTicks(9050), new DateTime(2025, 5, 2, 8, 37, 21, 110, DateTimeKind.Utc).AddTicks(9060), "AQAAAAIAAYagAAAAEE1p6GrvL4atQ1CB4380qxDYJKUbTL2OLnA2WxihwLbVl1Wdt9KXDbLaKcluHvSf6Q==", "fb20e59d-f586-4d05-8185-69a8d6cf5d3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa46cce3-0c4c-4e08-b755-e429abe4ac79", new DateTime(2025, 5, 2, 8, 37, 21, 145, DateTimeKind.Utc).AddTicks(50), new DateTime(2025, 5, 2, 8, 37, 21, 145, DateTimeKind.Utc).AddTicks(50), "AQAAAAIAAYagAAAAENaEsQjvY7PPY+tUx5DqvjbNXs4L5ShJ6zdwM/fK8D/wesUQ/7Hui1S7rTUdS6kEww==", "e9cf9949-f271-4d93-924c-33326f4d53c6" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2010), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(8040), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(8040) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(8040), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(8040) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 5, 2, 8, 37, 21, 178, DateTimeKind.Utc).AddTicks(4130) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f993ec76-3429-49c4-8d32-951107af6e12", new DateTime(2025, 4, 7, 15, 54, 17, 856, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 4, 7, 15, 54, 17, 856, DateTimeKind.Utc).AddTicks(6420), "AQAAAAIAAYagAAAAEOJF9QQ8QUD+r308OBzt1SyPFEND84fhEvWGGTThQlZzAdo+xmmpFYMj4AR4N5dm6A==", "600b405b-3403-4dd6-9eee-b8951bd831d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf56be89-05d8-485d-8ad2-e64ff9501627", new DateTime(2025, 4, 7, 15, 54, 17, 890, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 7, 15, 54, 17, 890, DateTimeKind.Utc).AddTicks(8410), "AQAAAAIAAYagAAAAEJPzWM+169qLU32mdBi4JwMgvC6n/HFEetRlGaAj7Of2bhtS5QwOH7sntFlLbgMRXg==", "5ef4b558-6507-4ab9-8783-91d0aefdc439" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(8880), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(7280) });
        }
    }
}
