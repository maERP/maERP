using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
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
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldStatus = table.Column<int>(type: "int", nullable: false),
                    NewStatus = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "747ecc28-52c4-4109-aaaf-3b76bd72d565", new DateTime(2025, 4, 7, 15, 53, 52, 646, DateTimeKind.Utc).AddTicks(8000), new DateTime(2025, 4, 7, 15, 53, 52, 646, DateTimeKind.Utc).AddTicks(8000), "AQAAAAIAAYagAAAAEER44k2x4DoT46AJFbp45Mu/WbxuPZ52PYMX7DOYzyf/6PbPOw8hn3D/R9gZKD3fwg==", "923329a4-bbb7-4ee7-876e-3cfae47931a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24aa5191-6d5b-44c9-814f-d0442a710001", new DateTime(2025, 4, 7, 15, 53, 52, 682, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 682, DateTimeKind.Utc).AddTicks(7300), "AQAAAAIAAYagAAAAEL9JUho6/A2x3IfSGDN3bB5u2qz45lG9QkCfokKlUxZtfgnNZX3R1Hmr+IJ4nF6skQ==", "296e739a-c765-4c8b-a389-84355b8ef670" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(6970), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(6970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7300), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7320), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(7350) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(1660) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 4, 7, 15, 53, 52, 719, DateTimeKind.Utc).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(9870), new DateTime(2025, 4, 7, 15, 53, 52, 718, DateTimeKind.Utc).AddTicks(9870) });

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
                values: new object[] { "f83122b9-563c-4253-8292-9717576c278f", new DateTime(2025, 3, 3, 18, 43, 31, 565, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 3, 3, 18, 43, 31, 565, DateTimeKind.Utc).AddTicks(2640), "AQAAAAIAAYagAAAAECUONVGmqOszvJMSUsX1afbtTUNLZN5GakttyAtWYfo7T3kYvnCfU/xXj/a7n7mnGQ==", "c56d805b-3bf6-4b6c-afcc-d5d510474c29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "363c71b8-656f-4f9e-b666-28dd0bd12232", new DateTime(2025, 3, 3, 18, 43, 31, 600, DateTimeKind.Utc).AddTicks(8020), new DateTime(2025, 3, 3, 18, 43, 31, 600, DateTimeKind.Utc).AddTicks(8020), "AQAAAAIAAYagAAAAECvaYFvjS0XY0zRZB8LDnYCPDuThtkRh3N/WegdlSTkNlOv7SrGwM9weHL8w20ySMg==", "7caed5a1-18bf-4111-bc83-515963094f6c" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5520), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5520) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5830), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5890) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(9600) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 637, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 3, 3, 18, 43, 31, 637, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 637, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 3, 3, 18, 43, 31, 637, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 637, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 3, 3, 18, 43, 31, 637, DateTimeKind.Utc).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(8010), new DateTime(2025, 3, 3, 18, 43, 31, 636, DateTimeKind.Utc).AddTicks(8010) });
        }
    }
}
