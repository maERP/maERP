using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.SQLite.Migrations
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
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StockMin",
                table: "ProductStock",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StorageLocation",
                table: "ProductStock",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    WarehouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Supplier = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                    SalesChannelsId = table.Column<int>(type: "INTEGER", nullable: false),
                    WarehousesId = table.Column<int>(type: "INTEGER", nullable: false)
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
                values: new object[] { "59891598-b5f2-494d-8578-8759ae2bcb45", new DateTime(2025, 7, 1, 12, 48, 23, 884, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 7, 1, 12, 48, 23, 884, DateTimeKind.Utc).AddTicks(2450), "AQAAAAIAAYagAAAAEKbB3FvmuR189r6Gtit7tjXGVYe4K7gjyFMFV4EyfPontZYba5WWm+0BKjATXWq11w==", "0ac7a7a7-cdad-448d-98d4-bbf47b24881f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "823bb6c3-e4e1-4c53-b1dd-07b3d54edee5", new DateTime(2025, 7, 1, 12, 48, 23, 916, DateTimeKind.Utc).AddTicks(4090), new DateTime(2025, 7, 1, 12, 48, 23, 916, DateTimeKind.Utc).AddTicks(4090), "AQAAAAIAAYagAAAAEA+hl0Md/xu0yvKb7FjWO1norr0iQ9g5bDsSxOwZ04iSQjw3yPy0CD6BGK6zMXmTtg==", "a191f8bc-6129-4145-8b00-9e48fc3d5fb2" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(2810), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3110), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3120), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3140), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3160), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3170), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(3170) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(4590), new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(4590) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 7, 1, 12, 48, 23, 950, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "Name" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 7, 1, 12, 48, 23, 949, DateTimeKind.Utc).AddTicks(5280), "Hauptlager" });

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
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80790637-4538-49df-a1ac-fcb57b91ca28", new DateTime(2025, 6, 14, 8, 11, 18, 50, DateTimeKind.Utc).AddTicks(9890), new DateTime(2025, 6, 14, 8, 11, 18, 50, DateTimeKind.Utc).AddTicks(9920), "AQAAAAIAAYagAAAAENNfsPcFirW6DZxn5/brXZJjJeRrzzT1+MZt1d56DlAqP9E2+NhOO31go4bEhkTDjA==", "fe396ce4-4073-4d71-94c1-2be712d739ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a49d18f-c059-4253-97cd-238e58872165", new DateTime(2025, 6, 14, 8, 11, 18, 88, DateTimeKind.Utc).AddTicks(1090), new DateTime(2025, 6, 14, 8, 11, 18, 88, DateTimeKind.Utc).AddTicks(1090), "AQAAAAIAAYagAAAAELmoG/VdFiww/GgtHR7yhFf3cGIBd9M57zIilgEonI6adE+uDw25/X+K3DZLiZCU3g==", "26717a0b-dc6d-41aa-bfb1-d091219f35f4" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(780), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1130), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1160) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1240), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(1250) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "WarehouseId" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(5410), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(5410), 1 });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(8150), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(8150) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(8250), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(8250) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(8250), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(8250) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "Name" },
                values: new object[] { new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(3650), new DateTime(2025, 6, 14, 8, 11, 18, 124, DateTimeKind.Utc).AddTicks(3650), "Testlager" });

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
