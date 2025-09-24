using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(6930), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(6930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7450), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7450), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7450), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7470), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7470), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7470), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7520), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7520), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7550), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7550), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7550), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7560), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7560), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7560), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 16, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 9, 24, 9, 0, 53, 16, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 26, DateTimeKind.Utc).AddTicks(8510), new DateTime(2025, 9, 24, 9, 0, 53, 26, DateTimeKind.Utc).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9660), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9860), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9860), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 9, 24, 9, 0, 53, 29, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 27, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 24, 9, 0, 53, 27, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 27, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 24, 9, 0, 53, 27, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 27, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 24, 9, 0, 53, 27, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 9, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 9, 24, 9, 0, 53, 9, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61da4d1b-2d8e-4c1c-bf9a-92d6ef6273f9", new DateTime(2025, 9, 24, 9, 0, 52, 937, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 9, 24, 9, 0, 52, 937, DateTimeKind.Utc).AddTicks(7570), "AQAAAAIAAYagAAAAEIuRaoEf6gtMgWaryNiOvU3gxEOecOt/YBIzBktKMbIH0BmJ2BlXuO7w2UTHh4oeDA==", "a2137c89-04d1-4b70-824b-4d896286f1d9" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "21024df1-7ce7-4a59-8b69-e3c72fd9dfe0", new DateTime(2025, 9, 24, 9, 0, 52, 974, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 9, 24, 9, 0, 52, 974, DateTimeKind.Utc).AddTicks(8400), "AQAAAAIAAYagAAAAEJTr8qXENJ9DLeto3iiVb5RSplUnizJwde2gVvbjk92waMLyuC0Nbk1sqI3mGOLisg==", "06088702-4a62-4c09-bdce-2b691afb2bf0" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 13, DateTimeKind.Utc).AddTicks(670), new DateTime(2025, 9, 24, 9, 0, 53, 13, DateTimeKind.Utc).AddTicks(770), new Guid("f1c6a1d3-fd58-430f-a116-61ea0c98a037") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 13, DateTimeKind.Utc).AddTicks(870), new DateTime(2025, 9, 24, 9, 0, 53, 13, DateTimeKind.Utc).AddTicks(870), new Guid("0d2a7408-7218-4e87-a8bb-f5b97f037118") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(9440), new DateTime(2025, 9, 24, 9, 0, 53, 15, DateTimeKind.Utc).AddTicks(9440) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "order");

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6320), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6320), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6330), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6330), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6330), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6340), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6360), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6360), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6360), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6390), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6390), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6390), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 895, DateTimeKind.Utc).AddTicks(3560), new DateTime(2025, 9, 24, 8, 26, 22, 895, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 905, DateTimeKind.Utc).AddTicks(8360), new DateTime(2025, 9, 24, 8, 26, 22, 905, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9460), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9460) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 9, 24, 8, 26, 22, 908, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 906, DateTimeKind.Utc).AddTicks(2050), new DateTime(2025, 9, 24, 8, 26, 22, 906, DateTimeKind.Utc).AddTicks(2050) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 906, DateTimeKind.Utc).AddTicks(2180), new DateTime(2025, 9, 24, 8, 26, 22, 906, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 906, DateTimeKind.Utc).AddTicks(2180), new DateTime(2025, 9, 24, 8, 26, 22, 906, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 888, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 9, 24, 8, 26, 22, 888, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f622559f-c4c3-4abc-8be1-46dbbf3c2f62", new DateTime(2025, 9, 24, 8, 26, 22, 819, DateTimeKind.Utc).AddTicks(3830), new DateTime(2025, 9, 24, 8, 26, 22, 819, DateTimeKind.Utc).AddTicks(3830), "AQAAAAIAAYagAAAAEMNvYDGB8fwC/23GtszgRlOyuTBXdZKN0PeEsso1hNslHUOLIRusB41NJnQQKlI3Pw==", "4779ff9e-e670-417e-aaad-c184bef1ea99" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a0c4140-4903-424b-87e1-550f90a6b31c", new DateTime(2025, 9, 24, 8, 26, 22, 854, DateTimeKind.Utc).AddTicks(5110), new DateTime(2025, 9, 24, 8, 26, 22, 854, DateTimeKind.Utc).AddTicks(5110), "AQAAAAIAAYagAAAAEACRms1frHRkd3sNaQC52TZtttl1AzJHrXYHem+9f6bcncFN2mnBBrZlznONb8p9aA==", "dffd8b36-9d11-4b19-96e6-07c40caa2a65" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 891, DateTimeKind.Utc).AddTicks(9280), new DateTime(2025, 9, 24, 8, 26, 22, 891, DateTimeKind.Utc).AddTicks(9380), new Guid("0558f460-f91e-4f60-bcd3-0c404ad0f536") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 891, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 9, 24, 8, 26, 22, 891, DateTimeKind.Utc).AddTicks(9480), new Guid("d0e1303d-005c-4169-87f3-385209765183") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(8320), new DateTime(2025, 9, 24, 8, 26, 22, 894, DateTimeKind.Utc).AddTicks(8320) });
        }
    }
}
