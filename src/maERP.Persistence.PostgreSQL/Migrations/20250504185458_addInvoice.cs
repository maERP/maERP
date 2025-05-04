using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace maERP.Persistence.PostgreSQL.Migrations
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: true),
                    Subtotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    InvoiceStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressFirstName = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressLastName = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressCompanyName = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressPhone = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressStreet = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressCity = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressZip = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressCountry = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressFirstName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressLastName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressCompanyName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressPhone = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressZip = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InvoiceId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    EAN = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    OrderItemId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                values: new object[] { "007be001-9c23-44ff-8bd9-acc5c27aa7da", new DateTime(2025, 5, 4, 18, 54, 58, 359, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 5, 4, 18, 54, 58, 359, DateTimeKind.Utc).AddTicks(5740), "AQAAAAIAAYagAAAAENorXjqOj9DxdqNtUZgW05IB/ss4LVgpMNCQ2GG/bFMqL6RKUSpKnb5dYWCSKzBmNQ==", "99deaae4-2725-4a7b-a42c-3745a98c3e1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24b19895-5a51-4243-a32e-a5cc672690b0", new DateTime(2025, 5, 4, 18, 54, 58, 393, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 5, 4, 18, 54, 58, 393, DateTimeKind.Utc).AddTicks(6440), "AQAAAAIAAYagAAAAENiLNtMnsF8y46K5+vvKTKOY7wfWfZfp2kDJzq/VN4ux7QGS6P+pnH7g3DqLkBY7uQ==", "a415ac04-5850-437c-9187-fec147b8d865" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2410) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2670) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2690) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2700) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2710) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2720), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(2730) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(6000), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(6000) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(8580), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(8580) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(8670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(8670) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(8670), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(8670) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 5, 4, 18, 54, 58, 427, DateTimeKind.Utc).AddTicks(4610) });

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
                values: new object[] { "718878a3-2769-46c6-a61a-2d2219ecc4c8", new DateTime(2025, 5, 4, 13, 36, 14, 835, DateTimeKind.Utc).AddTicks(2224), new DateTime(2025, 5, 4, 13, 36, 14, 835, DateTimeKind.Utc).AddTicks(2226), "AQAAAAIAAYagAAAAENTG4E4HEG8qBsDag13ASLMBhi5BOKCkysCbp6nYzEigjnbudcfIT4dd1LFMvyuQuw==", "6b74e672-05a3-4d64-8c98-c62895ecb881" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "DateCreated", "DateModified", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2e73580-5fe6-4bbb-9391-966837fa1319", new DateTime(2025, 5, 4, 13, 36, 14, 868, DateTimeKind.Utc).AddTicks(5128), new DateTime(2025, 5, 4, 13, 36, 14, 868, DateTimeKind.Utc).AddTicks(5130), "AQAAAAIAAYagAAAAEOuCKuPAz14vm7dCS2wQpk6GtT+wtjCUorvZPSmIioOkbY33M1oSuJMmBYEotXBMxQ==", "70fce629-dac9-45f9-98fc-ae9961786c87" });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1492), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1494) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1733), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1733) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1734), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1734) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1735), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1735) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1736), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1736) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1737), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1737) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1738), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1738) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1739), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1739) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1740) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1741), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1741) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1742), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1742) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1749), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1749) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1751) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1751), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1752) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1752), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1753) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1753), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1753) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1754), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1754) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1755), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1755) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1756), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1756) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1757), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1757) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1758), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1758) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1759), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1759) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1759), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1760) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1760), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1761) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1761), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1761) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1762), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1762) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1763), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1763) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1764), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1764) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1765), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1765) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1766), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1766) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1767), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1767) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1768), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1768) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1768), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1769) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1769), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1770) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1770), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1771) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1771), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1772) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1772), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1772) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1773), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1774) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1779), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1780), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1781), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1781) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1782), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1782) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1783), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1783) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1784), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1784) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1785), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1785) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1786), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1786) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1787), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1787) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1788), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1788) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1789), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1789) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1789), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1790) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1790), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1791) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1791), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1791) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1792), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1792) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1793), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1793) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1794), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1794) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1795), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1795) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1796), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1796) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1797), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1797) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1798), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1798) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1798), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1799) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1799), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1800), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1801) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1801), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1802) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1802), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1803) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1803), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1803) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1804), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1805) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1810), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1810) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1811), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1812), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1812) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1813), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1813) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1814), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1814) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1815), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1815) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1816), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1816) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1817), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1817) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1818), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1818) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1819), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1819) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1820), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1820) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1820), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1821) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1821), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1822) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1822), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1822) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1823), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1823) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1824), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1824) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1825), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1825) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1826), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1826) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1827), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1827) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1828), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1828) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1829), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1829) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1830) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1830), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1831) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1831), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1832) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1832), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1833) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1833), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1834) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1834), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1835) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1835), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1836) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1836), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1837) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1837), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1838) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1839), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1839) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1839), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1841), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1841) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1842), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1842) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1843), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1843) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1843), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1844) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1844), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1845) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1845), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1846) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1846), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1847) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1847), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1847) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1848), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1848) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1849), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1849) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1850), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1850) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1851), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1851) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1852), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1852) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1853), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1853) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1854), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1854) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1854), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1855) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1855), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1856) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1856), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1856) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1857), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1857) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1858), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1858) });

            migrationBuilder.UpdateData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1859), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(1859) });

            migrationBuilder.UpdateData(
                table: "SalesChannel",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(6198), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(6198) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(8866), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(8867) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(8964), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(8964) });

            migrationBuilder.UpdateData(
                table: "TaxClass",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(8965), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(8965) });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(4382), new DateTime(2025, 5, 4, 13, 36, 14, 902, DateTimeKind.Utc).AddTicks(4382) });
        }
    }
}
