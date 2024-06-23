using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAIModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Setting");

            migrationBuilder.CreateTable(
                name: "AIModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AIType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ApiUsername = table.Column<string>(type: "text", nullable: false),
                    ApiPassword = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    PromptText = table.Column<string>(type: "text", nullable: false),
                    AIType = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompt", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9135600b-9bab-4f4e-8112-e9b39e45bbf4", "AQAAAAIAAYagAAAAEOLjdDLIK4LvOeUJCRtwjEGn8GRzohzXDmaSM+qfCNU2DEyBWlES4+ZUQ7zdLnevnw==", "f0aeb9aa-df77-4d35-9777-fea23d3f6bd2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "caa670f7-b4b4-4722-bc13-33f985bafd85", "AQAAAAIAAYagAAAAEJlapUOqs9xaQRtDwWPsFb8301Cm4uXi5ITDylkSN3cBdHG5Y+Lh1218ceJJsk1Wvw==", "c82b4c34-0cfa-4222-8937-469f97b14672" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3040), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3040) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3050) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3060) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3190), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3190), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3190), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3210), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3250) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(3260) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4200), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4200) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4360), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4360), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4360), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4030), new DateTime(2024, 6, 23, 16, 52, 41, 850, DateTimeKind.Utc).AddTicks(4040) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AIModel");

            migrationBuilder.DropTable(
                name: "Prompt");

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "Setting",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8fcb1c13-f65e-49f8-8d28-53b89efa4a09", "AQAAAAIAAYagAAAAEMkKvTeWRHLVAEFZppkEl0qaTy0qtc7u8tBbEt5klc+fhiqdT2GOv7tOIcYSfsUg2g==", "382c65c8-6d76-428b-b760-706b02b50fcf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7f9d4e3-b7ac-46b2-bdfd-41075fabcf52", "AQAAAAIAAYagAAAAEBq85fZPcJ+y5L73zBAu8Tp0YM8XtZdfJGadKc5w7U5+kTjtyR0BjSSqSahD7+Pz7A==", "b75637f7-1d18-4f20-90e1-7535b49fbf4d" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6057), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6072) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6080), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6082) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6084), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6084) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6085), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6086) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6087), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6089) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6089), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6090), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6090) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6091), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6091) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6092), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6094) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6094), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6094) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6095), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6097) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6098), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6098) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6099), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6100) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6100), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6101) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6101), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6101) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6102), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6103) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6104), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6104) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6105), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6105) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6105), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6107) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6107), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6108) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6108), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6109) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6109), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6110), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6110) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6111), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6111) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6112), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6112) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6113), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6113) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6114), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6116) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6116), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6117), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6119) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6119), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6121) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6122), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6122) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6123), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6125) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6126), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6127) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6128), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6130), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6132) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6132), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6135) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6135), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6138) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6138), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6141) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6141), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6142) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6142), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6144) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6145), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6146) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6147), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6148) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6149), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6151) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6152), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6154) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6155), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6158) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6159), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6161) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6161), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6161) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6162), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6164) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6164), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6166) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6166), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6168) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6169), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6172) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6172), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6174) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6175), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6176) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6177), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6179) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6179), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6180), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6182) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6183), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6184) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6185), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6187) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6187), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6190), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6192) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6192), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6197) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6197), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6199) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6200), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6200) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6201), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6203) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6204), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6206) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6206), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6208) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6209), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6211) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6212), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6214) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6215), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6216) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6217), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6219) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6220), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6223), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6224) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6224), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6226) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6227), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6228) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6229), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6231) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6231), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6233) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6234), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6236) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6236), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6238) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6238), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6239) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6239), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6242) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6242), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6244) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6244), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6247) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6247), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6249) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6250), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6252) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6252), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6254) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6255), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6257) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6258), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6258) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6258), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6386), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6386) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6387), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6387) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6388), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6388) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6388), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6389) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6389), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6390), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6391) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6391), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6391) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6392), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6392) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6393), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6393) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6394), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6394) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6395), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6395) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6396), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6396) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6396), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6397) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6397), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6398), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6398) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6399), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6399) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6400), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6400) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6401), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6401) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6402), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6402) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6403), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6403) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6403), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6404) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6404), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6405) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6405), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6405) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6406), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6406) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6407), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6408), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6408) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6409), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6410), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6410), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6411) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6411), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6412) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6412), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6412) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6413), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6413) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8353), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8353) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8667), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8668) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8706), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8706) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8707), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8707) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8040), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8041) });
        }
    }
}
