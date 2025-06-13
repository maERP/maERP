using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class addNCtx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateEnrollment",
                table: "Customer",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "NCtx",
                table: "AiModel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd6f6cde-7802-42a0-abce-d43be6c20bc5", new DateTime(2025, 6, 13, 9, 7, 4, 122, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 6, 13, 9, 7, 4, 122, DateTimeKind.Utc).AddTicks(2980), "AQAAAAIAAYagAAAAEP549xjatXxVepUFIXR4G4UQs6S4mmKNSQkRjPB/RoUxx2I8iFmEu5OiLs1rjPTrUg==", "d3917a9e-c6e0-460c-ae60-369e6719994d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1adaed85-7fd6-42c4-93ce-0cc8c2444795", new DateTime(2025, 6, 13, 9, 7, 4, 156, DateTimeKind.Utc).AddTicks(1940), new DateTime(2025, 6, 13, 9, 7, 4, 156, DateTimeKind.Utc).AddTicks(1940), "AQAAAAIAAYagAAAAELbTqTmgzFTcbWOQ/pl2nFRDoTiRHu0igJ7Xqn0/ZdAfmj2GWutRlO9YnvqL98Ws6Q==", "737d0a8d-68e2-4ac2-85bd-340fafc1c799" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(2180), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(570), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(570) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NCtx",
                table: "AiModel");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnrollment",
                table: "Customer",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9053a9b-d89f-48ed-83a6-48cc7c2d9f80", new DateTime(2025, 5, 10, 9, 25, 3, 711, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 5, 10, 9, 25, 3, 711, DateTimeKind.Utc).AddTicks(7740), "AQAAAAIAAYagAAAAEB+W5PW1/iDn7Otli2xoavwReS3D0yB//o7zdorPR229WTU2PSEG9+E+KkjNyYm+aw==", "9acae09a-9d56-40ed-ba4c-4aafef507d99" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b8f7dd0-dc97-4210-a020-6f37513e0213", new DateTime(2025, 5, 10, 9, 25, 3, 746, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 5, 10, 9, 25, 3, 746, DateTimeKind.Utc).AddTicks(5100), "AQAAAAIAAYagAAAAEBeVd+7KCEsT9qCv6ZLTdP9BaOaqidjE4V2cOz2CmsL8ZP4pl4wJN7aEVgfFynpHBQ==", "f610e4b9-6ab7-47ed-9e03-c5870df29575" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(8510), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1080), new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1080) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1220), new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1220) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(7010) });
        }
    }
}
