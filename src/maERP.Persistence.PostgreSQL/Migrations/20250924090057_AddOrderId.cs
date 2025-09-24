using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
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
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(3710), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4140), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4200), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4270), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4360), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4360), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4380), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 782, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 9, 24, 9, 0, 56, 782, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(1020), new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(1020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(690), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(890), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(890) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(900), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(900), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(900), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 9, 24, 9, 0, 56, 795, DateTimeKind.Utc).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(4350), new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(4350) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 9, 24, 9, 0, 56, 792, DateTimeKind.Utc).AddTicks(4450) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 775, DateTimeKind.Utc).AddTicks(7240), new DateTime(2025, 9, 24, 9, 0, 56, 775, DateTimeKind.Utc).AddTicks(7240) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43da66c5-374d-4737-8788-d533c4fb5f3e", new DateTime(2025, 9, 24, 9, 0, 56, 707, DateTimeKind.Utc).AddTicks(1970), new DateTime(2025, 9, 24, 9, 0, 56, 707, DateTimeKind.Utc).AddTicks(1970), "AQAAAAIAAYagAAAAEHf70Cq7ql8GNVunxmfIeXAkkpTVsxtnqLpoMA/dH+cwL58ezrnNcGFTOGobTV4q1A==", "329f077c-0028-4d33-af65-4ed3a733e40c" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "721e8594-fd50-4832-a64f-70bb26436bcf", new DateTime(2025, 9, 24, 9, 0, 56, 741, DateTimeKind.Utc).AddTicks(8330), new DateTime(2025, 9, 24, 9, 0, 56, 741, DateTimeKind.Utc).AddTicks(8330), "AQAAAAIAAYagAAAAEP2aTx6/mtnd/7S+4Ay9KEY/UN1G3vEVAZxQiMbwVJlquQpN1C0Zqdi3TSbkRy93Sg==", "3437da06-3e15-4aff-819e-d0f4b7991184" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 778, DateTimeKind.Utc).AddTicks(8340), new DateTime(2025, 9, 24, 9, 0, 56, 778, DateTimeKind.Utc).AddTicks(8440), new Guid("e6bb3694-c137-4afb-a54a-af335251bffd") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 778, DateTimeKind.Utc).AddTicks(8540), new DateTime(2025, 9, 24, 9, 0, 56, 778, DateTimeKind.Utc).AddTicks(8540), new Guid("30a55449-647b-4fae-812e-58abf056574b") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(5990), new DateTime(2025, 9, 24, 9, 0, 56, 781, DateTimeKind.Utc).AddTicks(5990) });
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
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6410), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6760), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(6770) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 210, DateTimeKind.Utc).AddTicks(3650), new DateTime(2025, 9, 24, 8, 26, 26, 210, DateTimeKind.Utc).AddTicks(3650) });

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 220, DateTimeKind.Utc).AddTicks(8200), new DateTime(2025, 9, 24, 8, 26, 26, 220, DateTimeKind.Utc).AddTicks(8200) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 9, 24, 8, 26, 26, 223, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1650), new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 9, 24, 8, 26, 26, 221, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 203, DateTimeKind.Utc).AddTicks(4150), new DateTime(2025, 9, 24, 8, 26, 26, 203, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e1d78ee-4c4c-451f-9b96-67b53c2c1bea", new DateTime(2025, 9, 24, 8, 26, 26, 135, DateTimeKind.Utc).AddTicks(1410), new DateTime(2025, 9, 24, 8, 26, 26, 135, DateTimeKind.Utc).AddTicks(1410), "AQAAAAIAAYagAAAAEGPtg2kUYeRBF1svdfmnJDwcC531ZVR8aaz9e9/to1fqPHjyQ7JPITObGmi/AFz0UQ==", "6bff7c99-06d1-4b2e-b8c4-22272eadee6d" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57c1cdfd-efd2-4217-b330-4de1256ba8b4", new DateTime(2025, 9, 24, 8, 26, 26, 169, DateTimeKind.Utc).AddTicks(3580), new DateTime(2025, 9, 24, 8, 26, 26, 169, DateTimeKind.Utc).AddTicks(3580), "AQAAAAIAAYagAAAAEG+Jn+yNTU5FigYbTfq+EZXmhsxTKVzKPXtM93gE54loScbrFXgms2OJm/SHJzhU9Q==", "bd70b65b-af15-4c20-9474-bab4a136e16f" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9200), new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9310), new Guid("7f6dbfe8-bea7-4fc4-9e9b-7fb75e04523d") });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9430), new DateTime(2025, 9, 24, 8, 26, 26, 206, DateTimeKind.Utc).AddTicks(9430), new Guid("56ede1a7-5076-4671-b41f-353695685e29") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 9, 24, 8, 26, 26, 209, DateTimeKind.Utc).AddTicks(8610) });
        }
    }
}
