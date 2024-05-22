using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemSerialNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "OrderItem");

            migrationBuilder.AlterColumn<double>(
                name: "TaxRate",
                table: "OrderItem",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "OrderItem",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "OrderItemSerialNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderItemId = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemSerialNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemSerialNumber_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fec3c87-19fb-4e70-8038-8e292c85805c", "AQAAAAIAAYagAAAAEGiYhOZXjCzP0Gr4agOhW6e3Lu9JNUCmRsg6EQIUME7xuzMgZw2KQzKj7xIyaYUTqg==", "20b71704-61bf-4de0-9c51-8a9a2db27568" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0168455-3c70-4877-abf1-d165eea74046", "AQAAAAIAAYagAAAAEEDycOhi2AwRH8as3txJwuC3NnDd0i78Zzixgc/oBrZnjLGGt1e1jfaCDEqx2TfBoQ==", "5801c090-5bc1-4e5c-8738-6e15662c4bf1" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(760), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(766) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(770), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(771) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(772), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(772) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(773), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(773) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(774), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(774) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(775), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(775) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(775), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(776) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(776), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(777) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(777), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(778) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(778), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(778) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(779), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(779) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(780), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(781), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(781) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(782), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(782) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(783), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(783) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(784), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(784) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(784), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(785) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(785), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(786) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(786), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(787), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(788), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(788) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(789), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(789) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(790), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(791), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(791) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(792), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(792) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(793), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(793) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(794), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(794) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(794), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(795) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(795), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(796) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(796), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(797) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(797), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(797) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(798), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(798) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(799), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(799) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(800), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(801), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(801) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(802), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(802) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(803), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(803) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(803), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(804) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(804), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(805) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(805), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(806) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(806), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(806) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(807), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(807) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(808), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(808) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(809), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(809) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(810), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(811), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(811) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(812), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(813), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(813) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(813), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(814) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(814), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(815) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(815), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(816), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(817), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(817) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(818), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(818) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(819), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(819) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(820), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(820) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(821), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(821) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(822), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(822) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(823), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(823) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(823), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(824) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(824), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(825) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(825), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(826) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(826), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(826) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(827), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(827) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(828), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(828) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(829), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(829) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(830), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(893), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(893) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(894), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(894) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(895), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(895) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(896), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(896) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(897), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(897) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(898), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(898) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(898), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(899) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(899), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(900) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(900), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(901) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(901), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(901) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(902), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(902) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(903), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(903) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(904), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(904) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(905), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(905) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(906), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(906) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(907), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(907) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(907), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(908) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(908), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(909) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(909), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(910), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(910) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(911), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(911) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(912), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(912) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(913), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(913) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(914), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(914) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(915), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(915) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(916), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(916) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(916), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(917) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(917), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(918) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(918), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(919) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(919), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(919) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(920), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(920) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(921), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(921) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(922), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(922) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(923), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(923) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(924), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(924) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(925), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(925) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(925), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(926) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(926), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(927) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(927), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(928) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(928), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(928) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(929), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(929) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(930), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(931), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(931) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(932), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(932) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(933), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(933) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(934), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(934) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(934), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(935) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(935), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(936) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(936), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(937) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(937), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(937) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(938), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(938) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(939), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(939) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(940), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(940) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2123), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2354), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2354) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2355), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2355) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2356), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(2356) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(1910), new DateTime(2024, 5, 22, 8, 21, 31, 918, DateTimeKind.Utc).AddTicks(1910) });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSerialNumber_OrderItemId",
                table: "OrderItemSerialNumber",
                column: "OrderItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItemSerialNumber");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "OrderItem",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItem",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "OrderItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10a21017-59d9-40ca-8133-5b9ac7451113", "AQAAAAIAAYagAAAAEN4/BqSCj6KD7NIuGB0t7dy6rTklwWvPCPc5SNIfEYAfTb1r/2xHNxujs7k2zBP/Cw==", "29f9b042-367b-4e84-bbf1-8553c547c4f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ad77711-72af-41fe-9419-fddaded4f3ba", "AQAAAAIAAYagAAAAEJpaMyjcWiMzAUhKmF6KVktkxA46jK2ZACkqE9m1x9+AjFlP0/wQAPJ4tLi1P5bT5w==", "46bfd986-6620-4643-a6aa-62dac6b480b0" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1795), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1804) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1808), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1809) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1810), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1811), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1812), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1813), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1813) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1814), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1814) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1815), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1815) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1815), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1816), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1817), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1817) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1818), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1818) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1819), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1820) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1821), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1821) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1821), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1822) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1822), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1823) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1823), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1823) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1824), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1824) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1825), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1825) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1826), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1826) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1826), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1827) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1827), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1828) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1828), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1828) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1829), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1829) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1830), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1831), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1831) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1831), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1832) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1832), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1832) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1833), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1833) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1834), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1834) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1834), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1835) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1835), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1836) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1836), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1836) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1837), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1837) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1966), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1966) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1967), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1967) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1968), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1968) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1969), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1969) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1969), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1970), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1971), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1971) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1972), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1972) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1973), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1973) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1973), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1974), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1974) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1975), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1975) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1976), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1976) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1977), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1977) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1977), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1978) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1978), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1978) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1979), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1979) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1980), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1981), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1981) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1981), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1982) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1982), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1983) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1983), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1983) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1984), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1984) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1985), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1985) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1986), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1986) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1986), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1987) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1987), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1987) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1988), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1988) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1989), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1989) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1990), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1990), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1991) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1991), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1991) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1992), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1992) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1993), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1993) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1994), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1994) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1994), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1995) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1995), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1996) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1996), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1996) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1997), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1997) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1998), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1998) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1998), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1999) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(1999), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2000), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2001), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2001) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2002), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2002) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2003), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2003) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2003), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2004) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2004), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2004) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2005), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2005) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2006), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2006) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2007), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2007) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2007), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2008), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2009), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2009) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2010), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2011), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2011) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2011), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2012) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2012), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2012) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2013), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2013) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2014), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2014) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2015), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2015) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2015), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2016) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2016), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2016) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2017), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2017) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2018), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2018) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2019), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2019) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2019), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2020), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2021), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2021) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2022), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2022) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2023), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2023) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2023), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2024) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2024), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2024) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2025), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2025) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2026), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2026) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2027), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2027) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2027), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2028) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2028), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2028) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2029), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2029) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2030), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2031), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2031) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2031), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2032) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2032), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2032) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2033), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2033) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2034), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2034) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2035), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(2035) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(3980), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(4254), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(4254) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(4256), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(4256) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(4257), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(4257) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(3632), new DateTime(2024, 5, 21, 9, 50, 3, 218, DateTimeKind.Utc).AddTicks(3632) });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId",
                unique: true);
        }
    }
}
