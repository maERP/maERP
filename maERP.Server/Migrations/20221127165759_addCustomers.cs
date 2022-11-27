using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Server.Migrations
{
    /// <inheritdoc />
    public partial class addCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8478289-ff7b-4edc-b360-836ae8a4187e");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "930c4770-88b7-4b85-aada-47879c5f230b", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4df884cd-ed9d-490b-bf56-67c1a02125cc", "AQAAAAIAAYagAAAAEA7ZbwpuN2S1c2vEsAT1LLyrN1JU8TDS7LbLNksZWEBKpx/mt0Na+gP1GXbI2A8kRg==", "858181a3-be96-40da-b26f-f46521e67778" });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 27, 17, 57, 59, 406, DateTimeKind.Local).AddTicks(8990), new DateTime(2022, 11, 27, 17, 57, 59, 406, DateTimeKind.Local).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 27, 17, 57, 59, 406, DateTimeKind.Local).AddTicks(9020), new DateTime(2022, 11, 27, 17, 57, 59, 406, DateTimeKind.Local).AddTicks(9020) });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 11, 27, 17, 57, 59, 406, DateTimeKind.Local).AddTicks(9020), new DateTime(2022, 11, 27, 17, 57, 59, 406, DateTimeKind.Local).AddTicks(9020) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "930c4770-88b7-4b85-aada-47879c5f230b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8478289-ff7b-4edc-b360-836ae8a4187e", "3f6032f3-a635-4693-8ca2-efc776d53566", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "77e3590d-377a-48de-9ec7-f1f4e22029fc", "AQAAAAEAACcQAAAAECJ45wt7xjOBeXpuCbK4cq685+YVzT5cT/gvT1NEcJFYvA7vlK1tW1QcZI/rLy4DRA==", "efa43848-114c-4eee-9fe4-44be39128663" });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 11, 14, 28, 40, 194, DateTimeKind.Local).AddTicks(7980), new DateTime(2022, 6, 11, 14, 28, 40, 194, DateTimeKind.Local).AddTicks(8030) });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 11, 14, 28, 40, 194, DateTimeKind.Local).AddTicks(8030), new DateTime(2022, 6, 11, 14, 28, 40, 194, DateTimeKind.Local).AddTicks(8030) });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2022, 6, 11, 14, 28, 40, 194, DateTimeKind.Local).AddTicks(8040), new DateTime(2022, 6, 11, 14, 28, 40, 194, DateTimeKind.Local).AddTicks(8040) });
        }
    }
}
