using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Family = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplacedByTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPersistent = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8300), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8890), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8910), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8910), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8910), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8920), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8920), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8920), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8930), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8930), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8930), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8950), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8950), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8950), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8960), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8960), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8960), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8970), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8990), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8990), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8990), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9000), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9000), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9000), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9010), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9010), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9010), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9060), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9060), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9060), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9070), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9070), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9070), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9080), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9100), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9100), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9100), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9110), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9110), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9110), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9120), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9120), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9120), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9140), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9140), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9140), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9150), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9150), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9150), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9160), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9180), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9180), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9180), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9190), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9190), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9190), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9200), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9220), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9220), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9220), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9250), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9250), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9250), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9260), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9260), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9260), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9280), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9280), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9280), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9290), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9290), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9290), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9300), new DateTime(2026, 5, 7, 11, 16, 21, 804, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 805, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 7, 11, 16, 21, 805, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "edb6a10d-1480-4f96-8a1e-5f476029892d");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "00d424cb-01f3-46bd-a3d1-82b062e0569c");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 814, DateTimeKind.Utc).AddTicks(8260), new DateTime(2026, 5, 7, 11, 16, 21, 814, DateTimeKind.Utc).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7260), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7260) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7470), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7470), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7530), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 7, 11, 16, 21, 823, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 815, DateTimeKind.Utc).AddTicks(9110), new DateTime(2026, 5, 7, 11, 16, 21, 815, DateTimeKind.Utc).AddTicks(9110) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 815, DateTimeKind.Utc).AddTicks(9220), new DateTime(2026, 5, 7, 11, 16, 21, 815, DateTimeKind.Utc).AddTicks(9220) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 815, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 7, 11, 16, 21, 815, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 796, DateTimeKind.Utc).AddTicks(5820), new DateTime(2026, 5, 7, 11, 16, 21, 796, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3803bd1e-41e9-4c81-b4ec-cc94539c1c3c", new DateTime(2026, 5, 7, 11, 16, 21, 755, DateTimeKind.Utc).AddTicks(5240), new DateTime(2026, 5, 7, 11, 16, 21, 755, DateTimeKind.Utc).AddTicks(5240), "AQAAAAIAAYagAAAAEOMctTGSdS8OUww7DHgqII8x5aVnYSq1aHiWSeeA8MVcZfp5Q66aHg8irlaC747HAQ==", "3078ea25-2396-4ef9-bf11-fa1aaa17f7eb" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 800, DateTimeKind.Utc).AddTicks(2260), new DateTime(2026, 5, 7, 11, 16, 21, 800, DateTimeKind.Utc).AddTicks(2370), new Guid("2a4fc254-fd26-436e-a841-c30fb3a3bb7a") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 7, 11, 16, 21, 805, DateTimeKind.Utc).AddTicks(1970), new DateTime(2026, 5, 7, 11, 16, 21, 805, DateTimeKind.Utc).AddTicks(1970) });

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
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(5720), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6120), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6120), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6140), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6140), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6140), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6160), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6160), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6160), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6170), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6170), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6170), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6190), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6190), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6190), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6210), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6210), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6210), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6230), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6230), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6230), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6240), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6240), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6240), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6260), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6260), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6260), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6280), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6280), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6280), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6290), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6310), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6310), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6310), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6310) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6320) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6330), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6330) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6360), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6360), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6360), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6370) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6380), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6380), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6380), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6400), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6400), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6400), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6410), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6410), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6410), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6430), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6430), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6430), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6450), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6450), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6450), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6460), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6460), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6460), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6480), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6480), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6480), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6490), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6490), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6490), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(6490) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 919, DateTimeKind.Utc).AddTicks(3260), new DateTime(2026, 5, 2, 20, 7, 30, 919, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "4bcb96ba-b1be-4878-b099-f09f499f6a07");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "71cc6d30-fccd-4886-8d00-0e249007b194");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 927, DateTimeKind.Utc).AddTicks(7300), new DateTime(2026, 5, 2, 20, 7, 30, 927, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2940), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 928, DateTimeKind.Utc).AddTicks(7050), new DateTime(2026, 5, 2, 20, 7, 30, 928, DateTimeKind.Utc).AddTicks(7050) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 928, DateTimeKind.Utc).AddTicks(7150), new DateTime(2026, 5, 2, 20, 7, 30, 928, DateTimeKind.Utc).AddTicks(7150) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 928, DateTimeKind.Utc).AddTicks(7160), new DateTime(2026, 5, 2, 20, 7, 30, 928, DateTimeKind.Utc).AddTicks(7160) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 912, DateTimeKind.Utc).AddTicks(6820), new DateTime(2026, 5, 2, 20, 7, 30, 912, DateTimeKind.Utc).AddTicks(6820) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e91bdd58-7de1-4777-888d-e3c273c9f37b", new DateTime(2026, 5, 2, 20, 7, 30, 875, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 2, 20, 7, 30, 875, DateTimeKind.Utc).AddTicks(2690), "AQAAAAIAAYagAAAAEMjhZjnF63R0kg6K4oNvIHFADBrLNwji0NbsgL2zqrLaBl5HbmPTN4nQxcUrVCfTaQ==", "11e3b58a-85a5-438d-a2ae-8361c6f8ea6a" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 916, DateTimeKind.Utc).AddTicks(2450), new DateTime(2026, 5, 2, 20, 7, 30, 916, DateTimeKind.Utc).AddTicks(2550), new Guid("b3efeac3-d7b1-417a-9839-759bce7c606f") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(8120), new DateTime(2026, 5, 2, 20, 7, 30, 918, DateTimeKind.Utc).AddTicks(8120) });
        }
    }
}
