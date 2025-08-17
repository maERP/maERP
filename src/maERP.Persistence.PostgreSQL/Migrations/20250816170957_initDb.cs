using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AiModelType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ApiUrl = table.Column<string>(type: "text", nullable: false),
                    ApiUsername = table.Column<string>(type: "text", nullable: false),
                    ApiPassword = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    NCtx = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    DateEnrollment = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Website = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Logo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ImportProducts = table.Column<bool>(type: "boolean", nullable: false),
                    ImportCustomers = table.Column<bool>(type: "boolean", nullable: false),
                    ImportOrders = table.Column<bool>(type: "boolean", nullable: false),
                    ExportProducts = table.Column<bool>(type: "boolean", nullable: false),
                    ExportCustomers = table.Column<bool>(type: "boolean", nullable: false),
                    ExportOrders = table.Column<bool>(type: "boolean", nullable: false),
                    InitialProductImportCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    InitialProductExportCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TenantCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ContactEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AiPrompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AiModelId = table.Column<int>(type: "integer", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    PromptText = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                name: "CustomerSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    SalesChannelId = table.Column<int>(type: "integer", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    Subtotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentProvider = table.Column<string>(type: "text", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "text", nullable: false),
                    CustomerNote = table.Column<string>(type: "text", nullable: false),
                    InternalNote = table.Column<string>(type: "text", nullable: false),
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
                    OrderConfirmationSent = table.Column<bool>(type: "boolean", nullable: false),
                    InvoiceSent = table.Column<bool>(type: "boolean", nullable: false),
                    ShippingInformationSent = table.Column<bool>(type: "boolean", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    MaxLength = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWidth = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxHeight = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    ShippingProviderId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    NameOptimized = table.Column<string>(type: "text", nullable: false),
                    Ean = table.Column<string>(type: "text", nullable: false),
                    Asin = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DescriptionOptimized = table.Column<string>(type: "text", nullable: false),
                    UseOptimized = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Msrp = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Width = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Depth = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    TaxClassId = table.Column<int>(type: "integer", nullable: false),
                    ManufacturerId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DefaultTenantId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Tenant_DefaultTenantId",
                        column: x => x.DefaultTenantId,
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalesChannelWarehouses",
                columns: table => new
                {
                    SalesChannelsId = table.Column<int>(type: "integer", nullable: false),
                    WarehousesId = table.Column<int>(type: "integer", nullable: false)
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
                });

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
                    Subtotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
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
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatusOld = table.Column<int>(type: "integer", nullable: true),
                    OrderStatusNew = table.Column<int>(type: "integer", nullable: true),
                    PaymentStatusOld = table.Column<int>(type: "integer", nullable: true),
                    PaymentStatusNew = table.Column<int>(type: "integer", nullable: true),
                    ShippingStatusOld = table.Column<string>(type: "text", nullable: true),
                    ShippingStatusNew = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsSystemGenerated = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    MissingProductSku = table.Column<string>(type: "text", nullable: false),
                    MissingProductEan = table.Column<string>(type: "text", nullable: false),
                    ShippingId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    Supplier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RepricingType = table.Column<bool>(type: "boolean", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    Stock = table.Column<double>(type: "double precision", nullable: false),
                    StockMin = table.Column<double>(type: "double precision", nullable: false),
                    StockMax = table.Column<double>(type: "double precision", nullable: false),
                    StorageLocation = table.Column<double>(type: "double precision", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "UserTenant",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    OrderItemId = table.Column<int>(type: "integer", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "OrderItemSerialNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderItemId = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: true)
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
                });

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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "6cee1d19-d0a9-42ff-9ba2-370aa5893102", new DateTime(2025, 8, 16, 17, 9, 57, 246, DateTimeKind.Utc).AddTicks(5140), new DateTime(2025, 8, 16, 17, 9, 57, 246, DateTimeKind.Utc).AddTicks(5140), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEIqUj8KJi41J2AOY9EezVxwKbSm9xQHuGE+8KhNNZCz7S/xvGcKkgqRMmi+xUOoW1g==", null, false, "6e840f6f-10dc-4c75-930d-347d1af2613a", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "f012159a-c2aa-4655-977d-df3292c37279", new DateTime(2025, 8, 16, 17, 9, 57, 280, DateTimeKind.Utc).AddTicks(3930), new DateTime(2025, 8, 16, 17, 9, 57, 280, DateTimeKind.Utc).AddTicks(3930), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEICmzgQfFcjrKGv6/ixNzlbKD0yW4RJ9naTI0aNcxvqUdo4CgJh4g/aJyBtq7R5Izg==", null, false, "9f024b7e-0b8a-472e-a743-b8419e97acff", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1350), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1350), "Germany", null },
                    { 2, "AT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Austria", null },
                    { 3, "CH", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Switzerland", null },
                    { 4, "AD", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Andorra", null },
                    { 5, "AF", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Afghanistan", null },
                    { 6, "AG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Antigua and Barbuda", null },
                    { 7, "AL", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Albania", null },
                    { 8, "AM", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Armenia", null },
                    { 9, "AO", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Angola", null },
                    { 10, "AX", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Åland Islands", null },
                    { 11, "AR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Argentina", null },
                    { 12, "AT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Antarctica", null },
                    { 13, "AU", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Australia", null },
                    { 14, "AZ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Azerbaijan", null },
                    { 15, "BA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Bosnia and Herzegovina", null },
                    { 16, "BB", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Barbados", null },
                    { 17, "BE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1660), "Belgium", null },
                    { 18, "BG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Bulgaria", null },
                    { 19, "BL", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Saint Barthélemy", null },
                    { 20, "BO", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Bolivia", null },
                    { 21, "BR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Brazil", null },
                    { 22, "BS", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Bahamas", null },
                    { 23, "BY", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Belarus", null },
                    { 24, "BZ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Belize", null },
                    { 25, "CA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Canada", null },
                    { 26, "CH", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Cocos (Keeling) Islands", null },
                    { 27, "CI", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Ivory Coast", null },
                    { 28, "CL", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Chile", null },
                    { 29, "CN", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "China", null },
                    { 30, "CO", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Colombia", null },
                    { 31, "CR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Costa Rica", null },
                    { 32, "CU", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Cuba", null },
                    { 33, "CY", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Cyprus", null },
                    { 34, "CZ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Czech Republic", null },
                    { 35, "DO", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Dominican Republic", null },
                    { 36, "DK", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1670), "Denmark", null },
                    { 37, "DZ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Algeria", null },
                    { 38, "EC", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Ecuador", null },
                    { 39, "EE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Estonia", null },
                    { 40, "EG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Egypt", null },
                    { 41, "ER", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Eritrea", null },
                    { 42, "ES", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Spain", null },
                    { 43, "ET", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Ethiopia", null },
                    { 44, "FI", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Finland", null },
                    { 45, "FR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "France", null },
                    { 46, "GB", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "United Kingdom", null },
                    { 47, "GE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Georgia", null },
                    { 48, "GF", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "French Guiana", null },
                    { 49, "GH", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Ghana", null },
                    { 50, "GL", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Greenland", null },
                    { 51, "GP", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Guadeloupe", null },
                    { 52, "GR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Greece", null },
                    { 53, "GT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Guatemala", null },
                    { 54, "GY", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1680), "Guyana", null },
                    { 55, "HN", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Honduras", null },
                    { 56, "HR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Croatia", null },
                    { 57, "HT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Haiti", null },
                    { 58, "HU", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Hungary", null },
                    { 59, "ID", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Indonesia", null },
                    { 60, "IE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Ireland", null },
                    { 61, "IN", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "India", null },
                    { 62, "IR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Iran", null },
                    { 63, "IS", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Iceland", null },
                    { 64, "IT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Italy", null },
                    { 65, "JM", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Jamaica", null },
                    { 66, "JP", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Japan", null },
                    { 67, "KE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Kenya", null },
                    { 68, "KG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Kyrgyzstan", null },
                    { 69, "KR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "South Korea", null },
                    { 70, "KW", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Kuwait", null },
                    { 71, "KZ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Kazakhstan", null },
                    { 72, "LU", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1690), "Luxembourg", null },
                    { 73, "LT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Lithuania", null },
                    { 74, "LV", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Latvia", null },
                    { 75, "MA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Morocco", null },
                    { 76, "MC", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Monaco", null },
                    { 77, "MD", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Moldova", null },
                    { 78, "MF", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Saint Martin", null },
                    { 79, "MG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Madagascar", null },
                    { 80, "MQ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Martinique", null },
                    { 81, "MT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Malta", null },
                    { 82, "MX", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Mexico", null },
                    { 83, "MY", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Malaysia", null },
                    { 84, "NG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Nigeria", null },
                    { 85, "NI", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Nicaragua", null },
                    { 86, "NL", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Netherlands", null },
                    { 87, "NO", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Norway", null },
                    { 88, "NZ", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "New Zealand", null },
                    { 89, "OM", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Oman", null },
                    { 90, "PA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1700), "Panama", null },
                    { 91, "PE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Peru", null },
                    { 92, "PL", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Poland", null },
                    { 93, "PM", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Saint Pierre and Miquelon", null },
                    { 94, "PR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Puerto Rico", null },
                    { 95, "PT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Portugal", null },
                    { 96, "PY", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Paraguay", null },
                    { 97, "QA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Qatar", null },
                    { 98, "RO", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Romania", null },
                    { 99, "RS", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Serbia", null },
                    { 100, "RU", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Russia", null },
                    { 101, "SA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Saudi Arabia", null },
                    { 102, "SE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Sweden", null },
                    { 103, "SG", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Singapore", null },
                    { 104, "SI", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Slovenia", null },
                    { 105, "SK", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Slovakia", null },
                    { 106, "SN", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Senegal", null },
                    { 107, "SR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "Suriname", null },
                    { 108, "SV", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1710), "El Salvador", null },
                    { 109, "TR", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Turkey", null },
                    { 110, "TT", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Trinidad and Tobago", null },
                    { 111, "UA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Ukraine", null },
                    { 112, "US", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "United States", null },
                    { 113, "UY", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Uruguay", null },
                    { 114, "VE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Venezuela", null },
                    { 115, "VI", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Virgin Islands", null },
                    { 116, "VN", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Vietnam", null },
                    { 117, "YE", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Yemen", null },
                    { 118, "ZA", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "South Africa", null },
                    { 119, "ZM", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Zambia", null },
                    { 120, "ZW", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(1720), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { 1, "Berlin", "Deutschland", new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(7820), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(7820), "info@beispiel-hersteller.de", "", "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", null, "https://www.beispiel-hersteller.de", "10115" });

            migrationBuilder.InsertData(
                table: "SalesChannel",
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "TenantId", "Type", "Url", "Username" },
                values: new object[] { 1, new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(6940), new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(6940), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", null, 1, "", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7640), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7640), "Company.Name", null, "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.Address", null, "Musterstraße 123" },
                    { 3, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.ZipCity", null, "12345 Musterstadt" },
                    { 4, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.Country", null, "Deutschland" },
                    { 5, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.Phone", null, "+49 123 456789" },
                    { 6, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.Email", null, "info@musterfirma.de" },
                    { 7, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.Website", null, "www.musterfirma.de" },
                    { 8, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.TaxId", null, "123/456/7890" },
                    { 9, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7830), "Company.VatId", null, "DE123456789" },
                    { 10, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Company.BankName", null, "Musterbank" },
                    { 11, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Company.Iban", null, "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Company.Bic", null, "MUSTDEXXX" },
                    { 13, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Company.LogoPath", null, "" },
                    { 14, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Jwt.Key", null, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Jwt.Issuer", null, "maERP.Server" },
                    { 16, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Jwt.Audience", null, "maERP.Client" },
                    { 17, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Jwt.DurationInMinutes", null, "60" },
                    { 18, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Jwt.RefreshTokenExpireDays", null, "7" },
                    { 19, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Email.ApiKey", null, "Sendgrid-Key" },
                    { 20, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Email.FromAddress", null, "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Email.FromName", null, "maERP" },
                    { 22, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Telemetry.Endpoint", null, "http://localhost:4317" },
                    { 23, new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 16, 17, 9, 57, 321, DateTimeKind.Utc).AddTicks(7840), "Telemetry.ServiceName", null, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(9490), 19.0, null },
                    { 2, new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(9590), 7.0, null },
                    { 3, new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(9590), new DateTime(2025, 8, 16, 17, 9, 57, 318, DateTimeKind.Utc).AddTicks(9590), 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { 1, "admin@example.com", new DateTime(2025, 8, 16, 17, 9, 57, 313, DateTimeKind.Utc).AddTicks(430), new DateTime(2025, 8, 16, 17, 9, 57, 313, DateTimeKind.Utc).AddTicks(430), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { 1, new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 8, 16, 17, 9, 57, 317, DateTimeKind.Utc).AddTicks(3090), "Hauptlager", null });

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
