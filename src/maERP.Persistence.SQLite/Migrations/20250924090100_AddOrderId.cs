using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.SQLite.Migrations
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
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5330), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 874, DateTimeKind.Utc).AddTicks(3420), new DateTime(2025, 9, 24, 9, 0, 59, 874, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 884, DateTimeKind.Utc).AddTicks(9970), new DateTime(2025, 9, 24, 9, 0, 59, 884, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(460), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(700), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(700), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(710), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(710), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(710), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 24, 9, 0, 59, 888, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 885, DateTimeKind.Utc).AddTicks(3600), new DateTime(2025, 9, 24, 9, 0, 59, 885, DateTimeKind.Utc).AddTicks(3600) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 885, DateTimeKind.Utc).AddTicks(3710), new DateTime(2025, 9, 24, 9, 0, 59, 885, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 885, DateTimeKind.Utc).AddTicks(3720), new DateTime(2025, 9, 24, 9, 0, 59, 885, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 867, DateTimeKind.Utc).AddTicks(6980), new DateTime(2025, 9, 24, 9, 0, 59, 867, DateTimeKind.Utc).AddTicks(6980) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f0cdc55-6c5c-46c4-9697-9b308a978f91", new DateTime(2025, 9, 24, 9, 0, 59, 797, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 9, 24, 9, 0, 59, 797, DateTimeKind.Utc).AddTicks(2750), "AQAAAAIAAYagAAAAEIAhTLAx43XDJc9sFtqe+uU0y+Qx9y64uERJ9MqiebnWQwY7sNomjgtHoOPUVTsC3g==", "a2ed6a52-37bf-405b-8c5d-ad4f91860f0e" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caeb6294-03d9-4b56-984d-de5ffc16d867", new DateTime(2025, 9, 24, 9, 0, 59, 832, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 9, 24, 9, 0, 59, 832, DateTimeKind.Utc).AddTicks(3520), "AQAAAAIAAYagAAAAECHSuBmLR1OXGQFSpLtVNAzKIaRlkLj1ggrgG7luuN+8s1ZbmYwJgtZ4V334Nunh/A==", "5ec096f1-482b-4ab6-bcb0-ae2a2b2fd629" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 870, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 9, 24, 9, 0, 59, 870, DateTimeKind.Utc).AddTicks(8870), new Guid("77d81bdb-2659-45ce-b637-2bb00cd7a382") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 870, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 9, 0, 59, 870, DateTimeKind.Utc).AddTicks(8980), new Guid("04acf4e8-4720-4e57-8abf-5da9c22b3bc9") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(8110), new DateTime(2025, 9, 24, 9, 0, 59, 873, DateTimeKind.Utc).AddTicks(8110) });
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
    }
}
