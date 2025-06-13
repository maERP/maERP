using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class addNCtx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NCtx",
                table: "AiModel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(6710) });

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
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(5260), new DateTime(2025, 6, 13, 9, 7, 7, 351, DateTimeKind.Utc).AddTicks(5260) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NCtx",
                table: "AiModel");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "813a2230-9c11-4f78-b698-0a3e0ba078b6", new DateTime(2025, 5, 10, 9, 25, 6, 849, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 5, 10, 9, 25, 6, 849, DateTimeKind.Utc).AddTicks(1700), "AQAAAAIAAYagAAAAEPMybIpkneqg7wau9TkayKBKjmpL7orc//7dYECsIVGfujjtM0SMkIwDNnakFtsSgw==", "213ea59b-2ae8-4756-926f-e0174a9bd4f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d8b5939-87d7-4687-b066-8867fde8c110", new DateTime(2025, 5, 10, 9, 25, 6, 883, DateTimeKind.Utc).AddTicks(7810), new DateTime(2025, 5, 10, 9, 25, 6, 883, DateTimeKind.Utc).AddTicks(7810), "AQAAAAIAAYagAAAAEJeUaiFY4sAx0QPWPg8K1v6suf91Esu9JvvGN7YLO/AETYcg0I2KctViJOeA2yDnTA==", "d45e48d3-9fa3-431c-8a1b-0551f1516b48" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8260), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8550) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8560) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8600), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8620) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8630), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8650), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8650), new DateTime(2025, 5, 10, 9, 25, 6, 917, DateTimeKind.Utc).AddTicks(8650) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(2340) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(4910), new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(720), new DateTime(2025, 5, 10, 9, 25, 6, 918, DateTimeKind.Utc).AddTicks(720) });
        }
    }
}
