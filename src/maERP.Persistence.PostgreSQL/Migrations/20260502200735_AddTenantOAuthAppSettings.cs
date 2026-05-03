using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
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
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "saleschannel",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "saleschannel",
                type: "character varying(8192)",
                maxLength: 8192,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalConfigJson",
                table: "saleschannel",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "saleschannel",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncStartedAt",
                table: "saleschannel",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketplaceId",
                table: "saleschannel",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "saleschannel",
                type: "character varying(8192)",
                maxLength: 8192,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncIntervalSeconds",
                table: "saleschannel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiresAt",
                table: "saleschannel",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RepricingType",
                table: "product_saleschannel",
                type: "integer",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "RemoteProductId",
                table: "product_saleschannel",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "product_saleschannel",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalListingId",
                table: "product_saleschannel",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FulfillmentChannel",
                table: "product_saleschannel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsListed",
                table: "product_saleschannel",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastErrorMessage",
                table: "product_saleschannel",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastExportHash",
                table: "product_saleschannel",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "product_saleschannel",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "product_saleschannel",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetadataJson",
                table: "product_saleschannel",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "product_saleschannel",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockBuffer",
                table: "product_saleschannel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SyncStatus",
                table: "product_saleschannel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gtin",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mpn",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "channel_export_outbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    AggregateType = table.Column<int>(type: "integer", nullable: false),
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    PayloadJson = table.Column<string>(type: "text", nullable: false),
                    IdempotencyKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    NextAttemptAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LastError = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Operation = table.Column<int>(type: "integer", nullable: false),
                    TriggerSource = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ItemsProcessed = table.Column<int>(type: "integer", nullable: false),
                    ItemsFailed = table.Column<int>(type: "integer", nullable: false),
                    ErrorSummary = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<int>(type: "integer", nullable: false),
                    StateToken = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Nonce = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oauth_state", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant_oauth_app_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ClientId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ClientSecret = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    RedirectUri = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RuName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Scopes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    UseSandbox = table.Column<bool>(type: "boolean", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                columns: new[] { "AccessToken", "AdditionalConfigJson", "DateCreated", "DateModified", "IsEnabled", "LastSyncStartedAt", "MarketplaceId", "RefreshToken", "SyncIntervalSeconds", "TokenExpiresAt" },
                values: new object[] { null, null, new DateTime(2026, 5, 2, 20, 7, 34, 632, DateTimeKind.Utc).AddTicks(8690), new DateTime(2026, 5, 2, 20, 7, 34, 632, DateTimeKind.Utc).AddTicks(8690), true, null, null, null, 60, null });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1170), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1170), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1370), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1380), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1390), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1400), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), new DateTime(2026, 5, 2, 20, 7, 34, 641, DateTimeKind.Utc).AddTicks(1410), false });

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

            migrationBuilder.CreateIndex(
                name: "IX_product_saleschannel_TenantId_ProductId_SalesChannelId",
                table: "product_saleschannel",
                columns: new[] { "TenantId", "ProductId", "SalesChannelId" },
                unique: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "saleschannel",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4096)",
                oldMaxLength: 4096);

            migrationBuilder.AlterColumn<bool>(
                name: "RepricingType",
                table: "product_saleschannel",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "RemoteProductId",
                table: "product_saleschannel",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldNullable: true);

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
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680) });

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

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9690) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9700), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9700), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9700), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9710) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9720), new DateTime(2026, 5, 1, 12, 51, 37, 164, DateTimeKind.Utc).AddTicks(9720) });

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
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 150, DateTimeKind.Utc).AddTicks(5490), new DateTime(2026, 5, 1, 12, 51, 37, 150, DateTimeKind.Utc).AddTicks(5570), new Guid("5ea3466f-60ec-4f91-8f9b-3056f9cc7388") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(8930), new DateTime(2026, 5, 1, 12, 51, 37, 152, DateTimeKind.Utc).AddTicks(8930) });
        }
    }
}
