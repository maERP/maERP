using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: false),
                    VatNumber = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    CustomerStatus = table.Column<int>(type: "integer", nullable: false),
                    DateEnrollment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Section = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "integer", nullable: false),
                    TrackingNumber = table.Column<string>(type: "text", nullable: false),
                    ShippingCost = table.Column<string>(type: "text", nullable: false),
                    ShippingTaxRate = table.Column<string>(type: "text", nullable: false),
                    ShippingProviderName = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNr = table.Column<string>(type: "text", nullable: false),
                    Zip = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    DefaultDeliveryAddress = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultInvoiceAddress = table.Column<bool>(type: "boolean", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAddress_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesChannelId = table.Column<int>(type: "integer", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DeliveryAddressFirstName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressLastName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressCompanyName = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressPhone = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "text", nullable: false),
                    DeliverAddressZip = table.Column<string>(type: "text", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressFirstName = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressLastName = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressCompanyName = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressPhone = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressStreet = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressCity = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressZip = table.Column<string>(type: "text", nullable: false),
                    InvoiceAddressCountry = table.Column<string>(type: "text", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProviderRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxLength = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxWidth = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxHeight = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProviderRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingProviderRate_ShippingProvider_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProvider",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ean = table.Column<string>(type: "text", nullable: false),
                    Asin = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Msrp = table.Column<decimal>(type: "numeric", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    Width = table.Column<decimal>(type: "numeric", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Depth = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxClassId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_TaxClass_TaxClassId",
                        column: x => x.TaxClassId,
                        principalTable: "TaxClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ImportProducts = table.Column<bool>(type: "boolean", nullable: false),
                    ImportCustomers = table.Column<bool>(type: "boolean", nullable: false),
                    ImportOrders = table.Column<bool>(type: "boolean", nullable: false),
                    ExportProducts = table.Column<bool>(type: "boolean", nullable: false),
                    ExportCustomers = table.Column<bool>(type: "boolean", nullable: false),
                    ExportOrders = table.Column<bool>(type: "boolean", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesChannel_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    ShippingId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStock_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStock_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesChannelId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    RemoteProductId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSalesChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSalesChannel_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSalesChannel_SalesChannel_SalesChannelId",
                        column: x => x.SalesChannelId,
                        principalTable: "SalesChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2406), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2409), "Germany" },
                    { 2, "AT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2413), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2413), "Austria" },
                    { 3, "CH", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2414), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2414), "Switzerland" },
                    { 4, "AD", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2414), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2415), "Andorra" },
                    { 5, "AF", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2415), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2415), "Afghanistan" },
                    { 6, "AG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2416), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2416), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2417), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2417), "Albania" },
                    { 8, "AM", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2418), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2418), "Armenia" },
                    { 9, "AO", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2418), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2418), "Angola" },
                    { 10, "AX", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2419), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2419), "Åland Islands" },
                    { 11, "AR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2420), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2420), "Argentina" },
                    { 12, "AT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2421), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2421), "Antarctica" },
                    { 13, "AU", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2421), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2421), "Australia" },
                    { 14, "AZ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2422), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2422), "Azerbaijan" },
                    { 15, "BA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2423), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2423), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2424), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2424), "Barbados" },
                    { 17, "BE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2424), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2425), "Belgium" },
                    { 18, "BG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2425), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2425), "Bulgaria" },
                    { 19, "BL", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2426), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2426), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2427), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2427), "Bolivia" },
                    { 21, "BR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2427), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2428), "Brazil" },
                    { 22, "BS", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2428), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2428), "Bahamas" },
                    { 23, "BY", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2429), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2429), "Belarus" },
                    { 24, "BZ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2430), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2430), "Belize" },
                    { 25, "CA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2430), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2431), "Canada" },
                    { 26, "CH", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2431), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2431), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2432), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2432), "Ivory Coast" },
                    { 28, "CL", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2433), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2433), "Chile" },
                    { 29, "CN", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2433), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2434), "China" },
                    { 30, "CO", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2434), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2434), "Colombia" },
                    { 31, "CR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2435), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2435), "Costa Rica" },
                    { 32, "CU", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2435), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2436), "Cuba" },
                    { 33, "CY", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2436), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2437), "Cyprus" },
                    { 34, "CZ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2437), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2437), "Czech Republic" },
                    { 35, "DO", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2438), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2438), "Dominican Republic" },
                    { 36, "DK", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2439), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2439), "Denmark" },
                    { 37, "DZ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2439), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2440), "Algeria" },
                    { 38, "EC", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2440), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2440), "Ecuador" },
                    { 39, "EE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2441), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2441), "Estonia" },
                    { 40, "EG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2442), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2442), "Egypt" },
                    { 41, "ER", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2442), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2443), "Eritrea" },
                    { 42, "ES", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2443), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2443), "Spain" },
                    { 43, "ET", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2444), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2444), "Ethiopia" },
                    { 44, "FI", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2445), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2445), "Finland" },
                    { 45, "FR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2445), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2446), "France" },
                    { 46, "GB", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2446), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2446), "United Kingdom" },
                    { 47, "GE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2447), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2447), "Georgia" },
                    { 48, "GF", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2448), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2448), "French Guiana" },
                    { 49, "GH", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2448), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2449), "Ghana" },
                    { 50, "GL", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2449), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2449), "Greenland" },
                    { 51, "GP", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2450), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2450), "Guadeloupe" },
                    { 52, "GR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2451), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2451), "Greece" },
                    { 53, "GT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2451), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2452), "Guatemala" },
                    { 54, "GY", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2452), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2452), "Guyana" },
                    { 55, "HN", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2453), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2453), "Honduras" },
                    { 56, "HR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2454), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2454), "Croatia" },
                    { 57, "HT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2455), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2455), "Haiti" },
                    { 58, "HU", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2456), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2456), "Hungary" },
                    { 59, "ID", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2456), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2457), "Indonesia" },
                    { 60, "IE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2457), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2457), "Ireland" },
                    { 61, "IN", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2458), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2458), "India" },
                    { 62, "IR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2459), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2459), "Iran" },
                    { 63, "IS", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2459), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2460), "Iceland" },
                    { 64, "IT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2460), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2460), "Italy" },
                    { 65, "JM", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2461), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2461), "Jamaica" },
                    { 66, "JP", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2462), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2462), "Japan" },
                    { 67, "KE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2462), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2463), "Kenya" },
                    { 68, "KG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2463), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2463), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2464), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2464), "South Korea" },
                    { 70, "KW", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2465), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2465), "Kuwait" },
                    { 71, "KZ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2465), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2466), "Kazakhstan" },
                    { 72, "LU", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2466), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2466), "Luxembourg" },
                    { 73, "LT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2467), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2467), "Lithuania" },
                    { 74, "LV", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2468), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2468), "Latvia" },
                    { 75, "MA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2468), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2469), "Morocco" },
                    { 76, "MC", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2469), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2470), "Monaco" },
                    { 77, "MD", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2470), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2470), "Moldova" },
                    { 78, "MF", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2471), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2471), "Saint Martin" },
                    { 79, "MG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2471), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2472), "Madagascar" },
                    { 80, "MQ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2472), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2473), "Martinique" },
                    { 81, "MT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2473), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2473), "Malta" },
                    { 82, "MX", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2474), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2474), "Mexico" },
                    { 83, "MY", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2474), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2475), "Malaysia" },
                    { 84, "NG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2475), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2475), "Nigeria" },
                    { 85, "NI", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2476), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2476), "Nicaragua" },
                    { 86, "NL", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2477), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2477), "Netherlands" },
                    { 87, "NO", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2477), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2478), "Norway" },
                    { 88, "NZ", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2478), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2478), "New Zealand" },
                    { 89, "OM", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2479), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2479), "Oman" },
                    { 90, "PA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2480), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2480), "Panama" },
                    { 91, "PE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2480), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2481), "Peru" },
                    { 92, "PL", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2481), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2481), "Poland" },
                    { 93, "PM", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2482), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2482), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2483), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2483), "Puerto Rico" },
                    { 95, "PT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2483), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2484), "Portugal" },
                    { 96, "PY", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2484), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2484), "Paraguay" },
                    { 97, "QA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2485), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2485), "Qatar" },
                    { 98, "RO", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2486), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2486), "Romania" },
                    { 99, "RS", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2486), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2487), "Serbia" },
                    { 100, "RU", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2487), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2487), "Russia" },
                    { 101, "SA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2488), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2488), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2489), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2489), "Sweden" },
                    { 103, "SG", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2490), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2490), "Singapore" },
                    { 104, "SI", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2490), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2491), "Slovenia" },
                    { 105, "SK", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2491), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2491), "Slovakia" },
                    { 106, "SN", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2492), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2492), "Senegal" },
                    { 107, "SR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2493), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2493), "Suriname" },
                    { 108, "SV", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2526), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2526), "El Salvador" },
                    { 109, "TR", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2527), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2527), "Turkey" },
                    { 110, "TT", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2527), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2528), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2528), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2528), "Ukraine" },
                    { 112, "US", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2529), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2529), "United States" },
                    { 113, "UY", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2530), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2530), "Uruguay" },
                    { 114, "VE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2530), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2531), "Venezuela" },
                    { 115, "VI", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2531), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2531), "Virgin Islands" },
                    { 116, "VN", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2532), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2532), "Vietnam" },
                    { 117, "YE", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2533), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2533), "Yemen" },
                    { 118, "ZA", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2533), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2534), "South Africa" },
                    { 119, "ZM", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2534), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2534), "Zambia" },
                    { 120, "ZW", new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2535), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(2535), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(3085), new DateTime(2024, 5, 12, 12, 40, 7, 349, DateTimeKind.Utc).AddTicks(3086), "Testlager" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CountryId",
                table: "CustomerAddress",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Sku",
                table: "Product",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxClassId",
                table: "Product",
                column: "TaxClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSalesChannel_ProductId",
                table: "ProductSalesChannel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSalesChannel_SalesChannelId",
                table: "ProductSalesChannel",
                column: "SalesChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductId",
                table: "ProductStock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_WarehouseId",
                table: "ProductStock",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesChannel_WarehouseId",
                table: "SalesChannel",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProviderRate_ShippingProviderId",
                table: "ShippingProviderRate",
                column: "ShippingProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductSalesChannel");

            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "ShippingProviderRate");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SalesChannel");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShippingProvider");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "TaxClass");
        }
    }
}
