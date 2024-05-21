using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class orderTotalTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tax",
                table: "Order",
                newName: "TotalTax");

            migrationBuilder.CreateTable(
                name: "CustomerSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    SalesChannelId = table.Column<int>(type: "integer", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSalesChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSalesChannel_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CustomerSalesChannel_CustomerId",
                table: "CustomerSalesChannel",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerSalesChannel");

            migrationBuilder.RenameColumn(
                name: "TotalTax",
                table: "Order",
                newName: "Tax");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d27e8c0-ffb8-478b-a750-97273c3d72f5", "AQAAAAIAAYagAAAAEOXhHeXl4NVx1Zf+QSWHdFSZn5UhATvNcwW19jezs04AekmxdL7T7V9GvWaT9Htaxw==", "565e7a6c-23e4-4a78-a012-264a0b68f91f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "71345e0c-c782-4e69-a2ec-2cfc5271e115", "AQAAAAIAAYagAAAAEFmRbhv9XYlVs/ssYZhzYzq0gw/m8IDZel59u7cyO4jfixtabnArBuHJ3k0rR4oFWA==", "d86463a0-da27-47ff-9d30-2c866f553943" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3940), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3953) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3959), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3959) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3960), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3961), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3961) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3962), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3962) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3963), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3963) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3963), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3964) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3964), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3967), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3967) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3968), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3968) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3969), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3969) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3969), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3970), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3971) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3971), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3972), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3972) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3973), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3973) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3974), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3974) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3975), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3975) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3976), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3976) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3977), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3977) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3978), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3978) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3979), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3979) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3979), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3980), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3981) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3981), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3982) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3982), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3982) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3983), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3983) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3984), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3984) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3985), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3985) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3986), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3986) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3987), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3987) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3988), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3988) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3988), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3989) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3989), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3990), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3991) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3991), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3991) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3992), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3992) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3993), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3993) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3994), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3994) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3995), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3995) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3996), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3996) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3997), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3998), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3998) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3998), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3999) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3999), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4000), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4001) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4001), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4001) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4002), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4002) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4003), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4003) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4004), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4004) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4005), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4005) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4006), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4006) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4007), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4007) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4008), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4008) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4009), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4009) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4009), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4010), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4011) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4011), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4011) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4012), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4012) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4013), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4013) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4014), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4014) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4015), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4015) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4016), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4016) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4017), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4017) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4018), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4018) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4018), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4019) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4019), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4020), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4021), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4021) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4022), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4022) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4023), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4023) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4024), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4024) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4025), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4025) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4026), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4026) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4027), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4027) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4027), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4028) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4028), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4029) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4029), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4030), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4031), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4031) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4032), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4032) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4033), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4033) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4034), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4034) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4035), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4035) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4036), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4036) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4037), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4037) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4037), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4038) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4038), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4039) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4039), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4040), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4040) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4041), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4041) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4042), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4042) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4043), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4043) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4044), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4044) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4045), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4045) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4046), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4046) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4046), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4047) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4047), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4048) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4048), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4049) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4049), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4049) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4050), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4051), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4051) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4052), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4052) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4053), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4053) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4054), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4054) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4055), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4055) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4056), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4056) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4056), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4057) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4057), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4058) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4058), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4058) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4059), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4060), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4061), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4061) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4062), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4062) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4063), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4063) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4064), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4064) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4065), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4065) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4066), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4066) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4067), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4067) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4067), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4068) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5916), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5917) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6246), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6246) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6250), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6251) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6251), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6251) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5613), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5613) });
        }
    }
}
