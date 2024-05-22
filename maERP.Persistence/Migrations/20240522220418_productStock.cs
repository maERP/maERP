using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class productStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductStock");

            migrationBuilder.AddColumn<double>(
                name: "Stock",
                table: "ProductStock",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "ProductStock");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProductStock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
