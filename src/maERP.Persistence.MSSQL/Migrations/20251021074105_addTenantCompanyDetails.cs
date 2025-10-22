using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
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
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tenant",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tenant",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tenant",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iban",
                table: "tenant",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "tenant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "tenant",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "tenant",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "tenant",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "tenant",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "tenant",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5950), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5980), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(5990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6040), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6150), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6150), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6150), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6170), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6170), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6170), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6190), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6190), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6190), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 394, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 10, 21, 7, 41, 5, 394, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 403, DateTimeKind.Utc).AddTicks(9380), new DateTime(2025, 10, 21, 7, 41, 5, 403, DateTimeKind.Utc).AddTicks(9380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4110), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 404, DateTimeKind.Utc).AddTicks(2860), new DateTime(2025, 10, 21, 7, 41, 5, 404, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 404, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 10, 21, 7, 41, 5, 404, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 404, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 10, 21, 7, 41, 5, 404, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "City", "CompanyName", "Country", "DateCreated", "DateModified", "Iban", "Phone", "PostalCode", "State", "Street", "Street2", "Website" },
                values: new object[] { null, null, null, new DateTime(2025, 10, 21, 7, 41, 5, 387, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 10, 21, 7, 41, 5, 387, DateTimeKind.Utc).AddTicks(6580), null, null, null, null, null, null, null });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1074a2e-bae2-4a96-bec0-20f70c966a18", new DateTime(2025, 10, 21, 7, 41, 5, 351, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 10, 21, 7, 41, 5, 351, DateTimeKind.Utc).AddTicks(1770), "AQAAAAIAAYagAAAAEOeEFT3gRxzQ3PNsvkvDxKn91q5qw0yCBl53cZwEQRhtxQH3HP3rnPTrpwIf0w2oPg==", "bc8c8d17-4217-4b87-808e-dbe28c58dc72" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 390, DateTimeKind.Utc).AddTicks(9090), new DateTime(2025, 10, 21, 7, 41, 5, 390, DateTimeKind.Utc).AddTicks(9190), new Guid("b570acbf-2903-4cd2-b0c1-88dd1f0136b7"), false });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 10, 21, 7, 41, 5, 393, DateTimeKind.Utc).AddTicks(7860) });
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
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(6920), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(6920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7650), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7650), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7650), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7660), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7670), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7740), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7740), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7740), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7750), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7750), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7750), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7770), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7770), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7770), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7790), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7790), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7790), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7850), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7850), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7850), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7870), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7870), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7940), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7940), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7940), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 10, 18, 12, 45, 54, 640, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 641, DateTimeKind.Utc).AddTicks(6030), new DateTime(2025, 10, 18, 12, 45, 54, 641, DateTimeKind.Utc).AddTicks(6030) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(6810), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(6820) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7030), new DateTime(2025, 10, 18, 12, 45, 54, 655, DateTimeKind.Utc).AddTicks(7030) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(8330), new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 10, 18, 12, 45, 54, 651, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 633, DateTimeKind.Utc).AddTicks(9700), new DateTime(2025, 10, 18, 12, 45, 54, 633, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7fb7a483-b89f-44bc-8427-dadf7b1f3a37", new DateTime(2025, 10, 18, 12, 45, 54, 597, DateTimeKind.Utc).AddTicks(6830), new DateTime(2025, 10, 18, 12, 45, 54, 597, DateTimeKind.Utc).AddTicks(6830), "AQAAAAIAAYagAAAAEFYfiJXNK8m7+RHKTZGoA1c/QNuSBcnLn/8jp8u5QheahDdB5WhecVnvV9feDKeCzQ==", "13dcc4c6-8db8-4951-917e-653b251d4368" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 637, DateTimeKind.Utc).AddTicks(8910), new DateTime(2025, 10, 18, 12, 45, 54, 637, DateTimeKind.Utc).AddTicks(9020), new Guid("53bac928-c0ee-4a15-a109-8b615d54e9a9") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 10, 18, 12, 45, 54, 641, DateTimeKind.Utc).AddTicks(230), new DateTime(2025, 10, 18, 12, 45, 54, 641, DateTimeKind.Utc).AddTicks(230) });
        }
    }
}
