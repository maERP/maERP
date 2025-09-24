using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerIdToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "customer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6770) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 210, DateTimeKind.Utc).AddTicks(3650), new DateTime(2025, 9, 24, 8, 26, 26, 210, DateTimeKind.Utc).AddTicks(3650) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 220, DateTimeKind.Utc).AddTicks(8200), new DateTime(2025, 9, 24, 8, 26, 26, 220, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1650), new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 203, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 9, 24, 8, 26, 26, 203, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e1d78ee-4c4c-451f-9b96-67b53c2c1bea", new DateTime(2025, 9, 24, 8, 26, 26, 135, DateTimeKind.Utc).AddTicks(1410), new DateTime(2025, 9, 24, 8, 26, 26, 135, DateTimeKind.Utc).AddTicks(1410), "AQAAAAIAAYagAAAAEGPtg2kUYeRBF1svdfmnJDwcC531ZVR8aaz9e9/to1fqPHjyQ7JPITObGmi/AFz0UQ==", "6bff7c99-06d1-4b2e-b8c4-22272eadee6d" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57c1cdfd-efd2-4217-b330-4de1256ba8b4", new DateTime(2025, 9, 24, 8, 26, 26, 169, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 9, 24, 8, 26, 26, 169, DateTimeKind.Utc).AddTicks(3580), "AQAAAAIAAYagAAAAEG+Jn+yNTU5FigYbTfq+EZXmhsxTKVzKPXtM93gE54loScbrFXgms2OJm/SHJzhU9Q==", "bd70b65b-af15-4c20-9474-bab4a136e16f" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9310), new Guid("7f6dbfe8-bea7-4fc4-9e9b-7fb75e04523d") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9430), new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9430), new Guid("56ede1a7-5076-4671-b41f-353695685e29") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(8610) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "customer");

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(9620) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 877, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 22, 20, 23, 49, 877, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(160), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(160) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(290), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 860, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 9, 22, 20, 23, 49, 860, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed75f91f-8d0d-43f2-b01e-42fc728a801f", new DateTime(2025, 9, 22, 20, 23, 49, 790, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 9, 22, 20, 23, 49, 790, DateTimeKind.Utc).AddTicks(4320), "AQAAAAIAAYagAAAAEI7vwFie7n0z+UWCXj/yBAQqBe81+wvX6YLCd+YDAcCWbaC1BgylVcLrqErtzexkTQ==", "15fead44-b2dc-4e4b-abe2-ea90c75bfaf7" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c39248f-65a1-4b93-ab8a-2abe33c15f65", new DateTime(2025, 9, 22, 20, 23, 49, 825, DateTimeKind.Utc).AddTicks(8040), new DateTime(2025, 9, 22, 20, 23, 49, 825, DateTimeKind.Utc).AddTicks(8040), "AQAAAAIAAYagAAAAEKyULp+Xysrsn5eGOt/aquZF+0ictkoelNKH+IjOdUVwq3NQcocbmwGZYzXXQcC9NQ==", "74f128cb-3e75-46dc-94f5-2a6b19166d14" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5310), new Guid("e28d1200-98dd-4b50-ba3f-794cb5628e81") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5450), new Guid("97aec4e7-8ce0-4e08-af74-956ce4f796e5") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(4490) });
        }
    }
}
