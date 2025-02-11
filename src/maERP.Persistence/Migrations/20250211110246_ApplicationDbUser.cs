using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationDbUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "SalesChannel",
                newName: "Url");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9afe923a-1822-4caa-9fe0-8baa795aa6df", new DateTime(2025, 2, 11, 11, 2, 45, 670, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 2, 11, 11, 2, 45, 670, DateTimeKind.Utc).AddTicks(1830), "AQAAAAIAAYagAAAAEOU8ZzfCRbff/zycn4Wh8ZmkohQ69VybcxutTgWiR6F4heKA6aJY7Tieez++epkuig==", "4767a2ad-9837-4be2-b17d-0568350f5107" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49b3dad1-67b0-4d94-a653-5b9d0eeb76d9", new DateTime(2025, 2, 11, 11, 2, 45, 705, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 2, 11, 11, 2, 45, 705, DateTimeKind.Utc).AddTicks(500), "AQAAAAIAAYagAAAAEA4DdyK8SxiAyFsniZdZP69wK17UYKAanPm4h5MpYBX143/+2Q81LyhFTXvMR8qn7Q==", "61aca491-1c0d-43a1-9be6-1fc0a19c906c" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8640) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8960), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8970) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(8990) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 2, 11, 11, 2, 45, 740, DateTimeKind.Utc).AddTicks(9030) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(5360), new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(1150), new DateTime(2025, 2, 11, 11, 2, 45, 741, DateTimeKind.Utc).AddTicks(1150) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "SalesChannel",
                newName: "URL");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b4687af-200e-4098-b579-9dc2e4389676", "AQAAAAIAAYagAAAAEKQWdCJwWm46N0lwAjYfPchGpsxQHMmD66ZidCJ/n+cJEvZIIndllPChhwLRfd/7+g==", "7948f1b9-d031-4312-8381-2c90a11b94bc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c31e445-9cb5-48b4-bc53-3d5472259230", "AQAAAAIAAYagAAAAEAmz/+yatdm1DWgp0ESfk1LTl4T8ju1QTsNGRCwbtkexbvtCw7CGOrcpypLuyFF3+w==", "d4d5d93c-e663-4582-8057-80fbab58339a" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3597), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3605) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3609), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3610) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3610), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3611) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3611), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3612) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3612), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3613) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3613), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3614) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3614), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3615) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3615), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3616) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3616), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3617) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3617), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3618), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3619) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3709), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3711), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3711) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3712), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3712) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3713), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3714), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3714) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3715), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3715) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3716), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3716) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3717), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3717) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3718), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3718) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3719), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3719) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3720), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3721), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3722) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3722), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3723) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3723), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3724) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3724), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3725) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3725), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3726) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3726), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3727), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3728) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3728), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3729) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3729), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3730), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3731) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3731), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3732) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3732), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3733) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3733), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3734) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3734), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3735) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3735), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3736) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3736), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3737) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3737), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3738) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3738), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3739) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3739), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3740), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3741) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3741), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3742) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3742), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3743) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3743), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3744) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3744), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3745) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3746), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3746) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3746), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3747) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3747), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3748) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3748), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3749) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3750), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3750), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3751) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3751), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3752) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3752), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3753) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3753), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3754) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3754), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3755) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3755), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3756) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3757), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3757) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3758), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3758) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3759), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3759) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3759), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3761), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3761) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3762), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3762) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3763), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3763) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3764), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3764) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3765), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3765) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3766), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3766) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3767), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3767) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3768), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3768) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3769), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3769) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3770), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3771), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3771) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3772), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3772) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3773), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3773) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3774), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3774) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3775), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3775) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3776), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3776) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3777), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3777) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3778), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3778) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3779), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3779) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3780), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3781), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3781) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3782), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3782) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3783), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3783) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3784), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3784) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3785), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3785) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3786), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3786) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3787), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3788), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3788) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3789), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3789) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3790), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3791), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3791) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3792), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3792) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3793), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3793) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3794), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3794) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3795), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3796), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3796) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3797), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3797) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3798), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3798) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3799), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3799) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3800), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3801), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3801) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3802), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3802) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3803), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3803) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3804), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3804) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3805), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3805) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3806), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3806) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3807), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3807) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3808), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3808) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3809), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3809) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3810), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3811), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3811) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3812), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3813), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3813) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3814), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3814) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3815), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3815) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3816), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3817), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3817) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3818), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3818) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3819), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(3819) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5031), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5031) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5242), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5243) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5244), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5244) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5245), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(5245) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(4826), new DateTime(2024, 7, 8, 14, 34, 53, 642, DateTimeKind.Utc).AddTicks(4826) });
        }
    }
}
