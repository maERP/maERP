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
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tenant");

            migrationBuilder.AlterColumn<bool>(
                name: "RoleManageTenant",
                table: "user_tenant",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

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
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3350), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3350) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3720), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3730), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3730), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3730), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3750), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3750), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3750), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3760), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3760), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3760), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3770), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3770), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3790), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3790), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3790), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3810), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3810), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3810), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3830), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3830), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3830), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3850), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3850), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3850), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3870), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3870), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3870), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3890), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3890), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3890), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3910), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3910), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3910), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3930), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3930), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3930), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3950), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3950), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3950), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3950) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3970), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3970), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3970), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3990), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3990), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3990), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(3990) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4010), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4010), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4010), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4030), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4030), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4030), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4050), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4050), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4060), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4060), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4060), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4070), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4070), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4070), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4070) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(4080) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 585, DateTimeKind.Utc).AddTicks(280), new DateTime(2026, 5, 1, 12, 37, 16, 585, DateTimeKind.Utc).AddTicks(280) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "6aaaa2d3-339c-48ad-9148-e7fca7b8e5e3");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "cbc40760-bdd0-451c-a7d0-4bea76d4c61c");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(4960), new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(660), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(850) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(860), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(860), "Email.ProviderType", "Smtp" });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(890), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(890) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(900), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(900), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666624"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(860), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(860), "Email.SmtpHost", "" },
                    { new Guid("66666666-6666-6666-6666-666666666625"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(860), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(860), "Email.SmtpPort", "587" },
                    { new Guid("66666666-6666-6666-6666-666666666626"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), "Email.SmtpUsername", "" },
                    { new Guid("66666666-6666-6666-6666-666666666627"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), "Email.SmtpPassword", "" },
                    { new Guid("66666666-6666-6666-6666-666666666628"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), "Email.SmtpEnableSsl", "true" },
                    { new Guid("66666666-6666-6666-6666-666666666629"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(870), "Email.M365TenantId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666630"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), "Email.M365ClientId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666631"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), "Email.M365ClientSecret", "" },
                    { new Guid("66666666-6666-6666-6666-666666666632"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(880), "Email.M365SenderAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666633"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(890), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(890), "Email.ReplyToAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666634"), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(890), new DateTime(2026, 5, 1, 12, 37, 16, 597, DateTimeKind.Utc).AddTicks(890), "Email.ReplyToName", "" }
                });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(7950), new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(8040), new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(8040) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(8040), new DateTime(2026, 5, 1, 12, 37, 16, 592, DateTimeKind.Utc).AddTicks(8040) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 579, DateTimeKind.Utc).AddTicks(6800), new DateTime(2026, 5, 1, 12, 37, 16, 579, DateTimeKind.Utc).AddTicks(6800) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea69d786-297a-466a-b2fc-d6cc4f5ec6a2", new DateTime(2026, 5, 1, 12, 37, 16, 544, DateTimeKind.Utc).AddTicks(8420), new DateTime(2026, 5, 1, 12, 37, 16, 544, DateTimeKind.Utc).AddTicks(8420), "AQAAAAIAAYagAAAAEBH2H8LllxKHvrFeyu9x4WcmPL/Ul+CuyHAC5wzc6bhqRRv3ZE4VXCGU31QpfMGGbg==", "a088fbaf-30f4-4dea-8aac-ba787d16e987" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 582, DateTimeKind.Utc).AddTicks(1630), new DateTime(2026, 5, 1, 12, 37, 16, 582, DateTimeKind.Utc).AddTicks(1710), new Guid("9844f333-2c8a-4944-ba04-4010e717bc80"), true });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(5520), new DateTime(2026, 5, 1, 12, 37, 16, 584, DateTimeKind.Utc).AddTicks(5520) });

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
