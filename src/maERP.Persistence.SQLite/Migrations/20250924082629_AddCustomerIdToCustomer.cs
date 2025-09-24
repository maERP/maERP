using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.SQLite.Migrations
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
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(870), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1270), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1360), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1500), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1560), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(8140), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(8140) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(250), new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(250) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(180), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(180) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1250), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1250) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1260), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1260), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1270), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1270), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1280) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1280) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 9, 24, 8, 26, 29, 320, DateTimeKind.Utc).AddTicks(1290) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 9, 24, 8, 26, 29, 315, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 296, DateTimeKind.Utc).AddTicks(2850), new DateTime(2025, 9, 24, 8, 26, 29, 296, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7e4e49a-3575-41f8-b03a-513947e5bd96", new DateTime(2025, 9, 24, 8, 26, 29, 228, DateTimeKind.Utc).AddTicks(670), new DateTime(2025, 9, 24, 8, 26, 29, 228, DateTimeKind.Utc).AddTicks(670), "AQAAAAIAAYagAAAAEPVJ34+cndZ4CWJHzjVnerkxxqFDto9tcfyGKt6z/T1GdyGtsdD2Flv2Fb850Z6ODA==", "68b55586-4508-423d-8c40-cfaf64540888" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff536816-01d1-44d2-b2e3-436388e566f1", new DateTime(2025, 9, 24, 8, 26, 29, 261, DateTimeKind.Utc).AddTicks(9800), new DateTime(2025, 9, 24, 8, 26, 29, 261, DateTimeKind.Utc).AddTicks(9800), "AQAAAAIAAYagAAAAEBzHh8LHnvAFzLMVasofxpD/lRoWL5TCdr82jp+WnxDe3kmOjynIbrjbwkrYTZb5yA==", "50446196-3636-419c-9003-72fef6ae0e5e" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 299, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 9, 24, 8, 26, 29, 299, DateTimeKind.Utc).AddTicks(5140), new Guid("ec92055f-45a9-4e9b-aab5-ff0e6f3b9004") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 299, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 9, 24, 8, 26, 29, 299, DateTimeKind.Utc).AddTicks(5240), new Guid("464a8f02-f49d-400c-a656-ddb318bcb866") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 9, 24, 8, 26, 29, 302, DateTimeKind.Utc).AddTicks(3190) });
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
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4520), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4520), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4520), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4530), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4530), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4530), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4550), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4550), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4550), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4570), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4570), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4570), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4580), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4600), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4600), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4600), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4630), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4780), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 977, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 9, 22, 20, 23, 52, 977, DateTimeKind.Utc).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7230), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7470), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 9, 22, 20, 23, 52, 990, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(9790), new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(9900), new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(9900) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(9900), new DateTime(2025, 9, 22, 20, 23, 52, 987, DateTimeKind.Utc).AddTicks(9900) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 970, DateTimeKind.Utc).AddTicks(8470), new DateTime(2025, 9, 22, 20, 23, 52, 970, DateTimeKind.Utc).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb463360-ce1a-41de-9a4b-69ae8b525ce0", new DateTime(2025, 9, 22, 20, 23, 52, 899, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 9, 22, 20, 23, 52, 899, DateTimeKind.Utc).AddTicks(9870), "AQAAAAIAAYagAAAAEJrHA8uqXvxK1k2OCcuYkri//aNHEw3KnvIPafqpjmup+l93EKjdotDyq7jEgf7rjQ==", "c5372381-7ad1-434e-85d2-93b87085118e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55b68804-9fcc-4290-9953-35f9a3a4c2b3", new DateTime(2025, 9, 22, 20, 23, 52, 936, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 52, 936, DateTimeKind.Utc).AddTicks(2360), "AQAAAAIAAYagAAAAEGM0IcKEOWqNBgVAuHMbqTSe0QlOlM9ckkgHjT/tSDbpB6yOgksFWVvoGT6CoM6Y/w==", "94374755-3a53-4c58-9b22-12b639e2c222" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 973, DateTimeKind.Utc).AddTicks(7890), new DateTime(2025, 9, 22, 20, 23, 52, 973, DateTimeKind.Utc).AddTicks(8000), new Guid("08369145-84e8-40d3-bfb7-7bf19f91a686") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 973, DateTimeKind.Utc).AddTicks(8100), new DateTime(2025, 9, 22, 20, 23, 52, 973, DateTimeKind.Utc).AddTicks(8100), new Guid("e7ec710a-ca4c-4c65-91f1-e806ad648496") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 22, 20, 23, 52, 976, DateTimeKind.Utc).AddTicks(6540) });
        }
    }
}
