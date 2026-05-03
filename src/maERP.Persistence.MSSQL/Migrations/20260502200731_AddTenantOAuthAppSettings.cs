using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantOAuthAppSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEncrypted",
                table: "setting",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "saleschannel",
                type: "nvarchar(max)",
                maxLength: 8192,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalConfigJson",
                table: "saleschannel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "saleschannel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncStartedAt",
                table: "saleschannel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketplaceId",
                table: "saleschannel",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "saleschannel",
                type: "nvarchar(max)",
                maxLength: 8192,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncIntervalSeconds",
                table: "saleschannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiresAt",
                table: "saleschannel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RepricingType",
                table: "product_saleschannel",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "RemoteProductId",
                table: "product_saleschannel",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "product_saleschannel",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalListingId",
                table: "product_saleschannel",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FulfillmentChannel",
                table: "product_saleschannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsListed",
                table: "product_saleschannel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastErrorMessage",
                table: "product_saleschannel",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastExportHash",
                table: "product_saleschannel",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "product_saleschannel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "product_saleschannel",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetadataJson",
                table: "product_saleschannel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "product_saleschannel",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockBuffer",
                table: "product_saleschannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SyncStatus",
                table: "product_saleschannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gtin",
                table: "product",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mpn",
                table: "product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "channel_export_outbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    AggregateType = table.Column<int>(type: "int", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdempotencyKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    NextAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel_export_outbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_channel_export_outbox_saleschannel_SalesChannelId",
                        column: x => x.SalesChannelId,
                        principalTable: "saleschannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "channel_sync_run",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    TriggerSource = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ItemsProcessed = table.Column<int>(type: "int", nullable: false),
                    ItemsFailed = table.Column<int>(type: "int", nullable: false),
                    ErrorSummary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel_sync_run", x => x.Id);
                    table.ForeignKey(
                        name: "FK_channel_sync_run_saleschannel_SalesChannelId",
                        column: x => x.SalesChannelId,
                        principalTable: "saleschannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oauth_state",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<int>(type: "int", nullable: false),
                    StateToken = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Nonce = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oauth_state", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant_oauth_app_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ClientSecret = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    RedirectUri = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RuName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Scopes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UseSandbox = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant_oauth_app_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tenant_oauth_app_settings_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "AccessToken", "AdditionalConfigJson", "DateCreated", "DateModified", "IsEnabled", "LastSyncStartedAt", "MarketplaceId", "RefreshToken", "SyncIntervalSeconds", "TokenExpiresAt" },
                values: new object[] { null, null, new DateTime(2026, 5, 2, 20, 7, 30, 927, DateTimeKind.Utc).AddTicks(7300), new DateTime(2026, 5, 2, 20, 7, 30, 927, DateTimeKind.Utc).AddTicks(7300), true, null, null, null, 60, null });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2720), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2940), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2950), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(3000), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2960), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2970), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2980), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), new DateTime(2026, 5, 2, 20, 7, 30, 936, DateTimeKind.Utc).AddTicks(2990), false });

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

            migrationBuilder.CreateIndex(
                name: "IX_product_saleschannel_TenantId_ProductId_SalesChannelId",
                table: "product_saleschannel",
                columns: new[] { "TenantId", "ProductId", "SalesChannelId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_product_Gtin",
                table: "product",
                column: "Gtin");

            migrationBuilder.CreateIndex(
                name: "IX_channel_export_outbox_SalesChannelId_IdempotencyKey",
                table: "channel_export_outbox",
                columns: new[] { "SalesChannelId", "IdempotencyKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_channel_export_outbox_Status_NextAttemptAt",
                table: "channel_export_outbox",
                columns: new[] { "Status", "NextAttemptAt" });

            migrationBuilder.CreateIndex(
                name: "IX_channel_sync_run_CorrelationId",
                table: "channel_sync_run",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_channel_sync_run_SalesChannelId_StartedAt",
                table: "channel_sync_run",
                columns: new[] { "SalesChannelId", "StartedAt" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_oauth_state_ExpiresAt",
                table: "oauth_state",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_oauth_state_StateToken",
                table: "oauth_state",
                column: "StateToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tenant_oauth_app_settings_TenantId_Provider",
                table: "tenant_oauth_app_settings",
                columns: new[] { "TenantId", "Provider" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channel_export_outbox");

            migrationBuilder.DropTable(
                name: "channel_sync_run");

            migrationBuilder.DropTable(
                name: "oauth_state");

            migrationBuilder.DropTable(
                name: "tenant_oauth_app_settings");

            migrationBuilder.DropIndex(
                name: "IX_product_saleschannel_TenantId_ProductId_SalesChannelId",
                table: "product_saleschannel");

            migrationBuilder.DropIndex(
                name: "IX_product_Gtin",
                table: "product");

            migrationBuilder.DropColumn(
                name: "IsEncrypted",
                table: "setting");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "AdditionalConfigJson",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "LastSyncStartedAt",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "MarketplaceId",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "SyncIntervalSeconds",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "TokenExpiresAt",
                table: "saleschannel");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "ExternalListingId",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "FulfillmentChannel",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "IsListed",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "LastErrorMessage",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "LastExportHash",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "MetadataJson",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "StockBuffer",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "SyncStatus",
                table: "product_saleschannel");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Gtin",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Mpn",
                table: "product");

            migrationBuilder.AlterColumn<bool>(
                name: "RepricingType",
                table: "product_saleschannel",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RemoteProductId",
                table: "product_saleschannel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

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
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200) });

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

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240), new DateTime(2026, 5, 1, 12, 51, 19, 392, DateTimeKind.Utc).AddTicks(3240) });

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
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 377, DateTimeKind.Utc).AddTicks(400), new DateTime(2026, 5, 1, 12, 51, 19, 377, DateTimeKind.Utc).AddTicks(490), new Guid("1fcb2c71-1eba-4b4a-ad14-c6504ca349c6") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(4210), new DateTime(2026, 5, 1, 12, 51, 19, 379, DateTimeKind.Utc).AddTicks(4210) });
        }
    }
}
