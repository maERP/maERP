using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class addOrderHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OldStatus = table.Column<int>(type: "int", nullable: false),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OldValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NewValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistory_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6fba779c-d67e-4224-ac16-f31444e4f016", new DateTime(2025, 4, 7, 15, 52, 20, 406, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 4, 7, 15, 52, 20, 406, DateTimeKind.Utc).AddTicks(8210), "AQAAAAIAAYagAAAAEG/rqOxLV397iRCf5kwCiL40KekCsLEncfR0QhknrPAul0AsnUJ9P5Cyqd2hPw5p+g==", "723c894d-dff6-4008-b80e-e87667d5853d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93089403-ab48-4711-99de-df972e7e7eaa", new DateTime(2025, 4, 7, 15, 52, 20, 442, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 4, 7, 15, 52, 20, 442, DateTimeKind.Utc).AddTicks(3500), "AQAAAAIAAYagAAAAEGHj4aA/eWnUB22JyiiUXHonJEB0I6Hpd7foz0rqkPUvA74IL2k/3YDNGkk0fG8X9Q==", "8fb865e4-f1c4-47b8-961b-90a80b21032d" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7000), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(1150) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3870), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3870) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 4, 7, 15, 52, 20, 477, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(9450), new DateTime(2025, 4, 7, 15, 52, 20, 476, DateTimeKind.Utc).AddTicks(9450) });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "764f1e43-9106-4eb5-971c-f6b4f6f2f75d", new DateTime(2025, 3, 3, 18, 43, 7, 937, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 3, 3, 18, 43, 7, 937, DateTimeKind.Utc).AddTicks(4870), "AQAAAAIAAYagAAAAEAdUh6X4nOFpjk005KcP2Khp4o4OL1iSZWgcMXveGGiGCJgwTT1zODS3YIUyLnke2Q==", "ef1d64cb-fea2-439b-bf4b-6d0d022178c2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4100d184-c198-47a2-ac46-9a8ff5a0c8ff", new DateTime(2025, 3, 3, 18, 43, 7, 972, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 3, 3, 18, 43, 7, 972, DateTimeKind.Utc).AddTicks(4680), "AQAAAAIAAYagAAAAEA11Gbv2XGP8fitz2bC095PE2i0XeM4EBAj/GIsAOTc/y2U3YcOrVUDDiMw4xu8BfQ==", "4a3005dc-8b84-468d-ba2c-c5496e629392" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(7120), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9650) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(5560), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(5560) });
        }
    }
}
