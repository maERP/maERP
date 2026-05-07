using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "refresh_token",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Family = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsPersistent = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_token", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4250), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4660), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4660), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4660), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4670), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4670), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4670), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4690), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4690), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4690), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4710), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4730), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4730), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4730), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4750), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4750), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4750), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4770), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4770), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4770), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4790), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4790), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4790), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4810), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4810), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4810), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4820), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4820), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4820), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4840), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4840), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4840), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4860), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4860), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4860), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4880), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4880), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4880), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4900), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4900), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4900), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4920), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4990), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4990), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4990), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5010), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5010), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5010), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5050), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5050), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(5050) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 873, DateTimeKind.Utc).AddTicks(1770), new DateTime(2026, 5, 7, 11, 16, 40, 873, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "7210c2dd-e533-479b-9d9e-3b2a679a4e50");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "d5e84c79-9f5a-40c0-8784-88eeed640eaf");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 882, DateTimeKind.Utc).AddTicks(1660), new DateTime(2026, 5, 7, 11, 16, 40, 882, DateTimeKind.Utc).AddTicks(1660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(4780), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(4990), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(4990), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(4990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5010), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5010), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5010), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5010) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5020) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5030) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5040), new DateTime(2026, 5, 7, 11, 16, 40, 890, DateTimeKind.Utc).AddTicks(5040) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 883, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 7, 11, 16, 40, 883, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 883, DateTimeKind.Utc).AddTicks(2840), new DateTime(2026, 5, 7, 11, 16, 40, 883, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 883, DateTimeKind.Utc).AddTicks(2840), new DateTime(2026, 5, 7, 11, 16, 40, 883, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 866, DateTimeKind.Utc).AddTicks(9950), new DateTime(2026, 5, 7, 11, 16, 40, 866, DateTimeKind.Utc).AddTicks(9950) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e0f375a-be59-47ab-b72e-2aa8d19f311e", new DateTime(2026, 5, 7, 11, 16, 40, 830, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 7, 11, 16, 40, 830, DateTimeKind.Utc).AddTicks(2760), "AQAAAAIAAYagAAAAEJcTeh3bUzecya79MDn+3Q2SnbefFLkDHN9O8VnqpIlpyxzMeAwu/VCjhJxe1ZZvTA==", "8ae050b3-be9d-4dbe-af11-eeb84d9f91a2" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 870, DateTimeKind.Utc).AddTicks(1060), new DateTime(2026, 5, 7, 11, 16, 40, 870, DateTimeKind.Utc).AddTicks(1160), new Guid("699b1823-7c3e-4882-9efe-a3db0a0f43e0") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(6660), new DateTime(2026, 5, 7, 11, 16, 40, 872, DateTimeKind.Utc).AddTicks(6660) });

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_ExpiresAt",
                table: "refresh_token",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_Family",
                table: "refresh_token",
                column: "Family");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_TokenHash",
                table: "refresh_token",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_token_UserId",
                table: "refresh_token",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "refresh_token");

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3130), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3140), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3140), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3140), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3150), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3150), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3150), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3160), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3160), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3160), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3180), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3180), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3180), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3260), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3260), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3260), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3280), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3280), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3280), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3290), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3290), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3290), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3310), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3390), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3400), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3400), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3400), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3410), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3410), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3410), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3430), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3430), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3430), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3450), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3450), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3450), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3460), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3460), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3460), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3490), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3500), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3500), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3500), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3520), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3520), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3520), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3540), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3540), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3540), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3550), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3570), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3570), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3570), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(3580) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 624, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 5, 2, 20, 7, 34, 624, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "cfe1b413-f688-4f25-9464-7322d96508a0");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "8d8a23d8-4043-43bb-9b00-caa35ceb051c");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 632, DateTimeKind.Utc).AddTicks(8690), new DateTime(2026, 5, 2, 20, 7, 34, 632, DateTimeKind.Utc).AddTicks(8690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1170), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 633, DateTimeKind.Utc).AddTicks(9120), new DateTime(2026, 5, 2, 20, 7, 34, 633, DateTimeKind.Utc).AddTicks(9120) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 633, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 2, 20, 7, 34, 633, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 633, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 2, 20, 7, 34, 633, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 617, DateTimeKind.Utc).AddTicks(8620), new DateTime(2026, 5, 2, 20, 7, 34, 617, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa9fa916-6d0a-44b5-b1b9-28471c9a886d", new DateTime(2026, 5, 2, 20, 7, 34, 581, DateTimeKind.Utc).AddTicks(3590), new DateTime(2026, 5, 2, 20, 7, 34, 581, DateTimeKind.Utc).AddTicks(3590), "AQAAAAIAAYagAAAAEJWWMPedVaJ4S6yrZyvbaex8jPOIIb6nlFoVf9j9kAiHfGkOvGZ01cqPNuO1X1rilA==", "7873d615-fdfc-4ecd-83dc-fa72e4b5c907" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 620, DateTimeKind.Utc).AddTicks(9670), new DateTime(2026, 5, 2, 20, 7, 34, 620, DateTimeKind.Utc).AddTicks(9770), new Guid("0312e4db-2f87-4bcc-9d06-98e795fe2d93") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(5210), new DateTime(2026, 5, 2, 20, 7, 34, 623, DateTimeKind.Utc).AddTicks(5210) });
        }
    }
}
