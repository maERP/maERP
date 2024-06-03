using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialProductImportExport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InitialProductExportCompleted",
                table: "SalesChannel",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InitialProductImportCompleted",
                table: "SalesChannel",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MissingProductEan",
                table: "OrderItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissingProductSku",
                table: "OrderItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25d3eedf-a0e9-417c-98b9-0e7d2350675f", "AQAAAAIAAYagAAAAELyEIw/rNPb74sQZb3PNx1tOiLKciMDOtCyRzQLqauzEOb3ML57x/ZrbSxS1ikA5Rw==", "5f8e009d-6dfb-49f0-8b87-0068c15d427e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4eaeb17e-77ea-4a70-b450-de956b80e8ac", "AQAAAAIAAYagAAAAELQv0NoCyAY6k2GoJs34iG+2YiyWq2dx/QDouVy6ZJdorHWSNqpytcB/F4MpsoeGrA==", "e25ab717-d449-4989-b83b-5d5670b23e03" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7597), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7602) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7607), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7608) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7609), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7610), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7611) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7611), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7612) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7612), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7612) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7613), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7613) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7614), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7614) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7615), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7615) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7696), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7697) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7697), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7698) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7698), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7698) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7699), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7699) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7700), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7701), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7701) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7702), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7702) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7702), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7703), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7703) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7704), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7704) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7705), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7705) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7706), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7706) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7707), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7707) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7707), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7708) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7708), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7709) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7709), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7709) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7710), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7711), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7711) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7712), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7712) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7712), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7713) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7713), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7713) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7714), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7715), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7715) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7716), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7716) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7717), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7717) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7717), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7718) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7718), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7718) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7719), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7719) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7720), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7721), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7721) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7722), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7722) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7722), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7723) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7723), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7723) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7724), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7724) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7725), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7725) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7726), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7726) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7727), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7727) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7727), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7728) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7728), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7728) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7729), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7729) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7730), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7731), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7731) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7732), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7732) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7732), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7733) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7733), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7733) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7734), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7734) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7735), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7735) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7736), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7736) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7737), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7737) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7737), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7738) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7738), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7739) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7739), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7739) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7740), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7741), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7741) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7742), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7742) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7742), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7743) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7743), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7743) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7744), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7744) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7745), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7745) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7746), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7746) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7747), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7747) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7747), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7748) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7748), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7748) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7749), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7749) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7750), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7751), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7751) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7752), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7752) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7752), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7753) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7753), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7753) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7754), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7754) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7755), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7755) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7756), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7756) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7757), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7757) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7757), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7758) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7758), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7758) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7759), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7759) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7760), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7761), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7761) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7762), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7762) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7762), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7763) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7763), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7763) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7764), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7764) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7765), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7765) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7766), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7766) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7767), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7767) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7767), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7768) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7768), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7769) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7769), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7769) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7770), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7771), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7771) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7772), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7772) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7772), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7773) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7773), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7774) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7774), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7774) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7775), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7775) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7776), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7776) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7777), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7777) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7777), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7778) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7778), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7778) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7779), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7779) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7780), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7781), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7781) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7781), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7782) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7782), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7783) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7783), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7783) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7784), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7784) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7785), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7785) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7786), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7786) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7787), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7787), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7788) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7788), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(7788) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "InitialProductExportCompleted", "InitialProductImportCompleted" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(8857), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(8857), false, false });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(9062), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(9062) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(9064), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(9064) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(9064), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(9065) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(8682), new DateTime(2024, 6, 3, 7, 19, 48, 76, DateTimeKind.Utc).AddTicks(8682) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialProductExportCompleted",
                table: "SalesChannel");

            migrationBuilder.DropColumn(
                name: "InitialProductImportCompleted",
                table: "SalesChannel");

            migrationBuilder.DropColumn(
                name: "MissingProductEan",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "MissingProductSku",
                table: "OrderItem");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35a7400f-7198-4886-be1c-0bb647410e43", "AQAAAAIAAYagAAAAELxKuBlfDG+RGMqRobe8EI/OIoQYx9F9FbtN+ChptgtoWHPRdxTJUJav/S0PBhGjrw==", "cc9ba91e-ad4a-4bb8-b090-b58b2d7ee1b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6a464d1-2467-4679-8658-ab37df4f5a4e", "AQAAAAIAAYagAAAAEH5B30wj/YS+dVv56QDLxRJDSfWngCBXIgaemCMjcNYNccVkAOHk428IEjQpYEyK3g==", "0d1ec9ab-7e5e-46d7-950f-11faedb6e730" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5768), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5785), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5786) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5786), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5787), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5788), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5789), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5789) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5790), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5791), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5791) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5792), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5792) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5792), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5793) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5793), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5794) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5794), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5794) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5795), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5795) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5796), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5796) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5797), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5797) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5798), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5798) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5799), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5799) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5799), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5800), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5801) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5801), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5801) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5802), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5802) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5803), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5803) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5804), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5804) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5805), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5805) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5805), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5806) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5806), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5807) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5807), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5808) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5808), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5808) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5809), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5809) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5810), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5811), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5811) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5812), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5813), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5813) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5813), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5814) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5814), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5815) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5815), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5815) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5816), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5817), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5817) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5818), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5818) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5819), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5819) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5819), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5820) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5820), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5821) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5821), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5822) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5822), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5822) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5823), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5823) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5824), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5824) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5825), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5826), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5826) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5826), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5827) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5827), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5828) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5828), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5828) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5829), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5829) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5830), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5831), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5831) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5832), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5832) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5833), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5833) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5833), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5834) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5834), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5835) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5835), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5835) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5836), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5836) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5837), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5837) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5838), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5838) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5839), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5839) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5840), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5840), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5841) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5841), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5842) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5842), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5842) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5843), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5843) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5844), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5844) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5845), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5846), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5846) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5846), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5847) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5847), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5848) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5848), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5848) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5849), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5849) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5850), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5851), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5852), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5852) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5853), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5853) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5853), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5854) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5854), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5855) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5855), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5855) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5856), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5856) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5857), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5857) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5858), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5858) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5859), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5859) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5860), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5861), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5861) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5861), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5862) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5862), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5863) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5863), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5863) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5864), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5864) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5865), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5865) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5866), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5866) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5867), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5867) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5867), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5868) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5868), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5869) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5869), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5870), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5870) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5871), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5871) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5872), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5872) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5873), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5873) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5874), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5874) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5874), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5875) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5875), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5876) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5876), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5876) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5877), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5877) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5878), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5878) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5879), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5879) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5880), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5880) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5881), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5881) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5881), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5882) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5882), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5883) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5883), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5883) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5884), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5884) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5885), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5885) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5886), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5886) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5887), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5887) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5888), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5888) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5888), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(5889) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8151), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8151) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8501), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8501) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8504), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8504) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8505), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(8505) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(7821), new DateTime(2024, 5, 22, 15, 4, 18, 344, DateTimeKind.Utc).AddTicks(7821) });
        }
    }
}
