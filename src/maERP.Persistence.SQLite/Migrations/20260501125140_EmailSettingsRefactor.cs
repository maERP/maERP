using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class EmailSettingsRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tenant");

            migrationBuilder.AlterColumn<bool>(
                name: "RoleManageTenant",
                table: "user_tenant",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "tenant_email_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProviderType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    SmtpHost = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    SmtpPort = table.Column<int>(type: "INTEGER", nullable: true),
                    SmtpUsername = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    SmtpPassword = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SmtpEnableSsl = table.Column<bool>(type: "INTEGER", nullable: true),
                    M365TenantId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    M365ClientId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    M365ClientSecret = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    M365SenderAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    FromAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    FromName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ReplyToAddress = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ReplyToName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_email_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenant_email_settings_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1590), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1930), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1930), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1930), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1990), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1990), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1990), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(1990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2040), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2090) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2100), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2100), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2100), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2100) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2110) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2120) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2130) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2140), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2150), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2160) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2180) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2190), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2200) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2230), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2230), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2230), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2230) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2240) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2250) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2260), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2260) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2260), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(2260) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(8370), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "ec7ac836-241e-4a0b-9419-a4dc33e88cbd");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "f85df178-9b2b-4138-839f-20730803c187");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7550), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7740), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7740), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7740), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), "Email.ProviderType", "Smtp" });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7790), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7790), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7790) });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666624"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), "Email.SmtpHost", "" },
                    { new Guid("66666666-6666-6666-6666-666666666625"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), "Email.SmtpPort", "587" },
                    { new Guid("66666666-6666-6666-6666-666666666626"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), "Email.SmtpUsername", "" },
                    { new Guid("66666666-6666-6666-6666-666666666627"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), "Email.SmtpPassword", "" },
                    { new Guid("66666666-6666-6666-6666-666666666628"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), "Email.SmtpEnableSsl", "true" },
                    { new Guid("66666666-6666-6666-6666-666666666629"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), "Email.M365TenantId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666630"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), "Email.M365ClientId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666631"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), "Email.M365ClientSecret", "" },
                    { new Guid("66666666-6666-6666-6666-666666666632"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), "Email.M365SenderAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666633"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), "Email.ReplyToAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666634"), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), "Email.ReplyToName", "" }
                });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(5470), new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(5560), new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(5560) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(5570), new DateTime(2026, 5, 1, 12, 51, 40, 228, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 215, DateTimeKind.Utc).AddTicks(3900), new DateTime(2026, 5, 1, 12, 51, 40, 215, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9a6d6c7-0c59-4ef4-a090-0634fcd68e8f", new DateTime(2026, 5, 1, 12, 51, 40, 182, DateTimeKind.Utc).AddTicks(390), new DateTime(2026, 5, 1, 12, 51, 40, 182, DateTimeKind.Utc).AddTicks(390), "AQAAAAIAAYagAAAAEDe8mHT+FixYBnGZNKGx8aSIUtoJbz21ySumb0nJ+URdd9o32A/2lv6Sk3D/hDQyNg==", "ec3284fc-f20e-4cc5-9444-d6fee0ce54f4" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 218, DateTimeKind.Utc).AddTicks(230), new DateTime(2026, 5, 1, 12, 51, 40, 218, DateTimeKind.Utc).AddTicks(360), new Guid("f60a330d-885c-4cbd-9878-d50bfd2398bd"), true });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(3570), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(3570) });

            migrationBuilder.CreateIndex(
                name: "IX_tenant_email_settings_TenantId",
                table: "tenant_email_settings",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tenant_email_settings");

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"));

            migrationBuilder.DeleteData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"));

            migrationBuilder.AlterColumn<bool>(
                name: "RoleManageTenant",
                table: "user_tenant",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tenant",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

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
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: null);

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
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040), new DateTime(2025, 10, 21, 7, 41, 12, 128, DateTimeKind.Utc).AddTicks(4040), "Email.ApiKey", "Sendgrid-Key" });

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
                columns: new[] { "DateCreated", "DateModified", "IsActive" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 12, 109, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 10, 21, 7, 41, 12, 109, DateTimeKind.Utc).AddTicks(5420), true });

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
    }
}
