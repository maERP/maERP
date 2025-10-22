using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MySQL.Migrations
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
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tenant",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tenant",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tenant",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Iban",
                table: "tenant",
                type: "varchar(34)",
                maxLength: 34,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "tenant",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "tenant",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "tenant",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "tenant",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "tenant",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "tenant",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1070), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1440), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1460), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1510), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1510), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1510), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1530), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1530), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1530), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1590), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3770), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3960), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 10, 21, 7, 40, 51, 732, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 40, 51, 728, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "City", "CompanyName", "Country", "DateCreated", "DateModified", "Iban", "Phone", "PostalCode", "State", "Street", "Street2", "Website" },
                values: new object[] { null, null, null, new DateTime(2025, 10, 21, 7, 40, 51, 714, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 10, 21, 7, 40, 51, 714, DateTimeKind.Utc).AddTicks(1490), null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16db10f1-b574-4d51-b103-70d447b3a83d", new DateTime(2025, 10, 21, 7, 40, 51, 683, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 10, 21, 7, 40, 51, 683, DateTimeKind.Utc).AddTicks(3500), "AQAAAAIAAYagAAAAEKENqsvMRYo353bgKkPvlKV3RHn6HidT6v1ER6lnuu8YZql721tNtNE2rBCxZYEZSQ==", "9378f3f0-d28d-4336-8fa1-525ecd18ca81" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 716, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 10, 21, 7, 40, 51, 716, DateTimeKind.Utc).AddTicks(7500), new Guid("b11e169e-6a6a-4f1a-a865-c289be6d77b4"), false });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 10, 21, 7, 40, 51, 719, DateTimeKind.Utc).AddTicks(3120) });
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
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(3700), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(3700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4110), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4110), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4120), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4120), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4120), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4360), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4360), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4360), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 166, DateTimeKind.Utc).AddTicks(880), new DateTime(2025, 10, 18, 12, 45, 51, 166, DateTimeKind.Utc).AddTicks(880) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 10, 18, 12, 45, 51, 179, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(7520), new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 10, 18, 12, 45, 51, 175, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 160, DateTimeKind.Utc).AddTicks(1060), new DateTime(2025, 10, 18, 12, 45, 51, 160, DateTimeKind.Utc).AddTicks(1070) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4476d0ba-8562-4859-88d0-45e68af11d94", new DateTime(2025, 10, 18, 12, 45, 51, 128, DateTimeKind.Utc).AddTicks(680), new DateTime(2025, 10, 18, 12, 45, 51, 128, DateTimeKind.Utc).AddTicks(680), "AQAAAAIAAYagAAAAEJmpHFsC1Upu8JuZBTPmZkW0HWDCs08rHyivvpGPHBI8awFHnks1/RHXilvdxq7qVg==", "c7d1da75-0b0f-4f61-acb8-f533be659a94" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 162, DateTimeKind.Utc).AddTicks(9040), new DateTime(2025, 10, 18, 12, 45, 51, 162, DateTimeKind.Utc).AddTicks(9130), new Guid("feee52f3-3b5e-4666-8d68-df979dc5c436") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 18, 12, 45, 51, 165, DateTimeKind.Utc).AddTicks(6070) });
        }
    }
}
