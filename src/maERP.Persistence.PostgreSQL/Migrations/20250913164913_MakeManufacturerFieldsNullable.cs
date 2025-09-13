using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class MakeManufacturerFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliverAddressZip",
                table: "order",
                newName: "DeliveryAddressZip");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "manufacturer",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "manufacturer",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "manufacturer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "manufacturer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "manufacturer",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "manufacturer",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "manufacturer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4090), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4810), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4820), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4830), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4840), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4850), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4860), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4890), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4900), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4920), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4920), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4940), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4950), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4960), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4980), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4990), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4990), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4990), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5020), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5020), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5040), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5050), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5050), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5060), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5060), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5060), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5070), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5070), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5070), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5310), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5310), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5310), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5330), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5330), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5340), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(5340) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified", "Logo" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 140, DateTimeKind.Utc).AddTicks(5620), new DateTime(2025, 9, 13, 16, 49, 13, 140, DateTimeKind.Utc).AddTicks(5620), null });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(290), new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(290) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666601"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666602"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666603"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666604"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666605"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9270), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666606"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9270), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666607"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9270), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666608"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9280), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666609"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9280), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666610"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9290), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666611"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9290), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666612"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9300), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666613"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9300), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9310), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9310), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9310), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9320), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9320), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9330), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9330), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 9, 13, 16, 49, 13, 147, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(5020), new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 9, 13, 16, 49, 13, 142, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 131, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 9, 13, 16, 49, 13, 131, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7226a718-77b7-41c5-b197-6ae53d7f2bbe", new DateTime(2025, 9, 13, 16, 49, 13, 40, DateTimeKind.Utc).AddTicks(5610), new DateTime(2025, 9, 13, 16, 49, 13, 40, DateTimeKind.Utc).AddTicks(5610), "AQAAAAIAAYagAAAAENJQjgiY0io9o6BQQexxCQIpFiGVbCgoAK3NORodko4mOH/3UbH24+B2hYYYJzbCdg==", "e8a5e4ff-a81a-45d6-bb0f-ca4290650412" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c26f483e-3ec2-4bf6-bb2a-d63cb7d0a210", new DateTime(2025, 9, 13, 16, 49, 13, 86, DateTimeKind.Utc).AddTicks(2200), new DateTime(2025, 9, 13, 16, 49, 13, 86, DateTimeKind.Utc).AddTicks(2200), "AQAAAAIAAYagAAAAEEFu4L1PD8JTVTEZQ6ZbAi4lzH5YXy+iPcbji7OuPntVpw3S/5s+ff9/SpsCEjwiwQ==", "d6a9b5fd-3291-46e2-a4df-af6f63da120d" });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 9, 13, 16, 49, 13, 139, DateTimeKind.Utc).AddTicks(8430) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryAddressZip",
                table: "order",
                newName: "DeliverAddressZip");

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "manufacturer",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "manufacturer",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "manufacturer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "manufacturer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "manufacturer",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "manufacturer",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "manufacturer",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2900), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2900), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2900), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2910), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2910), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2910), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3020), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3020), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3020), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3170), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3170), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3170), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3180), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3180), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3180), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3200), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3200), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3200), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3220), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3250), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified", "Logo" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 322, DateTimeKind.Utc).AddTicks(340), new DateTime(2025, 9, 11, 10, 47, 24, 322, DateTimeKind.Utc).AddTicks(340), "" });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(420), new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666601"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(5860), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666602"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666603"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666604"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666605"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666606"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666607"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6070), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6070) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666608"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666609"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666610"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6080), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6080) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666611"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666612"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666613"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6100), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6120), new DateTime(2025, 9, 11, 10, 47, 24, 326, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(3760), new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(3760) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(3860), new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(3870), new DateTime(2025, 9, 11, 10, 47, 24, 323, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 315, DateTimeKind.Utc).AddTicks(9140), new DateTime(2025, 9, 11, 10, 47, 24, 315, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2776ba3-8687-4b5e-8500-e306446f2779", new DateTime(2025, 9, 11, 10, 47, 24, 246, DateTimeKind.Utc).AddTicks(3320), new DateTime(2025, 9, 11, 10, 47, 24, 246, DateTimeKind.Utc).AddTicks(3320), "AQAAAAIAAYagAAAAEEE5Lh42vy67KCRtwSo5hcqVTzjNHsu2/eVwy/ZSXIbzLQjUaW6B0uk09mqTWDYASg==", "f422928d-f7a1-4be8-afba-fe27bb561450" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8b55cdf-4870-4f5c-baea-c6ef15731a5e", new DateTime(2025, 9, 11, 10, 47, 24, 280, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 9, 11, 10, 47, 24, 280, DateTimeKind.Utc).AddTicks(8790), "AQAAAAIAAYagAAAAELaEBF4lH72CFTKmy78ILr9aMa+vt1J3Xl9V+OL7bIFvsgJVazw/yB3SpK/h8SHXJA==", "322bd87c-0685-4000-a1f4-3b298a324ac6" });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(5030), new DateTime(2025, 9, 11, 10, 47, 24, 321, DateTimeKind.Utc).AddTicks(5030) });
        }
    }
}
