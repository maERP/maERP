using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.PostgreSQL.Migrations
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
            migrationBuilder.Sql(@"ALTER TABLE tenant DROP COLUMN IF EXISTS ""IsActive"";");

            migrationBuilder.AlterColumn<bool>(
                name: "RoleManageTenant",
                table: "user_tenant",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            // Idempotent — see DROP COLUMN IF EXISTS comment above. A previous
            // partially-applied run of this migration may have created the
            // table without recording the migration in __EFMigrationsHistory.
            // The table is brand-new in this migration so dropping it here
            // cannot lose user data.
            migrationBuilder.Sql(@"DROP TABLE IF EXISTS tenant_email_settings CASCADE;");

            migrationBuilder.CreateTable(
                name: "tenant_email_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderType = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SmtpHost = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SmtpPort = table.Column<int>(type: "integer", nullable: true),
                    SmtpUsername = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SmtpPassword = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SmtpEnableSsl = table.Column<bool>(type: "boolean", nullable: true),
                    M365TenantId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    M365ClientId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    M365ClientSecret = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    M365SenderAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FromAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FromName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ReplyToAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ReplyToName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7010), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7380), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7380), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7380), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7400), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7400), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7400), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7410) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7430), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7430), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7430), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7450), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7450), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7450), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7450) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7470), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7470), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7470), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7480) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7490) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7500), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7510) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7520), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7520) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7530) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7540), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7540), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7540), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7540) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7560), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7560), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7560), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7580), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7600), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7600), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7620), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7640), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7670), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7670), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7670), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7690), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7690), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7690), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7720), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7720), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7720), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 153, DateTimeKind.Utc).AddTicks(3610), new DateTime(2026, 5, 1, 12, 51, 37, 153, DateTimeKind.Utc).AddTicks(3610) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "3e189ac6-30be-434e-8041-021aaf9cb00e");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "8761d66d-0883-4ff2-9c60-d5dc47055a27");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(6190), new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9480), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9480) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9670), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9670), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680), "Email.ProviderType", "Smtp" });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9720), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9720) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9720), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9720) });

            // Idempotent — see comments above. A previous partially-applied
            // run may have inserted some of these seed rows already.
            migrationBuilder.Sql(@"
                INSERT INTO setting (""Id"", ""DateCreated"", ""DateModified"", ""Key"", ""Value"") VALUES
                    ('66666666-6666-6666-6666-666666666624', TIMESTAMPTZ '2026-05-01T12:51:37.164968Z', TIMESTAMPTZ '2026-05-01T12:51:37.164968Z', 'Email.SmtpHost', ''),
                    ('66666666-6666-6666-6666-666666666625', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', 'Email.SmtpPort', '587'),
                    ('66666666-6666-6666-6666-666666666626', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', 'Email.SmtpUsername', ''),
                    ('66666666-6666-6666-6666-666666666627', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', 'Email.SmtpPassword', ''),
                    ('66666666-6666-6666-6666-666666666628', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', TIMESTAMPTZ '2026-05-01T12:51:37.164969Z', 'Email.SmtpEnableSsl', 'true'),
                    ('66666666-6666-6666-6666-666666666629', TIMESTAMPTZ '2026-05-01T12:51:37.164970Z', TIMESTAMPTZ '2026-05-01T12:51:37.164970Z', 'Email.M365TenantId', ''),
                    ('66666666-6666-6666-6666-666666666630', TIMESTAMPTZ '2026-05-01T12:51:37.164970Z', TIMESTAMPTZ '2026-05-01T12:51:37.164970Z', 'Email.M365ClientId', ''),
                    ('66666666-6666-6666-6666-666666666631', TIMESTAMPTZ '2026-05-01T12:51:37.164970Z', TIMESTAMPTZ '2026-05-01T12:51:37.164970Z', 'Email.M365ClientSecret', ''),
                    ('66666666-6666-6666-6666-666666666632', TIMESTAMPTZ '2026-05-01T12:51:37.164971Z', TIMESTAMPTZ '2026-05-01T12:51:37.164971Z', 'Email.M365SenderAddress', ''),
                    ('66666666-6666-6666-6666-666666666633', TIMESTAMPTZ '2026-05-01T12:51:37.164971Z', TIMESTAMPTZ '2026-05-01T12:51:37.164971Z', 'Email.ReplyToAddress', ''),
                    ('66666666-6666-6666-6666-666666666634', TIMESTAMPTZ '2026-05-01T12:51:37.164972Z', TIMESTAMPTZ '2026-05-01T12:51:37.164972Z', 'Email.ReplyToName', '')
                ON CONFLICT (""Id"") DO NOTHING;");

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(9140), new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(9140) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(9230), new DateTime(2026, 5, 1, 12, 51, 37, 160, DateTimeKind.Utc).AddTicks(9230) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 148, DateTimeKind.Utc).AddTicks(1320), new DateTime(2026, 5, 1, 12, 51, 37, 148, DateTimeKind.Utc).AddTicks(1320) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e4783b9-df28-4c5a-8eb5-442a53066c72", new DateTime(2026, 5, 1, 12, 51, 37, 116, DateTimeKind.Utc).AddTicks(440), new DateTime(2026, 5, 1, 12, 51, 37, 116, DateTimeKind.Utc).AddTicks(440), "AQAAAAIAAYagAAAAEL2UJSPYDY2xdRWBXfmLfTFrz0Z3kcVe5nqL/MYsTsq/z+eehLAc4iEllVeHYQ39ig==", "6775acfc-41b0-4d19-9c30-28d7d97da2a8" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 150, DateTimeKind.Utc).AddTicks(5490), new DateTime(2026, 5, 1, 12, 51, 37, 150, DateTimeKind.Utc).AddTicks(5570), new Guid("5ea3466f-60ec-4f91-8f9b-3056f9cc7388"), true });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(8930), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(8930) });

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
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tenant",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 10, 21, 7, 41, 8, 892, DateTimeKind.Utc).AddTicks(6440), "Email.ApiKey", "Sendgrid-Key" });

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
                columns: new[] { "DateCreated", "DateModified", "IsActive" },
                values: new object[] { new DateTime(2025, 10, 21, 7, 41, 8, 872, DateTimeKind.Utc).AddTicks(590), new DateTime(2025, 10, 21, 7, 41, 8, 872, DateTimeKind.Utc).AddTicks(590), true });

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
    }
}
