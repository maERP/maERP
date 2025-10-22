using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class addTenantCompanyDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RoleManageTenant",
                table: "user_tenant",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tenant",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tenant",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tenant",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iban",
                table: "tenant",
                type: "TEXT",
                maxLength: 34,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "tenant",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "tenant",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "tenant",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "tenant",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "tenant",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "tenant",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5720), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5800), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5820), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 115, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 10, 21, 7, 41, 12, 115, DateTimeKind.Utc).AddTicks(2290) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(3830), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4030), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4030), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4030), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4050), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(4990), new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 10, 21, 7, 41, 12, 124, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "City", "CompanyName", "Country", "DateCreated", "DateModified", "Iban", "Phone", "PostalCode", "State", "Street", "Street2", "Website" },
                values: new object[] { null, null, null, new DateTime(2025, 10, 21, 7, 41, 12, 109, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 10, 21, 7, 41, 12, 109, DateTimeKind.Utc).AddTicks(5420), null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64b4a862-f951-4e29-ac10-b960f2b31cb0", new DateTime(2025, 10, 21, 7, 41, 12, 77, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 10, 21, 7, 41, 12, 77, DateTimeKind.Utc).AddTicks(390), "AQAAAAIAAYagAAAAECD/Sdqxb+v2sgaXe96WZDUVdEmh8X95NgLTXADR6B1Q7spgZG14uAtdHR65J82nFQ==", "d29d1b9e-5d27-44c1-9e2c-21a6d699469d" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 112, DateTimeKind.Utc).AddTicks(160), new DateTime(2025, 10, 21, 7, 41, 12, 112, DateTimeKind.Utc).AddTicks(250), new Guid("b929520a-79f0-4602-9300-b6405fe3b4d3"), false });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(7490), new DateTime(2025, 10, 21, 7, 41, 12, 114, DateTimeKind.Utc).AddTicks(7490) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleManageTenant",
                table: "user_tenant");

            migrationBuilder.DropColumn(
                name: "City",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "Iban",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "State",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "Street2",
                table: "tenant");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "tenant");

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9290), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9770), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9770), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9770), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9780), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9780), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9790), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9790), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9790), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9810), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9810), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9810), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9820), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9820), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9820), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9830), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9840), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9840), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9860), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9860), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9860), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9890), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9890), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9890), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9910), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9910), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9910), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9920), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9930), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9970), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9970), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9990), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9990), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9990), new DateTime(2025, 10, 18, 12, 46, 1, 473, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(10), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(10), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(10), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(10) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(20) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(30), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(30) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(30), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(30) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(30), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(30) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(40), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(50) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(60), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(60) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(60), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(60) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(60), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(60) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(70) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(80), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(80) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(80), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(80) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(80), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(80) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(90) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(150), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(150) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(6790), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(6790) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(490), new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4040), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 10, 18, 12, 46, 1, 488, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(3710), new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(3820), new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(3830), new DateTime(2025, 10, 18, 12, 46, 1, 484, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 468, DateTimeKind.Utc).AddTicks(3180), new DateTime(2025, 10, 18, 12, 46, 1, 468, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a5bb30a-d9f0-459a-b1c1-260de55167ad", new DateTime(2025, 10, 18, 12, 46, 1, 432, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 10, 18, 12, 46, 1, 432, DateTimeKind.Utc).AddTicks(3530), "AQAAAAIAAYagAAAAEGEUmzDVjOd/OjEG/2Gqc1saxFkxZhEQUOH013D+HWW/fJq7wUsZ3xNQ/yZYkvrAew==", "c615b8c0-0fb8-4f87-9423-6845d390480b" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 471, DateTimeKind.Utc).AddTicks(5710), new DateTime(2025, 10, 18, 12, 46, 1, 471, DateTimeKind.Utc).AddTicks(5830), new Guid("7020bf05-4566-4462-97ca-fa03fc410701") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(2130), new DateTime(2025, 10, 18, 12, 46, 1, 474, DateTimeKind.Utc).AddTicks(2130) });
        }
    }
}
