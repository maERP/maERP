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
                name: "ai_model",
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
                    table.PrimaryKey("PK_ai_model", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "country",
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
                    table.PrimaryKey("PK_country", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer",
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
                    table.PrimaryKey("PK_customer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manufacturer",
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
                    table.PrimaryKey("PK_manufacturer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
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
                    table.PrimaryKey("PK_role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "saleschannel",
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
                    table.PrimaryKey("PK_saleschannel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "setting",
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
                    table.PrimaryKey("PK_setting", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shipping",
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
                    table.PrimaryKey("PK_shipping", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shipping_provider",
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
                    table.PrimaryKey("PK_shipping_provider", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tax_class",
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
                    table.PrimaryKey("PK_tax_class", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tenant",
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
                    table.PrimaryKey("PK_tenant", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "warehouse",
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
                    table.PrimaryKey("PK_warehouse", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ai_prompt",
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
                    table.PrimaryKey("PK_ai_prompt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ai_prompt_ai_model_AiModelId",
                        column: x => x.AiModelId,
                        principalTable: "ai_model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_address",
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
                    table.PrimaryKey("PK_customer_address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customer_address_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customer_address_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer_saleschannel",
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
                    table.PrimaryKey("PK_customer_saleschannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customer_saleschannel_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order",
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
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role_claim",
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
                    table.PrimaryKey("PK_role_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_claim_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "shipping_provider_rate",
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
                    table.PrimaryKey("PK_shipping_provider_rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipping_provider_rate_shipping_provider_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "shipping_provider",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product",
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
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "manufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_tax_class_TaxClassId",
                        column: x => x.TaxClassId,
                        principalTable: "tax_class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
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
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_tenant_DefaultTenantId",
                        column: x => x.DefaultTenantId,
                        principalTable: "tenant",
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
                        name: "FK_SalesChannelWarehouses_saleschannel_SalesChannelsId",
                        column: x => x.SalesChannelsId,
                        principalTable: "saleschannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesChannelWarehouses_warehouse_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice",
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
                    table.PrimaryKey("PK_invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoice_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoice_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_history",
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
                    table.PrimaryKey("PK_order_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_history_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_item",
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
                    table.PrimaryKey("PK_order_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "goods_receipt",
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
                    table.PrimaryKey("PK_goods_receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_goods_receipt_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_goods_receipt_warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_saleschannel",
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
                    table.PrimaryKey("PK_product_saleschannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_saleschannel_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_saleschannel_saleschannel_SalesChannelId",
                        column: x => x.SalesChannelId,
                        principalTable: "saleschannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_stock",
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
                    table.PrimaryKey("PK_product_stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_stock_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_stock_warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_claim",
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
                    table.PrimaryKey("PK_user_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claim_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_login",
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
                    table.PrimaryKey("PK_user_login", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_user_login_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_role_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_tenant",
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
                    table.PrimaryKey("PK_user_tenant", x => new { x.UserId, x.TenantId });
                    table.ForeignKey(
                        name: "FK_user_tenant_tenant_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tenant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_tenant_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_token",
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
                    table.PrimaryKey("PK_user_token", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_user_token_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice_item",
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
                    table.PrimaryKey("PK_invoice_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_invoice_item_invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_invoice_item_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_item_serialnumber",
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
                    table.PrimaryKey("PK_order_item_serialnumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_serialnumber_order_item_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "order_item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8660), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8660), "Germany", null },
                    { 2, "AT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Austria", null },
                    { 3, "CH", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Switzerland", null },
                    { 4, "AD", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Andorra", null },
                    { 5, "AF", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Afghanistan", null },
                    { 6, "AG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Antigua and Barbuda", null },
                    { 7, "AL", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Albania", null },
                    { 8, "AM", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Armenia", null },
                    { 9, "AO", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Angola", null },
                    { 10, "AX", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Åland Islands", null },
                    { 11, "AR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Argentina", null },
                    { 12, "AT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), "Antarctica", null },
                    { 13, "AU", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8970), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Australia", null },
                    { 14, "AZ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Azerbaijan", null },
                    { 15, "BA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Bosnia and Herzegovina", null },
                    { 16, "BB", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Barbados", null },
                    { 17, "BE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Belgium", null },
                    { 18, "BG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Bulgaria", null },
                    { 19, "BL", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Saint Barthélemy", null },
                    { 20, "BO", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Bolivia", null },
                    { 21, "BR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Brazil", null },
                    { 22, "BS", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Bahamas", null },
                    { 23, "BY", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Belarus", null },
                    { 24, "BZ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Belize", null },
                    { 25, "CA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Canada", null },
                    { 26, "CH", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Cocos (Keeling) Islands", null },
                    { 27, "CI", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Ivory Coast", null },
                    { 28, "CL", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Chile", null },
                    { 29, "CN", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "China", null },
                    { 30, "CO", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Colombia", null },
                    { 31, "CR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8980), "Costa Rica", null },
                    { 32, "CU", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Cuba", null },
                    { 33, "CY", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Cyprus", null },
                    { 34, "CZ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Czech Republic", null },
                    { 35, "DO", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Dominican Republic", null },
                    { 36, "DK", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Denmark", null },
                    { 37, "DZ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Algeria", null },
                    { 38, "EC", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Ecuador", null },
                    { 39, "EE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Estonia", null },
                    { 40, "EG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Egypt", null },
                    { 41, "ER", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Eritrea", null },
                    { 42, "ES", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Spain", null },
                    { 43, "ET", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Ethiopia", null },
                    { 44, "FI", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Finland", null },
                    { 45, "FR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "France", null },
                    { 46, "GB", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "United Kingdom", null },
                    { 47, "GE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "Georgia", null },
                    { 48, "GF", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(8990), "French Guiana", null },
                    { 49, "GH", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Ghana", null },
                    { 50, "GL", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Greenland", null },
                    { 51, "GP", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Guadeloupe", null },
                    { 52, "GR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Greece", null },
                    { 53, "GT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Guatemala", null },
                    { 54, "GY", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Guyana", null },
                    { 55, "HN", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Honduras", null },
                    { 56, "HR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Croatia", null },
                    { 57, "HT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Haiti", null },
                    { 58, "HU", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Hungary", null },
                    { 59, "ID", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Indonesia", null },
                    { 60, "IE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Ireland", null },
                    { 61, "IN", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "India", null },
                    { 62, "IR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Iran", null },
                    { 63, "IS", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Iceland", null },
                    { 64, "IT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Italy", null },
                    { 65, "JM", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Jamaica", null },
                    { 66, "JP", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), "Japan", null },
                    { 67, "KE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9000), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Kenya", null },
                    { 68, "KG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Kyrgyzstan", null },
                    { 69, "KR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "South Korea", null },
                    { 70, "KW", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Kuwait", null },
                    { 71, "KZ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Kazakhstan", null },
                    { 72, "LU", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Luxembourg", null },
                    { 73, "LT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Lithuania", null },
                    { 74, "LV", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Latvia", null },
                    { 75, "MA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Morocco", null },
                    { 76, "MC", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Monaco", null },
                    { 77, "MD", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Moldova", null },
                    { 78, "MF", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Saint Martin", null },
                    { 79, "MG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Madagascar", null },
                    { 80, "MQ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Martinique", null },
                    { 81, "MT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Malta", null },
                    { 82, "MX", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Mexico", null },
                    { 83, "MY", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Malaysia", null },
                    { 84, "NG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Nigeria", null },
                    { 85, "NI", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9010), "Nicaragua", null },
                    { 86, "NL", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Netherlands", null },
                    { 87, "NO", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Norway", null },
                    { 88, "NZ", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "New Zealand", null },
                    { 89, "OM", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Oman", null },
                    { 90, "PA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Panama", null },
                    { 91, "PE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Peru", null },
                    { 92, "PL", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Poland", null },
                    { 93, "PM", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Saint Pierre and Miquelon", null },
                    { 94, "PR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Puerto Rico", null },
                    { 95, "PT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Portugal", null },
                    { 96, "PY", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Paraguay", null },
                    { 97, "QA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Qatar", null },
                    { 98, "RO", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Romania", null },
                    { 99, "RS", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Serbia", null },
                    { 100, "RU", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Russia", null },
                    { 101, "SA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Saudi Arabia", null },
                    { 102, "SE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Sweden", null },
                    { 103, "SG", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9020), "Singapore", null },
                    { 104, "SI", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Slovenia", null },
                    { 105, "SK", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Slovakia", null },
                    { 106, "SN", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Senegal", null },
                    { 107, "SR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Suriname", null },
                    { 108, "SV", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "El Salvador", null },
                    { 109, "TR", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Turkey", null },
                    { 110, "TT", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Trinidad and Tobago", null },
                    { 111, "UA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Ukraine", null },
                    { 112, "US", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "United States", null },
                    { 113, "UY", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Uruguay", null },
                    { 114, "VE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Venezuela", null },
                    { 115, "VI", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Virgin Islands", null },
                    { 116, "VN", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Vietnam", null },
                    { 117, "YE", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Yemen", null },
                    { 118, "ZA", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "South Africa", null },
                    { 119, "ZM", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Zambia", null },
                    { 120, "ZW", new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), new DateTime(2025, 8, 23, 9, 26, 20, 939, DateTimeKind.Utc).AddTicks(9030), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { 1, "Berlin", "Deutschland", new DateTime(2025, 8, 23, 9, 26, 20, 940, DateTimeKind.Utc).AddTicks(5540), new DateTime(2025, 8, 23, 9, 26, 20, 940, DateTimeKind.Utc).AddTicks(5540), "info@beispiel-hersteller.de", "", "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", null, "https://www.beispiel-hersteller.de", "10115" });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abc43a7e-f7bb-4447-baaf-1add431ddbdf", null, "Superadmin", "SUPERADMIN" },
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", null, "Employee", "EMPLOYEE" },
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "saleschannel",
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "TenantId", "Type", "Url", "Username" },
                values: new object[] { 1, new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(5010), new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(5010), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", null, 1, "", "" });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(6950), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(6950), "Company.Name", null, "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7150), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7150), "Company.Address", null, "Musterstraße 123" },
                    { 3, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.ZipCity", null, "12345 Musterstadt" },
                    { 4, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.Country", null, "Deutschland" },
                    { 5, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.Phone", null, "+49 123 456789" },
                    { 6, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.Email", null, "info@musterfirma.de" },
                    { 7, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.Website", null, "www.musterfirma.de" },
                    { 8, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.TaxId", null, "123/456/7890" },
                    { 9, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.VatId", null, "DE123456789" },
                    { 10, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.BankName", null, "Musterbank" },
                    { 11, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.Iban", null, "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.Bic", null, "MUSTDEXXX" },
                    { 13, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Company.LogoPath", null, "" },
                    { 14, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Jwt.Key", null, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Jwt.Issuer", null, "maERP.Server" },
                    { 16, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7190), "Jwt.Audience", null, "maERP.Client" },
                    { 17, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Jwt.DurationInMinutes", null, "60" },
                    { 18, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Jwt.RefreshTokenExpireDays", null, "7" },
                    { 19, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Email.ApiKey", null, "Sendgrid-Key" },
                    { 20, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Email.FromAddress", null, "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Email.FromName", null, "maERP" },
                    { 22, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Telemetry.Endpoint", null, "http://localhost:4317" },
                    { 23, new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), new DateTime(2025, 8, 23, 9, 26, 20, 944, DateTimeKind.Utc).AddTicks(7200), "Telemetry.ServiceName", null, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "tax_class",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(7620), new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(7620), 19.0, null },
                    { 2, new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(7720), 7.0, null },
                    { 3, new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(7720), new DateTime(2025, 8, 23, 9, 26, 20, 941, DateTimeKind.Utc).AddTicks(7720), 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { 1, "admin@example.com", new DateTime(2025, 8, 23, 9, 26, 20, 935, DateTimeKind.Utc).AddTicks(3840), new DateTime(2025, 8, 23, 9, 26, 20, 935, DateTimeKind.Utc).AddTicks(3840), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "DefaultTenantId", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "99113260-e50f-4f31-9cc6-79ec9627dd18", new DateTime(2025, 8, 23, 9, 26, 20, 864, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 8, 23, 9, 26, 20, 864, DateTimeKind.Utc).AddTicks(2500), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEAP7fJmz23RZQCmZywhij4la5c6M8YtA5g2/zzzayCXoHo/d94Ima2MbZ404nbfBWA==", null, false, "dec733e9-e2c5-45ab-abf8-6352e15c16cd", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "9995caab-15e9-4e42-b2f6-2bf4dee350dc", new DateTime(2025, 8, 23, 9, 26, 20, 900, DateTimeKind.Utc).AddTicks(6710), new DateTime(2025, 8, 23, 9, 26, 20, 900, DateTimeKind.Utc).AddTicks(6710), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAECfHW+tg72+zAWVW+jWKMX3QiQ/HTAYqVhvjxZST+S4eQT/H7Lp1nizoyrl5CFJtKA==", null, false, "fc1a9784-ad0f-4d29-bbab-0f597c324134", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { 1, new DateTime(2025, 8, 23, 9, 26, 20, 940, DateTimeKind.Utc).AddTicks(380), new DateTime(2025, 8, 23, 9, 26, 20, 940, DateTimeKind.Utc).AddTicks(380), "Hauptlager", null });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "9e224968-33e4-4652-b7b7-8574d048cdb9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ai_prompt_AiModelId",
                table: "ai_prompt",
                column: "AiModelId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_CountryId",
                table: "customer_address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_CustomerId",
                table: "customer_address",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_saleschannel_CustomerId",
                table: "customer_saleschannel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_goods_receipt_DateCreated",
                table: "goods_receipt",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_goods_receipt_ProductId",
                table: "goods_receipt",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_goods_receipt_ReceiptDate",
                table: "goods_receipt",
                column: "ReceiptDate");

            migrationBuilder.CreateIndex(
                name: "IX_goods_receipt_WarehouseId",
                table: "goods_receipt",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_CustomerId",
                table: "invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_OrderId",
                table: "invoice",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_item_InvoiceId",
                table: "invoice_item",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_item_ProductId",
                table: "invoice_item",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_order_CustomerId",
                table: "order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_order_history_OrderId",
                table: "order_history",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_OrderId",
                table: "order_item",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_serialnumber_OrderItemId",
                table: "order_item_serialnumber",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_product_ManufacturerId",
                table: "product",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_product_Sku",
                table: "product",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_TaxClassId",
                table: "product",
                column: "TaxClassId");

            migrationBuilder.CreateIndex(
                name: "IX_product_saleschannel_ProductId",
                table: "product_saleschannel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_product_saleschannel_SalesChannelId",
                table: "product_saleschannel",
                column: "SalesChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_product_stock_ProductId",
                table: "product_stock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_product_stock_WarehouseId",
                table: "product_stock",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_claim_RoleId",
                table: "role_claim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesChannelWarehouses_WarehousesId",
                table: "SalesChannelWarehouses",
                column: "WarehousesId");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_provider_rate_ShippingProviderId",
                table: "shipping_provider_rate",
                column: "ShippingProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_tenant_TenantCode",
                table: "tenant",
                column: "TenantCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "user",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_user_DefaultTenantId",
                table: "user",
                column: "DefaultTenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "user",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_claim_UserId",
                table: "user_claim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_login_UserId",
                table: "user_login",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_RoleId",
                table: "user_role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_tenant_TenantId",
                table: "user_tenant",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ai_prompt");

            migrationBuilder.DropTable(
                name: "customer_address");

            migrationBuilder.DropTable(
                name: "customer_saleschannel");

            migrationBuilder.DropTable(
                name: "goods_receipt");

            migrationBuilder.DropTable(
                name: "invoice_item");

            migrationBuilder.DropTable(
                name: "order_history");

            migrationBuilder.DropTable(
                name: "order_item_serialnumber");

            migrationBuilder.DropTable(
                name: "product_saleschannel");

            migrationBuilder.DropTable(
                name: "product_stock");

            migrationBuilder.DropTable(
                name: "role_claim");

            migrationBuilder.DropTable(
                name: "SalesChannelWarehouses");

            migrationBuilder.DropTable(
                name: "setting");

            migrationBuilder.DropTable(
                name: "shipping");

            migrationBuilder.DropTable(
                name: "shipping_provider_rate");

            migrationBuilder.DropTable(
                name: "user_claim");

            migrationBuilder.DropTable(
                name: "user_login");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "user_tenant");

            migrationBuilder.DropTable(
                name: "user_token");

            migrationBuilder.DropTable(
                name: "ai_model");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "invoice");

            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "saleschannel");

            migrationBuilder.DropTable(
                name: "warehouse");

            migrationBuilder.DropTable(
                name: "shipping_provider");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropTable(
                name: "tax_class");

            migrationBuilder.DropTable(
                name: "tenant");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
