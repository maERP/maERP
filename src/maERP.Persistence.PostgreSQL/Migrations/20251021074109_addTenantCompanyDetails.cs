using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
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
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tenant",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tenant",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tenant",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iban",
                table: "tenant",
                type: "character varying(34)",
                maxLength: 34,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "tenant",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "tenant",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "tenant",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "tenant",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "tenant",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "tenant",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4630), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4630), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4670), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4760), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4780), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4780), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4780), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4800), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4800), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4800), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4810), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4810), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4810), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4920), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 878, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 10, 21, 7, 41, 8, 878, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(1010), new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(1010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(4610) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 10, 21, 7, 41, 8, 888, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "City", "CompanyName", "Country", "DateCreated", "DateModified", "Iban", "Phone", "PostalCode", "State", "Street", "Street2", "Website" },
                values: new object[] { null, null, null, new DateTime(2025, 10, 21, 7, 41, 8, 872, DateTimeKind.Utc).AddTicks(590), new DateTime(2025, 10, 21, 7, 41, 8, 872, DateTimeKind.Utc).AddTicks(590), null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb587237-9e0c-4d80-82bf-a044a603df6d", new DateTime(2025, 10, 21, 7, 41, 8, 836, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 10, 21, 7, 41, 8, 836, DateTimeKind.Utc).AddTicks(8710), "AQAAAAIAAYagAAAAEJOT/81FcNzPNwjei/17IAzMFh91rdKNUbO1+De8/FKhb2cFI/vXM4QUbA6Kz+2C/A==", "d86199b2-45cd-457c-b23d-e72a3e7eb66d" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 874, DateTimeKind.Utc).AddTicks(7890), new DateTime(2025, 10, 21, 7, 41, 8, 874, DateTimeKind.Utc).AddTicks(8010), new Guid("0c1a66ae-4dde-4ea1-8b37-1b9273e51a75"), false });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 10, 21, 7, 41, 8, 877, DateTimeKind.Utc).AddTicks(6630) });
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
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4800), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4810), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4810), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4920), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4920), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5020), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5020), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5020), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5050), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5070), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5070), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5070), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5110), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5130), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5130), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5130), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5140), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5140), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5140), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5160), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5160), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5160), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 887, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 10, 18, 12, 45, 57, 887, DateTimeKind.Utc).AddTicks(2370) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9550), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9550) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9560), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9560) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9560), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9560) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9560), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9560) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 10, 18, 12, 45, 57, 901, DateTimeKind.Utc).AddTicks(9580) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(7040), new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(7150), new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(7150) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(7150), new DateTime(2025, 10, 18, 12, 45, 57, 897, DateTimeKind.Utc).AddTicks(7150) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 880, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 10, 18, 12, 45, 57, 880, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fafe080b-4a4a-47e3-835a-38eca691ee51", new DateTime(2025, 10, 18, 12, 45, 57, 843, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 10, 18, 12, 45, 57, 843, DateTimeKind.Utc).AddTicks(8960), "AQAAAAIAAYagAAAAEH/mjJSlHHljhiao+dSKykvEzUxmGSQrCFFsNOMe92akAIdjExzUTPPn3AMEOu7Kgg==", "d0aa6fcf-09c6-4af9-8fd5-3f39ee0cbff3" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 883, DateTimeKind.Utc).AddTicks(6910), new DateTime(2025, 10, 18, 12, 45, 57, 883, DateTimeKind.Utc).AddTicks(7020), new Guid("747bfb24-a411-438e-8927-5b85333d7de7") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(7100), new DateTime(2025, 10, 18, 12, 45, 57, 886, DateTimeKind.Utc).AddTicks(7100) });
        }
    }
}
