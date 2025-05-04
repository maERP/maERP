using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MinimumProfit",
                table: "ProductSalesChannel",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MinimumProfitUnit",
                table: "ProductSalesChannel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RepricingType",
                table: "ProductSalesChannel",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InvoiceSent",
                table: "Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OrderConfirmationSent",
                table: "Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShippingInformationSent",
                table: "Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0b529e2-f750-48eb-941a-84d01b1d8d2d", new DateTime(2025, 5, 4, 13, 36, 6, 697, DateTimeKind.Utc).AddTicks(7172), new DateTime(2025, 5, 4, 13, 36, 6, 697, DateTimeKind.Utc).AddTicks(7173), "AQAAAAIAAYagAAAAENbwTLolQ6aOjMyZt0BszuNNb+vVhTZ+OhBU2yEXPGdrs8V+oR4VOH7afRWTgqY1bg==", "ba43ee42-e00b-47d3-99ed-c115538b9b92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ab8caaf-3d15-444b-9be9-33da48770f50", new DateTime(2025, 5, 4, 13, 36, 6, 730, DateTimeKind.Utc).AddTicks(1466), new DateTime(2025, 5, 4, 13, 36, 6, 730, DateTimeKind.Utc).AddTicks(1467), "AQAAAAIAAYagAAAAEIs+oNnyem/DCNM5vqqGckf2X+CACEznpIWDBhVxOItzFjJ53zxrr9G8Zo12EQoIVQ==", "4b6cd960-fbaf-49a5-be55-198edf026642" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(59), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(61) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(293), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(293) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(294), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(294) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(295), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(295) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(296), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(296) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(297), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(297) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(298), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(299) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(299), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(299) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(300) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(301), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(301) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(302), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(302) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(303), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(303) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(304), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(304) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(305), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(305) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(306), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(306) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(307), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(307) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(308), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(308) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(309), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(309) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(310), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(310) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(311), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(311) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(312), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(312) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(313), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(313) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(313), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(314) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(314), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(315) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(315), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(316) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(316), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(317) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(317), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(317) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(318), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(318) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(327), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(327) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(328), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(328) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(329), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(329) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(330), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(331), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(331) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(332), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(332) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(333), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(333) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(334), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(334) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(335), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(335) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(336), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(336) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(336), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(337) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(337), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(338) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(338), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(338) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(339), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(339) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(340), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(340) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(341), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(341) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(342), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(342) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(343), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(343) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(344), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(344) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(345), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(345) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(345), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(347) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(347), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(347) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(348), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(348) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(349), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(349) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(350), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(350) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(351), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(351) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(352), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(352) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(358), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(358) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(359), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(359) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(360), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(361), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(361) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(362), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(362) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(363), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(363) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(364), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(364) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(365), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(366), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(366) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(367), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(367) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(368), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(368) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(368), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(369) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(369), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(370), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(371), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(371) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(372), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(372) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(373), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(373) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(374), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(374) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(375), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(375) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(376), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(376) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(377), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(377) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(377), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(378) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(378), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(379) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(379), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(380) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(381) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(381), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(382) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(382), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(382) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(383), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(383) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(384), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(384) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(391) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(391), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(392) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(392), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(393) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(393), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(393) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(394), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(394) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(395), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(395) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(396), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(396) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(397), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(397) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(398), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(398) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(399), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(399) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(401), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(401) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(401), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(402) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(402), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(403) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(404), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(404) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(405), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(405) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(406), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(406) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(407), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(407) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(407), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(408) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(439), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(439) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(440), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(441) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(441), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(442) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(442), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(442) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(443), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(443) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(444), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(444) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(445), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(445) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(446), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(446) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(447), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(447) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(448), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(448) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(449), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(449) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(450), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(450) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(451), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(451) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(452), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(452) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(453), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(453) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(454), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(454) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(5541), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(5542) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(8195), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(8195) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(8293), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(8293) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(8294), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(8294) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(3285), new DateTime(2025, 5, 4, 13, 36, 6, 763, DateTimeKind.Utc).AddTicks(3286) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumProfit",
                table: "ProductSalesChannel");

            migrationBuilder.DropColumn(
                name: "MinimumProfitUnit",
                table: "ProductSalesChannel");

            migrationBuilder.DropColumn(
                name: "RepricingType",
                table: "ProductSalesChannel");

            migrationBuilder.DropColumn(
                name: "InvoiceSent",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderConfirmationSent",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShippingInformationSent",
                table: "Order");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d6427a6-3348-4942-bb61-d6586e7092b0", new DateTime(2025, 5, 2, 8, 37, 14, 191, DateTimeKind.Utc).AddTicks(7500), new DateTime(2025, 5, 2, 8, 37, 14, 191, DateTimeKind.Utc).AddTicks(7560), "AQAAAAIAAYagAAAAEIhzwPURIGxujb1fiVfJsozkYRici6K3y+Q9f3v/8YHiPOVKq3t866TjsrvdtnsoLg==", "b030f218-6655-466a-bee7-c25c929a552b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "007f5251-2c01-484f-8568-f67b8ed39404", new DateTime(2025, 5, 2, 8, 37, 14, 227, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 5, 2, 8, 37, 14, 227, DateTimeKind.Utc).AddTicks(1150), "AQAAAAIAAYagAAAAEKxV0vE4tHpdkVNZo6L+YZXdfDCMW9kQ73Sv0KY1Ss/5F5SdZA7SSyX6m6VY807WsQ==", "8523f253-8094-4a38-926a-cab3a880763d" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7230), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7580) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7590) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7610), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(7630) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(3900), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(3900) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 5, 2, 8, 37, 14, 263, DateTimeKind.Utc).AddTicks(4000) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 5, 2, 8, 37, 14, 262, DateTimeKind.Utc).AddTicks(9600) });
        }
    }
}
