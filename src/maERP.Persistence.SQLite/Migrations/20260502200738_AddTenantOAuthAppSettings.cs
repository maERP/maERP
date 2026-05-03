using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.SQLite.Migrations
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
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "saleschannel",
                type: "TEXT",
                maxLength: 8192,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalConfigJson",
                table: "saleschannel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "saleschannel",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncStartedAt",
                table: "saleschannel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketplaceId",
                table: "saleschannel",
                type: "TEXT",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "saleschannel",
                type: "TEXT",
                maxLength: 8192,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncIntervalSeconds",
                table: "saleschannel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiresAt",
                table: "saleschannel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RemoteProductId",
                table: "product_saleschannel",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "product_saleschannel",
                type: "TEXT",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalListingId",
                table: "product_saleschannel",
                type: "TEXT",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FulfillmentChannel",
                table: "product_saleschannel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsListed",
                table: "product_saleschannel",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastErrorMessage",
                table: "product_saleschannel",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastExportHash",
                table: "product_saleschannel",
                type: "TEXT",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "product_saleschannel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "product_saleschannel",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetadataJson",
                table: "product_saleschannel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinPrice",
                table: "product_saleschannel",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockBuffer",
                table: "product_saleschannel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SyncStatus",
                table: "product_saleschannel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "product",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gtin",
                table: "product",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mpn",
                table: "product",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "channel_export_outbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Operation = table.Column<int>(type: "INTEGER", nullable: false),
                    AggregateType = table.Column<int>(type: "INTEGER", nullable: false),
                    AggregateId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PayloadJson = table.Column<string>(type: "TEXT", nullable: false),
                    IdempotencyKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    AttemptCount = table.Column<int>(type: "INTEGER", nullable: false),
                    NextAttemptAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    LastError = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Operation = table.Column<int>(type: "INTEGER", nullable: false),
                    TriggerSource = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ItemsProcessed = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemsFailed = table.Column<int>(type: "INTEGER", nullable: false),
                    ErrorSummary = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    CorrelationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true)
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Provider = table.Column<int>(type: "INTEGER", nullable: false),
                    StateToken = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Nonce = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ConsumedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oauth_state", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant_oauth_app_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Provider = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ClientSecret = table.Column<string>(type: "TEXT", maxLength: 4096, nullable: true),
                    RedirectUri = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RuName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Scopes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    UseSandbox = table.Column<bool>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2190), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2190) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2580), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2580), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2580) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2590), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2610), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2610) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2620), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2620) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2630) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2650), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2650) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2670), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2690), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2700), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2720), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2740), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2740) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2760), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2780), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2780), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2780), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2790), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2790), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2790), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2800) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2810), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2810), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2810), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2830), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2830) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2840), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2840), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2840), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2840) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2850), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2850) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2860) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2890), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2890) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2900), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2910) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2920), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2920), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2920), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2920) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000114"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000115"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000116"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2930) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000117"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2940), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000118"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2940), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000119"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2940), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2940) });

            migrationBuilder.UpdateData(
                table: "country",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000120"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2950), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.UpdateData(
                table: "manufacturer",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(9430), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "abc43a7e-f7bb-4447-baaf-1add431ddbdf",
                column: "ConcurrencyStamp",
                value: "8c9bc36b-b135-4d47-862a-b050363d47b1");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "6238d92f-e293-4d7f-92a3-b33c3df09d77");

            migrationBuilder.UpdateData(
                table: "saleschannel",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "AccessToken", "AdditionalConfigJson", "DateCreated", "DateModified", "IsEnabled", "LastSyncStartedAt", "MarketplaceId", "RefreshToken", "SyncIntervalSeconds", "TokenExpiresAt" },
                values: new object[] { null, null, new DateTime(2026, 5, 2, 20, 7, 38, 85, DateTimeKind.Utc).AddTicks(2870), new DateTime(2026, 5, 2, 20, 7, 38, 85, DateTimeKind.Utc).AddTicks(2870), true, null, null, null, 60, null });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666614"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9270), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9270), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666615"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9480), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9480), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666616"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9480), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9480), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666617"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666618"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666619"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666620"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666621"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666622"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9530), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9530), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666623"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9530), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9530), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9490), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9500), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9510), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9520), false });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified", "IsEncrypted" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9530), new DateTime(2026, 5, 2, 20, 7, 38, 92, DateTimeKind.Utc).AddTicks(9530), false });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777771"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 86, DateTimeKind.Utc).AddTicks(2530), new DateTime(2026, 5, 2, 20, 7, 38, 86, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777772"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 86, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 2, 20, 7, 38, 86, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "tax_class",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777773"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 86, DateTimeKind.Utc).AddTicks(2640), new DateTime(2026, 5, 2, 20, 7, 38, 86, DateTimeKind.Utc).AddTicks(2640) });

            migrationBuilder.UpdateData(
                table: "tenant",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 71, DateTimeKind.Utc).AddTicks(300), new DateTime(2026, 5, 2, 20, 7, 38, 71, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d36bf3d-36db-4e1c-84b4-a6a3a9b14c22", new DateTime(2026, 5, 2, 20, 7, 38, 36, DateTimeKind.Utc).AddTicks(4280), new DateTime(2026, 5, 2, 20, 7, 38, 36, DateTimeKind.Utc).AddTicks(4280), "AQAAAAIAAYagAAAAEOqcf/HYGDv1tbvaHfevnO/AOHHui4ulquZuVeyeaAlEy4xGuY5zHEChf0X6TzvG4w==", "85494942-610b-4569-9149-6471c0d3b07a" });

            migrationBuilder.UpdateData(
                table: "user_tenant",
                keyColumns: new[] { "TenantId", "UserId" },
                keyValues: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 73, DateTimeKind.Utc).AddTicks(8600), new DateTime(2026, 5, 2, 20, 7, 38, 73, DateTimeKind.Utc).AddTicks(8700), new Guid("af283b58-91c4-49e1-9c10-865a3585ea13") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(4440), new DateTime(2026, 5, 2, 20, 7, 38, 76, DateTimeKind.Utc).AddTicks(4440) });

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

            migrationBuilder.AlterColumn<Guid>(
                name: "RemoteProductId",
                table: "product_saleschannel",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

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
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750) });

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

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666624"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666625"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666626"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666627"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666628"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666629"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666630"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666631"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666632"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666633"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "setting",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666634"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780), new DateTime(2026, 5, 1, 12, 51, 40, 232, DateTimeKind.Utc).AddTicks(7780) });

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
                columns: new[] { "DateCreated", "DateModified", "Id" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 218, DateTimeKind.Utc).AddTicks(230), new DateTime(2026, 5, 1, 12, 51, 40, 218, DateTimeKind.Utc).AddTicks(360), new Guid("f60a330d-885c-4cbd-9878-d50bfd2398bd") });

            migrationBuilder.UpdateData(
                table: "warehouse",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(3570), new DateTime(2026, 5, 1, 12, 51, 40, 220, DateTimeKind.Utc).AddTicks(3570) });
        }
    }
}
