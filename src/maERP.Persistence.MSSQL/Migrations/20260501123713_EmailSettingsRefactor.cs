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
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tenant");

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
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5170), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5570), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5580), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5580), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5580), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5590), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5590), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5590), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5600), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5600), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5600), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5620), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5620), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5620), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5630), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5630), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5630), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5650), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5650), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5650), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5660), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5680), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5680), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5680), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5700), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5700), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5700), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5710), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5710), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5710), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5730), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5730), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5730), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5740), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5740), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5740), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5760), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5760), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5760), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5770), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5770), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5770), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5790), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5800), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5800), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5800), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5820), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5820), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5820), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5840), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5840), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5840), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5850), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5850), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5850), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5870), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5870), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5870), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5880), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5880), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5880), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5900), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5900), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5900), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5910), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5910), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5910), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5930), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5930), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5930), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5940), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 508, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 1, 12, 36, 58, 508, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "451364c0-f33a-4776-a948-3076d84629db");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "8f27c560-a474-4947-9c28-34591c04ad5f");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 516, DateTimeKind.Utc).AddTicks(8610), new DateTime(2026, 5, 1, 12, 36, 58, 516, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(420), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(630) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "Key", "Value" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(640), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(640), "Email.ProviderType", "Smtp" });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(670), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(670) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(680), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(680), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(680) });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666624"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(640), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(640), "Email.SmtpHost", "" },
                    { new Guid("66666666-6666-6666-6666-666666666625"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(640), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(640), "Email.SmtpPort", "587" },
                    { new Guid("66666666-6666-6666-6666-666666666626"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), "Email.SmtpUsername", "" },
                    { new Guid("66666666-6666-6666-6666-666666666627"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), "Email.SmtpPassword", "" },
                    { new Guid("66666666-6666-6666-6666-666666666628"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), "Email.SmtpEnableSsl", "true" },
                    { new Guid("66666666-6666-6666-6666-666666666629"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(650), "Email.M365TenantId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666630"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), "Email.M365ClientId", "" },
                    { new Guid("66666666-6666-6666-6666-666666666631"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), "Email.M365ClientSecret", "" },
                    { new Guid("66666666-6666-6666-6666-666666666632"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(660), "Email.M365SenderAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666633"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(670), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(670), "Email.ReplyToAddress", "" },
                    { new Guid("66666666-6666-6666-6666-666666666634"), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(670), new DateTime(2026, 5, 1, 12, 36, 58, 522, DateTimeKind.Utc).AddTicks(670), "Email.ReplyToName", "" }
                });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 517, DateTimeKind.Utc).AddTicks(2340), new DateTime(2026, 5, 1, 12, 36, 58, 517, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 517, DateTimeKind.Utc).AddTicks(2440), new DateTime(2026, 5, 1, 12, 36, 58, 517, DateTimeKind.Utc).AddTicks(2440) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 517, DateTimeKind.Utc).AddTicks(2450), new DateTime(2026, 5, 1, 12, 36, 58, 517, DateTimeKind.Utc).AddTicks(2450) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 501, DateTimeKind.Utc).AddTicks(5510), new DateTime(2026, 5, 1, 12, 36, 58, 501, DateTimeKind.Utc).AddTicks(5510) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d37993f-f318-40b6-81dc-b5f3c0ef28b4", new DateTime(2026, 5, 1, 12, 36, 58, 464, DateTimeKind.Utc).AddTicks(8060), new DateTime(2026, 5, 1, 12, 36, 58, 464, DateTimeKind.Utc).AddTicks(8060), "AQAAAAIAAYagAAAAEI3TE8PJF3zNqWmcfYcLfhO3V7MwZnVpzvxFKMdE9UZkHVGvOSi3BdDvp57iLS3qUQ==", "a46afec6-e975-471f-bb3c-7588c654eaa6" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id", "RoleManageTenant" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 505, DateTimeKind.Utc).AddTicks(1950), new DateTime(2026, 5, 1, 12, 36, 58, 505, DateTimeKind.Utc).AddTicks(2050), new Guid("98796805-4491-474d-87eb-3583b7646d54"), true });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(7590), new DateTime(2026, 5, 1, 12, 36, 58, 507, DateTimeKind.Utc).AddTicks(7590) });

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
