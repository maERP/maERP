using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class EmailSettingsRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Idempotent drop — older deployments may already lack the column
            // (it was removed out-of-band on at least one production database
            // before this migration was authored). The model has no IsActive
            // on Tenant either way; either state is fine going forward.
            migrationBuilder.Sql(@"
                IF COL_LENGTH('tenant', 'IsActive') IS NOT NULL
                    ALTER TABLE [tenant] DROP COLUMN [IsActive];");

            migrationBuilder.AlterColumn<bool>(
                name: "RoleManageTenant",
                table: "user_tenant",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "tenant_email_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SmtpHost = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SmtpPort = table.Column<int>(type: "int", nullable: true),
                    SmtpUsername = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SmtpPassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SmtpEnableSsl = table.Column<bool>(type: "bit", nullable: true),
                    M365TenantId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    M365ClientId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    M365ClientSecret = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    M365SenderAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FromAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FromName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReplyToAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReplyToName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2190), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2520), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2540), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2550), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2570), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2570), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2570), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2840), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2900), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2900), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(9030), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "8cffc0be-1a34-4dbf-95ee-22737de75e21");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "73b37784-9720-459f-95ee-bcc335068691");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(5380), new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(5380) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(2990) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3190), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), "Email.ProviderType", "Smtp" });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666624"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), "Email.SmtpHost", "" },
                    { new Guid("66666666-6666-6666-6666-666666666625"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), "Email.SmtpPort", "587" },
                    { new Guid("66666666-6666-6666-6666-666666666626"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), "Email.SmtpUsername", "" },
                    { new Guid("66666666-6666-6666-6666-666666666627"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), "Email.SmtpPassword", "" },
                    { new Guid("66666666-6666-6666-6666-666666666628"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), "Email.SmtpEnableSsl", "true" },
                    { new Guid("66666666-6666-6666-6666-666666666629"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), "Email.M365TenantId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666630"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), "Email.M365ClientId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666631"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), "Email.M365ClientSecret", "" },
                    { new Guid("66666666-6666-6666-6666-666666666632"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), "Email.M365SenderAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666633"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230), "Email.ReplyToAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666634"), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240), "Email.ReplyToName", "" }
                });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(8480), new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(8480) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(8570), new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(8580), new DateTime(2026, 5, 1, 12, 51, 19, 387, DateTimeKind.Utc).AddTicks(8580) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 374, DateTimeKind.Utc).AddTicks(370), new DateTime(2026, 5, 1, 12, 51, 19, 374, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afd4598a-0851-4f14-b67e-91ce0cb10ce4", new DateTime(2026, 5, 1, 12, 51, 19, 342, DateTimeKind.Utc).AddTicks(5600), new DateTime(2026, 5, 1, 12, 51, 19, 342, DateTimeKind.Utc).AddTicks(5600), "AQAAAAIAAYagAAAAEDad3vJS7hbmsSwNNPIgaYvay9LeI86kL1gfchHvCvtgSMtcwCwX9G1FWQ9xDYgyiQ==", "3ae0ba8f-35bd-4897-8eb8-f4b12eed88c7" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 377, DateTimeKind.Utc).AddTicks(400), new DateTime(2026, 5, 1, 12, 51, 19, 377, DateTimeKind.Utc).AddTicks(490), new Guid("1fcb2c71-1eba-4b4a-ad14-c6504ca349c6"), true });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(4210), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(4210) });

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
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tenant",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 10, 21, 7, 41, 5, 408, DateTimeKind.Utc).AddTicks(4330), "Email.ApiKey", "Sendgrid-Key" });

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
                columns: new[] { "DateCreated", "DateModified", "IsActive" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 5, 387, DateTimeKind.Utc).AddTicks(6580), new DateTime(2025, 10, 21, 7, 41, 5, 387, DateTimeKind.Utc).AddTicks(6580), true });

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
    }
}
