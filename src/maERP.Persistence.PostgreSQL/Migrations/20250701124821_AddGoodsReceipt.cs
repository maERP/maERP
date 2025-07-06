using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
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
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StockMin",
                table: "ProductStock",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StorageLocation",
                table: "ProductStock",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    Supplier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    SalesChannelsId = table.Column<int>(type: "integer", nullable: false),
                    WarehousesId = table.Column<int>(type: "integer", nullable: false)
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
                values: new object[] { "531dd48e-9ea4-4eed-9a44-7d54f238de1e", new DateTime(2025, 7, 1, 12, 48, 20, 716, DateTimeKind.Utc).AddTicks(4660), new DateTime(2025, 7, 1, 12, 48, 20, 716, DateTimeKind.Utc).AddTicks(4660), "AQAAAAIAAYagAAAAEMiojw4pqmyG44aXbMxxqf/1BJOQELwA/z1Q/ahsFRjmg1iVJnJLVhBrsSW14lwuqg==", "89f329d3-1674-4728-b1c4-2bb2ad219123" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc1695e4-5d9b-4a39-afd5-67a36c0819bd", new DateTime(2025, 7, 1, 12, 48, 20, 751, DateTimeKind.Utc).AddTicks(1570), new DateTime(2025, 7, 1, 12, 48, 20, 751, DateTimeKind.Utc).AddTicks(1570), "AQAAAAIAAYagAAAAEHG+Phuo+chcUPcwvlvyeOPHIxzVBv+Nw53iZg38U3zqSnw2OQ2iKzZklly5ylsKaA==", "8a1e1997-0ce1-4cef-a918-bc82aa9ac4e6" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5110), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5420) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(7220), new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(7220) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 7, 1, 12, 48, 20, 784, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "Name" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(8030), new DateTime(2025, 7, 1, 12, 48, 20, 783, DateTimeKind.Utc).AddTicks(8030), "Hauptlager" });

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
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "688caab3-bf05-4035-a2de-6d785851578d", new DateTime(2025, 6, 13, 9, 7, 7, 287, DateTimeKind.Utc).AddTicks(1870), new DateTime(2025, 6, 13, 9, 7, 7, 287, DateTimeKind.Utc).AddTicks(1870), "AQAAAAIAAYagAAAAEIRMDpX/YZU0GLHZoE+VZCkbOk+RD7fKkfp7U1Dk9xJ5fyKBgiq/BVSvzqEE5Bouxg==", "75a2bddd-676d-40e1-85b1-6a04451df333" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a67eb714-d1b9-445a-8334-5518d9784a9b", new DateTime(2025, 6, 13, 9, 7, 7, 319, DateTimeKind.Utc).AddTicks(940), new DateTime(2025, 6, 13, 9, 7, 7, 319, DateTimeKind.Utc).AddTicks(940), "AQAAAAIAAYagAAAAEP+QsqaHrfEgkKThg4nuPY4St+h211n9gtX/39Z0pTi2CFJp7kFSKecIzY7oydkkCQ==", "d3e7ca73-86d8-43f8-8b3d-61116a986f26" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3380), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3380), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3380), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3390), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3390), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3390), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3440), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3440) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3440), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "WarehouseId" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(6710), 1 });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(9160), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(9160) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(9260) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "Name" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(5260), "Testlager" });

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
