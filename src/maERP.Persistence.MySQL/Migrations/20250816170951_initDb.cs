using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AiModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AiModelType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiUsername = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NCtx = table.Column<uint>(type: "int unsigned", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VatNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Note = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerStatus = table.Column<int>(type: "int", nullable: false),
                    DateEnrollment = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImportProducts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ImportCustomers = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ImportOrders = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExportProducts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExportCustomers = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExportOrders = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InitialProductImportCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InitialProductExportCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: false),
                    TrackingNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingCost = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingTaxRate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingProviderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShippingProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProvider", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaxClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxRate = table.Column<double>(type: "double", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClass", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenantCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ContactEmail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AiPrompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AiModelId = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PromptText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiPrompt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiPrompt_AiModel_AiModelId",
                        column: x => x.AiModelId,
                        principalTable: "AiModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseNr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Zip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultDeliveryAddress = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DefaultInvoiceAddress = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSalesChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSalesChannel_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentProvider = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentTransactionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerNote = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InternalNote = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressStreet = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliverAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressStreet = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderConfirmationSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InvoiceSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ShippingInformationSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShippingProviderRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxLength = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProviderRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingProviderRate_ShippingProvider_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProvider",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sku = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameOptimized = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ean = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Asin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionOptimized = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UseOptimized = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Msrp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Depth = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TaxClassId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_TaxClass_TaxClassId",
                        column: x => x.TaxClassId,
                        principalTable: "TaxClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DefaultTenantId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Tenant_DefaultTenantId",
                        column: x => x.DefaultTenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesChannelWarehouses",
                columns: table => new
                {
                    SalesChannelsId = table.Column<int>(type: "int", nullable: false),
                    WarehousesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannelWarehouses", x => new { x.SalesChannelsId, x.WarehousesId });
                    table.ForeignKey(
                        name: "FK_SalesChannelWarehouses_SalesChannel_SalesChannelsId",
                        column: x => x.SalesChannelsId,
                        principalTable: "SalesChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesChannelWarehouses_Warehouse_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    InvoiceStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentTransactionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressStreet = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressStreet = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusOld = table.Column<int>(type: "int", nullable: true),
                    OrderStatusNew = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusOld = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusNew = table.Column<int>(type: "int", nullable: true),
                    ShippingStatusOld = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingStatusNew = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSystemGenerated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistory_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "double", nullable: false),
                    MissingProductSku = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MissingProductEan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceipts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceipts_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RemoteProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RepricingType = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<double>(type: "double", nullable: false),
                    StockMin = table.Column<double>(type: "double", nullable: false),
                    StockMax = table.Column<double>(type: "double", nullable: false),
                    StorageLocation = table.Column<double>(type: "double", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTenant",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTenant", x => new { x.UserId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_UserTenant_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTenant_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EAN = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "double", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItemSerialNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemSerialNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemSerialNumber_OrderItem_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", null, "Employee", "EMPLOYEE" },
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "DefaultTenantId", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "e9243efb-278c-4836-a8b8-2ad670634522", new DateTime(2025, 8, 16, 17, 9, 50, 831, DateTimeKind.Utc).AddTicks(2800), new DateTime(2025, 8, 16, 17, 9, 50, 831, DateTimeKind.Utc).AddTicks(2800), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEJF84uiXOTwK1OLN27yaia4iGVRmGOucJ7K4+3Z9LE1e3HGf3jFxBqXF7IZOyIz/Qg==", null, false, "23da40ff-7cd2-426f-aef5-df52ad60fddf", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "a6227b34-3a27-4593-b340-4de6a6cde151", new DateTime(2025, 8, 16, 17, 9, 50, 866, DateTimeKind.Utc).AddTicks(9540), new DateTime(2025, 8, 16, 17, 9, 50, 866, DateTimeKind.Utc).AddTicks(9540), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEE5WUbDMsWJjss4MPh6nO7dNpdRfEJS6lgeMEbDB8PXwTbn8j+KFDr2RLlJq/PXIaw==", null, false, "23ad3746-ccc4-4d3a-9ff3-865255fb82ed", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9270), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9270), "Germany", null },
                    { 2, "AT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Austria", null },
                    { 3, "CH", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Switzerland", null },
                    { 4, "AD", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Andorra", null },
                    { 5, "AF", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Afghanistan", null },
                    { 6, "AG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Antigua and Barbuda", null },
                    { 7, "AL", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Albania", null },
                    { 8, "AM", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Armenia", null },
                    { 9, "AO", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Angola", null },
                    { 10, "AX", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Åland Islands", null },
                    { 11, "AR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Argentina", null },
                    { 12, "AT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Antarctica", null },
                    { 13, "AU", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Australia", null },
                    { 14, "AZ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Azerbaijan", null },
                    { 15, "BA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Bosnia and Herzegovina", null },
                    { 16, "BB", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Barbados", null },
                    { 17, "BE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9570), "Belgium", null },
                    { 18, "BG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Bulgaria", null },
                    { 19, "BL", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Saint Barthélemy", null },
                    { 20, "BO", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Bolivia", null },
                    { 21, "BR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Brazil", null },
                    { 22, "BS", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Bahamas", null },
                    { 23, "BY", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Belarus", null },
                    { 24, "BZ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Belize", null },
                    { 25, "CA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Canada", null },
                    { 26, "CH", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Cocos (Keeling) Islands", null },
                    { 27, "CI", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Ivory Coast", null },
                    { 28, "CL", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Chile", null },
                    { 29, "CN", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "China", null },
                    { 30, "CO", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Colombia", null },
                    { 31, "CR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Costa Rica", null },
                    { 32, "CU", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Cuba", null },
                    { 33, "CY", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Cyprus", null },
                    { 34, "CZ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Czech Republic", null },
                    { 35, "DO", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Dominican Republic", null },
                    { 36, "DK", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Denmark", null },
                    { 37, "DZ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9580), "Algeria", null },
                    { 38, "EC", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Ecuador", null },
                    { 39, "EE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Estonia", null },
                    { 40, "EG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Egypt", null },
                    { 41, "ER", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Eritrea", null },
                    { 42, "ES", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Spain", null },
                    { 43, "ET", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Ethiopia", null },
                    { 44, "FI", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Finland", null },
                    { 45, "FR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "France", null },
                    { 46, "GB", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "United Kingdom", null },
                    { 47, "GE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Georgia", null },
                    { 48, "GF", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "French Guiana", null },
                    { 49, "GH", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Ghana", null },
                    { 50, "GL", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9590), "Greenland", null },
                    { 51, "GP", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Guadeloupe", null },
                    { 52, "GR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Greece", null },
                    { 53, "GT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Guatemala", null },
                    { 54, "GY", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Guyana", null },
                    { 55, "HN", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Honduras", null },
                    { 56, "HR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Croatia", null },
                    { 57, "HT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Haiti", null },
                    { 58, "HU", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Hungary", null },
                    { 59, "ID", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Indonesia", null },
                    { 60, "IE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Ireland", null },
                    { 61, "IN", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "India", null },
                    { 62, "IR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Iran", null },
                    { 63, "IS", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Iceland", null },
                    { 64, "IT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Italy", null },
                    { 65, "JM", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Jamaica", null },
                    { 66, "JP", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Japan", null },
                    { 67, "KE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Kenya", null },
                    { 68, "KG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Kyrgyzstan", null },
                    { 69, "KR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "South Korea", null },
                    { 70, "KW", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Kuwait", null },
                    { 71, "KZ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9600), "Kazakhstan", null },
                    { 72, "LU", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Luxembourg", null },
                    { 73, "LT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Lithuania", null },
                    { 74, "LV", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Latvia", null },
                    { 75, "MA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Morocco", null },
                    { 76, "MC", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Monaco", null },
                    { 77, "MD", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Moldova", null },
                    { 78, "MF", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Saint Martin", null },
                    { 79, "MG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Madagascar", null },
                    { 80, "MQ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Martinique", null },
                    { 81, "MT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Malta", null },
                    { 82, "MX", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Mexico", null },
                    { 83, "MY", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Malaysia", null },
                    { 84, "NG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Nigeria", null },
                    { 85, "NI", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Nicaragua", null },
                    { 86, "NL", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Netherlands", null },
                    { 87, "NO", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Norway", null },
                    { 88, "NZ", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "New Zealand", null },
                    { 89, "OM", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Oman", null },
                    { 90, "PA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Panama", null },
                    { 91, "PE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), "Peru", null },
                    { 92, "PL", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9610), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Poland", null },
                    { 93, "PM", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Saint Pierre and Miquelon", null },
                    { 94, "PR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Puerto Rico", null },
                    { 95, "PT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Portugal", null },
                    { 96, "PY", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Paraguay", null },
                    { 97, "QA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Qatar", null },
                    { 98, "RO", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Romania", null },
                    { 99, "RS", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Serbia", null },
                    { 100, "RU", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Russia", null },
                    { 101, "SA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Saudi Arabia", null },
                    { 102, "SE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Sweden", null },
                    { 103, "SG", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Singapore", null },
                    { 104, "SI", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Slovenia", null },
                    { 105, "SK", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Slovakia", null },
                    { 106, "SN", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Senegal", null },
                    { 107, "SR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Suriname", null },
                    { 108, "SV", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "El Salvador", null },
                    { 109, "TR", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Turkey", null },
                    { 110, "TT", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Trinidad and Tobago", null },
                    { 111, "UA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "Ukraine", null },
                    { 112, "US", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9620), "United States", null },
                    { 113, "UY", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Uruguay", null },
                    { 114, "VE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Venezuela", null },
                    { 115, "VI", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Virgin Islands", null },
                    { 116, "VN", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Vietnam", null },
                    { 117, "YE", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Yemen", null },
                    { 118, "ZA", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "South Africa", null },
                    { 119, "ZM", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Zambia", null },
                    { 120, "ZW", new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), new DateTime(2025, 8, 16, 17, 9, 50, 906, DateTimeKind.Utc).AddTicks(9630), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { 1, "Berlin", "Deutschland", new DateTime(2025, 8, 16, 17, 9, 50, 907, DateTimeKind.Utc).AddTicks(5080), new DateTime(2025, 8, 16, 17, 9, 50, 907, DateTimeKind.Utc).AddTicks(5080), "info@beispiel-hersteller.de", "", "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", null, "https://www.beispiel-hersteller.de", "10115" });

            migrationBuilder.InsertData(
                table: "SalesChannel",
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "TenantId", "Type", "Url", "Username" },
                values: new object[] { 1, new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(3730), new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(3730), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", null, 1, "", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2500), "Company.Name", null, "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Address", null, "Musterstraße 123" },
                    { 3, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.ZipCity", null, "12345 Musterstadt" },
                    { 4, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Country", null, "Deutschland" },
                    { 5, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Phone", null, "+49 123 456789" },
                    { 6, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Email", null, "info@musterfirma.de" },
                    { 7, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Website", null, "www.musterfirma.de" },
                    { 8, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.TaxId", null, "123/456/7890" },
                    { 9, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.VatId", null, "DE123456789" },
                    { 10, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.BankName", null, "Musterbank" },
                    { 11, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Iban", null, "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.Bic", null, "MUSTDEXXX" },
                    { 13, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Company.LogoPath", null, "" },
                    { 14, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Jwt.Key", null, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Jwt.Issuer", null, "maERP.Server" },
                    { 16, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Jwt.Audience", null, "maERP.Client" },
                    { 17, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Jwt.DurationInMinutes", null, "60" },
                    { 18, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Jwt.RefreshTokenExpireDays", null, "7" },
                    { 19, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2680), "Email.ApiKey", null, "Sendgrid-Key" },
                    { 20, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), "Email.FromAddress", null, "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), "Email.FromName", null, "maERP" },
                    { 22, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), "Telemetry.Endpoint", null, "http://localhost:4317" },
                    { 23, new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), new DateTime(2025, 8, 16, 17, 9, 50, 911, DateTimeKind.Utc).AddTicks(2690), "Telemetry.ServiceName", null, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(6060), new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(6060), 19.0, null },
                    { 2, new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(6150), new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(6150), 7.0, null },
                    { 3, new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(6150), new DateTime(2025, 8, 16, 17, 9, 50, 908, DateTimeKind.Utc).AddTicks(6150), 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { 1, "admin@example.com", new DateTime(2025, 8, 16, 17, 9, 50, 902, DateTimeKind.Utc).AddTicks(9460), new DateTime(2025, 8, 16, 17, 9, 50, 902, DateTimeKind.Utc).AddTicks(9470), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { 1, new DateTime(2025, 8, 16, 17, 9, 50, 907, DateTimeKind.Utc).AddTicks(820), new DateTime(2025, 8, 16, 17, 9, 50, 907, DateTimeKind.Utc).AddTicks(820), "Hauptlager", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "9e224968-33e4-4652-b7b7-8574d048cdb9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiPrompt_AiModelId",
                table: "AiPrompt",
                column: "AiModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DefaultTenantId",
                table: "AspNetUsers",
                column: "DefaultTenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CountryId",
                table: "CustomerAddress",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_CustomerId",
                table: "CustomerAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSalesChannel_CustomerId",
                table: "CustomerSalesChannel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_DateCreated",
                table: "GoodsReceipts",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_ProductId",
                table: "GoodsReceipts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_ReceiptDate",
                table: "GoodsReceipts",
                column: "ReceiptDate");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_WarehouseId",
                table: "GoodsReceipts",
                column: "WarehouseId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSerialNumber_OrderItemId",
                table: "OrderItemSerialNumber",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManufacturerId",
                table: "Product",
                column: "ManufacturerId");

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
                name: "IX_SalesChannelWarehouses_WarehousesId",
                table: "SalesChannelWarehouses",
                column: "WarehousesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingProviderRate_ShippingProviderId",
                table: "ShippingProviderRate",
                column: "ShippingProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_TenantCode",
                table: "Tenant",
                column: "TenantCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTenant_TenantId",
                table: "UserTenant",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiPrompt");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "CustomerSalesChannel");

            migrationBuilder.DropTable(
                name: "GoodsReceipts");

            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "OrderItemSerialNumber");

            migrationBuilder.DropTable(
                name: "ProductSalesChannel");

            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.DropTable(
                name: "SalesChannelWarehouses");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "ShippingProviderRate");

            migrationBuilder.DropTable(
                name: "UserTenant");

            migrationBuilder.DropTable(
                name: "AiModel");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SalesChannel");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "ShippingProvider");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "TaxClass");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
