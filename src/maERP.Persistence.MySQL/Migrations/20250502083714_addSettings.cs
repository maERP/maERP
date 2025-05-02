using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MySQL.Migrations
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
                values: new object[] { "5d6427a6-3348-4942-bb61-d6586e7092b0", new DateTime(2025, 5, 2, 8, 37, 14, 191, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 5, 2, 8, 37, 14, 191, DateTimeKind.Utc).AddTicks(7560), "AQAAAAIAAYagAAAAEIhzwPURIGxujb1fiVfJsozkYRici6K3y+Q9f3v/8YHiPOVKq3t866TjsrvdtnsoLg==", "b030f218-6655-466a-bee7-c25c929a552b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "007f5251-2c01-484f-8568-f67b8ed39404", new DateTime(2025, 5, 2, 8, 37, 14, 227, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 5, 2, 8, 37, 14, 227, DateTimeKind.Utc).AddTicks(1150), "AQAAAAIAAYagAAAAEKxV0vE4tHpdkVNZo6L+YZXdfDCMW9kQ73Sv0KY1Ss/5F5SdZA7SSyX6m6VY807WsQ==", "8523f253-8094-4a38-926a-cab3a880763d" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7230), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(3900), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(9600) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fba779c-d67e-4224-ac16-f31444e4f016", new DateTime(2025, 4, 7, 15, 52, 20, 406, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 4, 7, 15, 52, 20, 406, DateTimeKind.Utc).AddTicks(8210), "AQAAAAIAAYagAAAAEG/rqOxLV397iRCf5kwCiL40KekCsLEncfR0QhknrPAul0AsnUJ9P5Cyqd2hPw5p+g==", "723c894d-dff6-4008-b80e-e87667d5853d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93089403-ab48-4711-99de-df972e7e7eaa", new DateTime(2025, 4, 7, 15, 52, 20, 442, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 4, 7, 15, 52, 20, 442, DateTimeKind.Utc).AddTicks(3500), "AQAAAAIAAYagAAAAEGHj4aA/eWnUB22JyiiUXHonJEB0I6Hpd7foz0rqkPUvA74IL2k/3YDNGkk0fG8X9Q==", "8fb865e4-f1c4-47b8-961b-90a80b21032d" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7000), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3870), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(9450) });
        }
    }
}
