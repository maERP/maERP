using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
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
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "manufacturer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "manufacturer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "manufacturer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "manufacturer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "manufacturer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "manufacturer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9130), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9150), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9150), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9160), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9210), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9220), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9220), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9230), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9230), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9270), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9270), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9280), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9280), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9290), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9290), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9300), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9300), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9310), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9310), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9320), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9320), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9330), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9330), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9350), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9350), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9360), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9360), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9370), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9370), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9380), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9380), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9390), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9400), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9400), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9410), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9410), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9420), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9420), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9430), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9430), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9440), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9440), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9460), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9460), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9470), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9470), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9500), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9500), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9510), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9510), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9520), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9520), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9530), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9530), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9540), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9540), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9550), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9550), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9560), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9560), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9660), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9660), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9700), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9700), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9730), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9730), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9730), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9740), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9740), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9770), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9770), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9780), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9780), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9790), new DateTime(2025, 9, 13, 16, 49, 8, 818, DateTimeKind.Utc).AddTicks(9790) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified", "Logo" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 820, DateTimeKind.Utc).AddTicks(3390), new DateTime(2025, 9, 13, 16, 49, 8, 820, DateTimeKind.Utc).AddTicks(3390), null });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(460), new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(460) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666601"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9070), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666602"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9610) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666603"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9620) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666604"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9620) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666605"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9630) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666606"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9640) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666607"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9640) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666608"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666609"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666610"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9660), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666611"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9660), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666612"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666613"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9670), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9680), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9690), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9700), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9700), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9710), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9720) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9720), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9730) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9730), new DateTime(2025, 9, 13, 16, 49, 8, 828, DateTimeKind.Utc).AddTicks(9730) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(6010) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 9, 13, 16, 49, 8, 822, DateTimeKind.Utc).AddTicks(6020) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 807, DateTimeKind.Utc).AddTicks(3720), new DateTime(2025, 9, 13, 16, 49, 8, 807, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66d59837-4b62-451f-a505-c7d7fc4ad6a8", new DateTime(2025, 9, 13, 16, 49, 8, 687, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 9, 13, 16, 49, 8, 687, DateTimeKind.Utc).AddTicks(2990), "AQAAAAIAAYagAAAAEGfsBjhlPV/DqOhgWg5DzYg27iwFa0u1BRr68NFKbkHwCHsUbbs9N/pOJ4/dsv5u/Q==", "0c0b7b75-41f9-434c-a5fd-abc82f43a2b6" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2b1f0ad-1cfc-4e38-84a1-7e5421b44968", new DateTime(2025, 9, 13, 16, 49, 8, 746, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 9, 13, 16, 49, 8, 746, DateTimeKind.Utc).AddTicks(4970), "AQAAAAIAAYagAAAAEAqO75mEWCbE0rtV6elqcMYgs165T38yuZfyb5vY7D4UwDfSLpGdtu6TeBSgGvNJBA==", "79f47c99-916f-42ba-8508-e72753643981" });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 8, 819, DateTimeKind.Utc).AddTicks(3680), new DateTime(2025, 9, 13, 16, 49, 8, 819, DateTimeKind.Utc).AddTicks(3680) });
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
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "manufacturer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "manufacturer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "manufacturer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "manufacturer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "manufacturer",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "manufacturer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5360), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5370), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5410), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5410), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5410), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5500), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5500), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5500), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5510), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5510), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5510), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5550), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5550), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5550), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5570), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5570), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5570), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5580), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5600), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5680), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5680), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5680), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5700), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5700), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5700), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5720), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5720), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5720), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5730), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5750), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified", "Logo" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 55, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 9, 11, 10, 47, 21, 55, DateTimeKind.Utc).AddTicks(2670), "" });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(2270) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666601"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7160), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7160) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666602"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666603"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666604"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666605"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666606"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666607"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666608"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666609"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666610"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666611"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666612"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666613"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7430), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 9, 11, 10, 47, 21, 59, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(5360), new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(5360), new DateTime(2025, 9, 11, 10, 47, 21, 56, DateTimeKind.Utc).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 48, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 11, 10, 47, 21, 48, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cea90652-ccb8-423a-87ec-7ba76ca7af53", new DateTime(2025, 9, 11, 10, 47, 20, 976, DateTimeKind.Utc).AddTicks(8580), new DateTime(2025, 9, 11, 10, 47, 20, 976, DateTimeKind.Utc).AddTicks(8580), "AQAAAAIAAYagAAAAEL2JqQLRQCxeladwpNiJOl0V+Ib235U+yQ1kNnqdNWMLgQjRtOiO6QUzDR8qjIGj5A==", "36a29eca-1dcc-4b7e-abb4-1bbd17c5b307" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a30b0e5-8e59-45bf-b423-a321136c9a90", new DateTime(2025, 9, 11, 10, 47, 21, 12, DateTimeKind.Utc).AddTicks(3620), new DateTime(2025, 9, 11, 10, 47, 21, 12, DateTimeKind.Utc).AddTicks(3620), "AQAAAAIAAYagAAAAEBfiBmI/PmT5cZsMyzpm42QSQVGhEStZsPSjbeuNnAF7wT/+2hbsK2SilmvLeaC+Og==", "c6cabd84-e94a-4c5f-aeef-8c2aadcfc643" });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(7420), new DateTime(2025, 9, 11, 10, 47, 21, 54, DateTimeKind.Utc).AddTicks(7420) });
        }
    }
}
