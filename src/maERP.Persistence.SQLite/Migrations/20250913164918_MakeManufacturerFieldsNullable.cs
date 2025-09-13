using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.SQLite.Migrations
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
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 0, DateTimeKind.Utc).AddTicks(9340), new DateTime(2025, 9, 13, 16, 49, 18, 0, DateTimeKind.Utc).AddTicks(9340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(350), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(360), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(360), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(370), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(370), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(410), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(410), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(420), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(420), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(430), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(430), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(430), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(440), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(440), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(510), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(520), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(520), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(530), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(530), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(530), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(540), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(540), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(550), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(550), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(560), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(560), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(560), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(570), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(570), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(580), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(580), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(580), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(590), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(590), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(600), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(600), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(610), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(610), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(610), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(620), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(620), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(630), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(630), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(630), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(640), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(640), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(650), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(650), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(650), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(660), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(660), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(670), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(670), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(680), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(680), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(690), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(690), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(700), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(700), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(700), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(710), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(710), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(730), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(730), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(740), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(740), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(750), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(750), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(760), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(760), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(760), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(770), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(770), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(780), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(780), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(790), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(800), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(800), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(810), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(810), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(820), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(820), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(830), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(830), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(830), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(840), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(840), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(850), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(850), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(860), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(860), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(860), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(870), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(870), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(880), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(880), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(890), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(890), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(900), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(900), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(900), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified", "Logo" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 2, DateTimeKind.Utc).AddTicks(2090), new DateTime(2025, 9, 13, 16, 49, 18, 2, DateTimeKind.Utc).AddTicks(2090), null });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 3, DateTimeKind.Utc).AddTicks(9530), new DateTime(2025, 9, 13, 16, 49, 18, 3, DateTimeKind.Utc).AddTicks(9530) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666601"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(7990), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666602"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8290), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8290) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666603"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8300), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8300) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666604"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8300), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8300) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666605"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8310), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8310) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666606"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8310), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8310) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666607"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8320), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8320) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666608"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8320), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8320) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666609"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8330), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666610"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8330), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666611"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8330), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666612"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8340), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666613"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8340), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8340) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8350), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8350), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8350) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8360), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8360), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8370), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8370), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 9, 13, 16, 49, 18, 9, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 4, DateTimeKind.Utc).AddTicks(5100), new DateTime(2025, 9, 13, 16, 49, 18, 4, DateTimeKind.Utc).AddTicks(5100) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 4, DateTimeKind.Utc).AddTicks(5360), new DateTime(2025, 9, 13, 16, 49, 18, 4, DateTimeKind.Utc).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 4, DateTimeKind.Utc).AddTicks(5370), new DateTime(2025, 9, 13, 16, 49, 18, 4, DateTimeKind.Utc).AddTicks(5370) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 17, 992, DateTimeKind.Utc).AddTicks(8470), new DateTime(2025, 9, 13, 16, 49, 17, 992, DateTimeKind.Utc).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0545a43-95cf-4aa2-a55d-0b3d1c45259f", new DateTime(2025, 9, 13, 16, 49, 17, 892, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 9, 13, 16, 49, 17, 892, DateTimeKind.Utc).AddTicks(930), "AQAAAAIAAYagAAAAEAj+qyzmpgt3APAZZ9evSOGBQ3aVXMTx4VIQ2aSoPX9UJBS3tRuanMaJ4B9BMN8C4Q==", "f72a5e1c-0a34-488e-a720-ef4dc63989f1" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1de205dc-41bb-4db2-96de-2e698394297b", new DateTime(2025, 9, 13, 16, 49, 17, 942, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 9, 13, 16, 49, 17, 942, DateTimeKind.Utc).AddTicks(4280), "AQAAAAIAAYagAAAAEFF8Z6IAabOZwizu9riwUTmUwlP+UDpmn6a3zRQ+Gcqk4hJUVKKy+HFg21hnPIifeA==", "a6c36013-4bb8-4cf2-8822-c52686661f68" });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(3670), new DateTime(2025, 9, 13, 16, 49, 18, 1, DateTimeKind.Utc).AddTicks(3670) });
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
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Website",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "manufacturer",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7230), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7690), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7700), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7730), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7740), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7750), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7750), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7750), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7770), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7770), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7770), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7790), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7790), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7790), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7850), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7850), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7850), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7870), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7870), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7870), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7880), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7880), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7880), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7900), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7900), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7900), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7920), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7940), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7940), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7940), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7950), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7990), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7990), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7990), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8000), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8000), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8000), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8050), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8050), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8050), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8080), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8080), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8080), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(8080) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified", "Logo" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 439, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 9, 11, 10, 47, 27, 439, DateTimeKind.Utc).AddTicks(5390), "" });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(5550), new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(5550) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666601"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666602"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666603"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666604"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666605"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666606"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666607"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666608"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666609"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666610"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666611"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1860) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666612"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1870), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666613"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1870), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1870), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1880), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1880), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1880), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1890) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1900), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1900), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1900), new DateTime(2025, 9, 11, 10, 47, 27, 444, DateTimeKind.Utc).AddTicks(1900) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(8830), new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(8830) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(8940), new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(8940), new DateTime(2025, 9, 11, 10, 47, 27, 440, DateTimeKind.Utc).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 433, DateTimeKind.Utc).AddTicks(3240), new DateTime(2025, 9, 11, 10, 47, 27, 433, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa0d768c-cd84-48e2-9556-90044dacc99a", new DateTime(2025, 9, 11, 10, 47, 27, 363, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 9, 11, 10, 47, 27, 363, DateTimeKind.Utc).AddTicks(6130), "AQAAAAIAAYagAAAAEHOKBozinENyvDAFwZdDIv2gt01zXEufWV+Xi6wiJLE4/E3TyIPidrjXC+eFfzno+Q==", "1eed715e-b33a-46c6-823f-14176d47e5ef" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3be43e33-c353-4a13-99bf-788dc5192f02", new DateTime(2025, 9, 11, 10, 47, 27, 398, DateTimeKind.Utc).AddTicks(5970), new DateTime(2025, 9, 11, 10, 47, 27, 398, DateTimeKind.Utc).AddTicks(5970), "AQAAAAIAAYagAAAAECzfLadCmurlAEJAOpEGLuxx8lKgQaiuhkHctyyXeLwniJ20v08jwmp/3OTxwmp+rw==", "3f1b1868-88fc-4792-bc58-fd0169a375a4" });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(9950), new DateTime(2025, 9, 11, 10, 47, 27, 438, DateTimeKind.Utc).AddTicks(9950) });
        }
    }
}
