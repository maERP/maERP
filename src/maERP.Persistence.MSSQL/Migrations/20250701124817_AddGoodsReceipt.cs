using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddGoodsReceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesChannel_Warehouse_WarehouseId",
                table: "SalesChannel");

            migrationBuilder.DropIndex(
                name: "IX_SalesChannel_WarehouseId",
                table: "SalesChannel");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "SalesChannel");

            migrationBuilder.AddColumn<double>(
                name: "StockMax",
                table: "ProductStock",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StockMin",
                table: "ProductStock",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StorageLocation",
                table: "ProductStock",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceipts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceipts_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesChannelWarehouses",
                columns: table => new
                {
                    SalesChannelsId = table.Column<int>(type: "int", nullable: false),
                    WarehousesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannelWarehouses", x => new { x.SalesChannelsId, x.WarehousesId });
                    table.ForeignKey(
                        name: "FK_SalesChannelWarehouses_SalesChannel_SalesChannelsId",
                        column: x => x.SalesChannelsId,
                        principalTable: "SalesChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesChannelWarehouses_Warehouse_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6f18ca8-2375-4ed5-855e-31c1f8a62ed2", new DateTime(2025, 7, 1, 12, 48, 17, 408, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 7, 1, 12, 48, 17, 408, DateTimeKind.Utc).AddTicks(930), "AQAAAAIAAYagAAAAEOZZBJ7lnYNVcD7052EhJh3aE1k+mLrWLxcxT0bKDcYMSgGi9+To49gxWh5WXaIV7w==", "312209d9-831d-4c12-8f63-089531468fae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1aa318b8-0d74-4182-a492-49b2b92de425", new DateTime(2025, 7, 1, 12, 48, 17, 444, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 7, 1, 12, 48, 17, 444, DateTimeKind.Utc).AddTicks(3990), "AQAAAAIAAYagAAAAEInJknot5havfod8UgIaNvKkRtSOhwcogfJuaf1IcltnJTIDYXSt1mU4EnZv8nm0LQ==", "6a67ee37-91cd-40f9-8eba-cbbf63626682" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4740), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(4750) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(8360), new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 7, 1, 12, 48, 17, 481, DateTimeKind.Utc).AddTicks(8470) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "Name" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(6830), new DateTime(2025, 7, 1, 12, 48, 17, 480, DateTimeKind.Utc).AddTicks(6830), "Hauptlager" });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_DateCreated",
                table: "GoodsReceipts",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_ProductId",
                table: "GoodsReceipts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_ReceiptDate",
                table: "GoodsReceipts",
                column: "ReceiptDate");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_WarehouseId",
                table: "GoodsReceipts",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesChannelWarehouses_WarehousesId",
                table: "SalesChannelWarehouses",
                column: "WarehousesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsReceipts");

            migrationBuilder.DropTable(
                name: "SalesChannelWarehouses");

            migrationBuilder.DropColumn(
                name: "StockMax",
                table: "ProductStock");

            migrationBuilder.DropColumn(
                name: "StockMin",
                table: "ProductStock");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "ProductStock");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "SalesChannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd6f6cde-7802-42a0-abce-d43be6c20bc5", new DateTime(2025, 6, 13, 9, 7, 4, 122, DateTimeKind.Utc).AddTicks(2980), new DateTime(2025, 6, 13, 9, 7, 4, 122, DateTimeKind.Utc).AddTicks(2980), "AQAAAAIAAYagAAAAEP549xjatXxVepUFIXR4G4UQs6S4mmKNSQkRjPB/RoUxx2I8iFmEu5OiLs1rjPTrUg==", "d3917a9e-c6e0-460c-ae60-369e6719994d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1adaed85-7fd6-42c4-93ce-0cc8c2444795", new DateTime(2025, 6, 13, 9, 7, 4, 156, DateTimeKind.Utc).AddTicks(1940), new DateTime(2025, 6, 13, 9, 7, 4, 156, DateTimeKind.Utc).AddTicks(1940), "AQAAAAIAAYagAAAAELbTqTmgzFTcbWOQ/pl2nFRDoTiRHu0igJ7Xqn0/ZdAfmj2GWutRlO9YnvqL98Ws6Q==", "737d0a8d-68e2-4ac2-85bd-340fafc1c799" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8060), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8420), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8440), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8450), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 6, 13, 9, 7, 4, 190, DateTimeKind.Utc).AddTicks(8460) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "WarehouseId" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(2180), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(2180), 1 });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4870) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "Name" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(570), new DateTime(2025, 6, 13, 9, 7, 4, 191, DateTimeKind.Utc).AddTicks(570), "Testlager" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesChannel_WarehouseId",
                table: "SalesChannel",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesChannel_Warehouse_WarehouseId",
                table: "SalesChannel",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
