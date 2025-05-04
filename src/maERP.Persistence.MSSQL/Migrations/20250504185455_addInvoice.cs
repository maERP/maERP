using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maERP.Persistence.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class addInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    InvoiceStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15c5e0bb-9bff-4e3d-8cd7-ed948887ada9", new DateTime(2025, 5, 4, 18, 54, 55, 54, DateTimeKind.Utc).AddTicks(9350), new DateTime(2025, 5, 4, 18, 54, 55, 54, DateTimeKind.Utc).AddTicks(9350), "AQAAAAIAAYagAAAAEFJ3qPUlDUxZHOZPkPJbA7wXJO9tnLBP+XNGuebq1vHePRQgtcPp0wd/ssQqInAbNg==", "b8bae9db-2733-4f94-9d5a-209802c8f8a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c780527c-7a87-40b6-a326-baea7c0be2be", new DateTime(2025, 5, 4, 18, 54, 55, 89, DateTimeKind.Utc).AddTicks(5680), new DateTime(2025, 5, 4, 18, 54, 55, 89, DateTimeKind.Utc).AddTicks(5680), "AQAAAAIAAYagAAAAEOtvGJyb0warKi9Ih0+gL3vh9Vl7wkxrEsHQ4CNmOEVZfU8PX15VWsOr82DgA8tC/w==", "c6de3254-e213-4dad-a22c-60911399d606" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2360) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2750), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2780), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(7050), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(7050) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(9660), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(9660) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(9760) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 5, 4, 18, 54, 55, 124, DateTimeKind.Utc).AddTicks(5430) });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OrderId",
                table: "Invoice",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_ProductId",
                table: "InvoiceItem",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebac8cd7-307d-492a-897f-1fa69b96a2b4", new DateTime(2025, 5, 4, 13, 36, 10, 821, DateTimeKind.Utc).AddTicks(2434), new DateTime(2025, 5, 4, 13, 36, 10, 821, DateTimeKind.Utc).AddTicks(2436), "AQAAAAIAAYagAAAAEO/afSSErGbItKmQsd3fzW1os08kin6+IFFT7HyaXQG9lVAAJlSBnTMMrcsYR2JPYg==", "0b3797d1-2f7e-4da7-b647-3a6cfde8e611" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2c41d17-8db3-4d3c-88f6-600a1aefa4d5", new DateTime(2025, 5, 4, 13, 36, 10, 856, DateTimeKind.Utc).AddTicks(6336), new DateTime(2025, 5, 4, 13, 36, 10, 856, DateTimeKind.Utc).AddTicks(6339), "AQAAAAIAAYagAAAAEKXiFvyRym/q75gVxc1xu1ObzgowADQaiyHZ5qDX3mHoYuvvnJT/PHQoyTSXh+VEaQ==", "1348ddb6-d5e2-4750-a728-4913c6d60438" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(5773), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(5776) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6124), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6125) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6126), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6126) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6128), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6128) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6130), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6130) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6131), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6131) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6132), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6132) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6133), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6133) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6134), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6134) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6135), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6136) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6136), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6137) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6138), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6138) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6139), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6139) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6141), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6141) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6142), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6143) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6144), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6144) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6145), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6145) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6146), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6146) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6147), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6147) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6149), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6149) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6150), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6151), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6151) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6152), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6152) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6153), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6154) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6154), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6155) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6155), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6156) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6156), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6157) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6168), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6168) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6169), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6170) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6171), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6171) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6172), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6172) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6173), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6173) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6174), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6174) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6175), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6175) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6176), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6176) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6177), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6178) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6179), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6179) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6180) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6181), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6182) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6182), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6183) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6184), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6184) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6185), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6185) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6186), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6186) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6187), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6187) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6188), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6188) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6189), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6189) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6190), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6191), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6192) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6192), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6193) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6194), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6194) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6195), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6195) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6196), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6196) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6197), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6197) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6198), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6198) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6199), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6199) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6200), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6201) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6208), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6210), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6211), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6211) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6212), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6213) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6214), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6214) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6215), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6215) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6216), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6216) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6217), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6217) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6218), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6219) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6220), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6220) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6221), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6221) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6222), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6222) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6223), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6223) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6224), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6224) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6225), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6225) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6226), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6227) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6228), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6228) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6229), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6229) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6230), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6230) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6231), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6231) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6232), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6232) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6233), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6234) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6234), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6235) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6235), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6236) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6237), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6237) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6238), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6238) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6239), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6239) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6247), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6248) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6249), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6249) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6251) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6251), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6252) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6253), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6253) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6254), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6254) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6255), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6255) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6256), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6256) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6257), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6257) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6258), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6258) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6259), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6259) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6261) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6262), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6262) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6263), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6263) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6264), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6264) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6265), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6266) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6266), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6267) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6268), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6268) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6269), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6269) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6271), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6271) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6272), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6272) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6273), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6274) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6275), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6275) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6276), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6276) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6277), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6277) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6278), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6278) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6279), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6279) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6281) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6282), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6282) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6283), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6283) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6284), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6284) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6285), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6285) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6286), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6287) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6288), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(6288) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(1076), new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(1077) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(3728), new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(3728) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(3858), new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(3858) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(3859), new DateTime(2025, 5, 4, 13, 36, 10, 892, DateTimeKind.Utc).AddTicks(3860) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(9175), new DateTime(2025, 5, 4, 13, 36, 10, 891, DateTimeKind.Utc).AddTicks(9176) });
        }
    }
}
