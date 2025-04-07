using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    OldStatus = table.Column<int>(type: "integer", nullable: false),
                    NewStatus = table.Column<int>(type: "integer", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    OldValue = table.Column<string>(type: "text", nullable: false),
                    NewValue = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                values: new object[] { "f993ec76-3429-49c4-8d32-951107af6e12", new DateTime(2025, 4, 7, 15, 54, 17, 856, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 4, 7, 15, 54, 17, 856, DateTimeKind.Utc).AddTicks(6420), "AQAAAAIAAYagAAAAEOJF9QQ8QUD+r308OBzt1SyPFEND84fhEvWGGTThQlZzAdo+xmmpFYMj4AR4N5dm6A==", "600b405b-3403-4dd6-9eee-b8951bd831d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf56be89-05d8-485d-8ad2-e64ff9501627", new DateTime(2025, 4, 7, 15, 54, 17, 890, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 4, 7, 15, 54, 17, 890, DateTimeKind.Utc).AddTicks(8410), "AQAAAAIAAYagAAAAEJPzWM+169qLU32mdBi4JwMgvC6n/HFEetRlGaAj7Of2bhtS5QwOH7sntFlLbgMRXg==", "5ef4b558-6507-4ab9-8783-91d0aefdc439" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(4930), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(4930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5250), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5270), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5280) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5290), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(5300) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(8880), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(8880) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1490), new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580), new DateTime(2025, 4, 7, 15, 54, 17, 927, DateTimeKind.Utc).AddTicks(1580) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 4, 7, 15, 54, 17, 926, DateTimeKind.Utc).AddTicks(7280) });

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
                values: new object[] { "a14428e9-029a-4488-a73b-31a2711b7886", new DateTime(2025, 3, 3, 18, 43, 42, 996, DateTimeKind.Utc).AddTicks(7680), new DateTime(2025, 3, 3, 18, 43, 42, 996, DateTimeKind.Utc).AddTicks(7680), "AQAAAAIAAYagAAAAEGIGMtWlmvYcGQUqg9nSxCJT0PNF+GxPp1IakBRDwQjQp1ONCdapCPNO6TctupUzfA==", "e047574e-2759-4b68-ab0b-e234bf36537d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6c1b0af-6611-46f3-bcb4-bf35d421ed6c", new DateTime(2025, 3, 3, 18, 43, 43, 30, DateTimeKind.Utc).AddTicks(5870), new DateTime(2025, 3, 3, 18, 43, 43, 30, DateTimeKind.Utc).AddTicks(5870), "AQAAAAIAAYagAAAAEBKuqdfCss9YXLHKWVZLTZBKlCKbkBamTxuUCXrjIFfhgIfXxXQHAYBIFKl7lBpZIA==", "aa25dbd4-1e1e-4eac-8141-1b43003a1699" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8370), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8710), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8800), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8810), new DateTime(2025, 3, 3, 18, 43, 43, 65, DateTimeKind.Utc).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(2830), new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(2830) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(5590), new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(5590) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(5690), new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(1190), new DateTime(2025, 3, 3, 18, 43, 43, 66, DateTimeKind.Utc).AddTicks(1190) });
        }
    }
}
