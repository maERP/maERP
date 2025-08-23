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
                name: "ai_model",
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
                    table.PrimaryKey("PK_ai_model", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "country",
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
                    table.PrimaryKey("PK_country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
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
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
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
                    table.PrimaryKey("PK_manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "saleschannel",
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
                    table.PrimaryKey("PK_saleschannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "setting",
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
                    table.PrimaryKey("PK_setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping",
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
                    table.PrimaryKey("PK_shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_provider",
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
                    table.PrimaryKey("PK_shipping_provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tax_class",
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
                    table.PrimaryKey("PK_tax_class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant",
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
                    table.PrimaryKey("PK_tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
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
                    table.PrimaryKey("PK_warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ai_prompt",
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
                    table.PrimaryKey("PK_ai_prompt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ai_prompt_ai_model_AiModelId",
                        column: x => x.AiModelId,
                        principalTable: "ai_model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_address",
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
                });

            migrationBuilder.CreateTable(
                name: "customer_saleschannel",
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
                    table.PrimaryKey("PK_customer_saleschannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_customer_saleschannel_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
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
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claim",
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
                    table.PrimaryKey("PK_role_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_claim_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipping_provider_rate",
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
                    table.PrimaryKey("PK_shipping_provider_rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipping_provider_rate_shipping_provider_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "shipping_provider",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "product",
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
                });

            migrationBuilder.CreateTable(
                name: "user",
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
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_tenant_DefaultTenantId",
                        column: x => x.DefaultTenantId,
                        principalTable: "tenant",
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
                });

            migrationBuilder.CreateTable(
                name: "invoice",
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
                });

            migrationBuilder.CreateTable(
                name: "order_history",
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
                    table.PrimaryKey("PK_order_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_history_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
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
                    table.PrimaryKey("PK_order_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goods_receipt",
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
                });

            migrationBuilder.CreateTable(
                name: "product_saleschannel",
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
                });

            migrationBuilder.CreateTable(
                name: "product_stock",
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
                });

            migrationBuilder.CreateTable(
                name: "user_claim",
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
                    table.PrimaryKey("PK_user_claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claim_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_login",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "user_tenant",
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
                });

            migrationBuilder.CreateTable(
                name: "user_token",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "invoice_item",
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
                });

            migrationBuilder.CreateTable(
                name: "order_item_serialnumber",
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
                    table.PrimaryKey("PK_order_item_serialnumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_item_serialnumber_order_item_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "order_item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(1950), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(1950), "Germany", null },
                    { 2, "AT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), "Austria", null },
                    { 3, "CH", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), "Switzerland", null },
                    { 4, "AD", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), "Andorra", null },
                    { 5, "AF", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), "Afghanistan", null },
                    { 6, "AG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), "Antigua and Barbuda", null },
                    { 7, "AL", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), "Albania", null },
                    { 8, "AM", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2250), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Armenia", null },
                    { 9, "AO", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Angola", null },
                    { 10, "AX", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Åland Islands", null },
                    { 11, "AR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Argentina", null },
                    { 12, "AT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Antarctica", null },
                    { 13, "AU", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Australia", null },
                    { 14, "AZ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Azerbaijan", null },
                    { 15, "BA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Bosnia and Herzegovina", null },
                    { 16, "BB", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Barbados", null },
                    { 17, "BE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Belgium", null },
                    { 18, "BG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Bulgaria", null },
                    { 19, "BL", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Saint Barthélemy", null },
                    { 20, "BO", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Bolivia", null },
                    { 21, "BR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Brazil", null },
                    { 22, "BS", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Bahamas", null },
                    { 23, "BY", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Belarus", null },
                    { 24, "BZ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), "Belize", null },
                    { 25, "CA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2260), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Canada", null },
                    { 26, "CH", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Cocos (Keeling) Islands", null },
                    { 27, "CI", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Ivory Coast", null },
                    { 28, "CL", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Chile", null },
                    { 29, "CN", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "China", null },
                    { 30, "CO", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Colombia", null },
                    { 31, "CR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Costa Rica", null },
                    { 32, "CU", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Cuba", null },
                    { 33, "CY", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Cyprus", null },
                    { 34, "CZ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Czech Republic", null },
                    { 35, "DO", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Dominican Republic", null },
                    { 36, "DK", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Denmark", null },
                    { 37, "DZ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Algeria", null },
                    { 38, "EC", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Ecuador", null },
                    { 39, "EE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Estonia", null },
                    { 40, "EG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Egypt", null },
                    { 41, "ER", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Eritrea", null },
                    { 42, "ES", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2270), "Spain", null },
                    { 43, "ET", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Ethiopia", null },
                    { 44, "FI", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Finland", null },
                    { 45, "FR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "France", null },
                    { 46, "GB", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "United Kingdom", null },
                    { 47, "GE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Georgia", null },
                    { 48, "GF", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "French Guiana", null },
                    { 49, "GH", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Ghana", null },
                    { 50, "GL", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Greenland", null },
                    { 51, "GP", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Guadeloupe", null },
                    { 52, "GR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Greece", null },
                    { 53, "GT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Guatemala", null },
                    { 54, "GY", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Guyana", null },
                    { 55, "HN", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Honduras", null },
                    { 56, "HR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Croatia", null },
                    { 57, "HT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Haiti", null },
                    { 58, "HU", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Hungary", null },
                    { 59, "ID", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), "Indonesia", null },
                    { 60, "IE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2280), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Ireland", null },
                    { 61, "IN", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "India", null },
                    { 62, "IR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Iran", null },
                    { 63, "IS", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Iceland", null },
                    { 64, "IT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Italy", null },
                    { 65, "JM", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Jamaica", null },
                    { 66, "JP", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Japan", null },
                    { 67, "KE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Kenya", null },
                    { 68, "KG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Kyrgyzstan", null },
                    { 69, "KR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "South Korea", null },
                    { 70, "KW", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Kuwait", null },
                    { 71, "KZ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Kazakhstan", null },
                    { 72, "LU", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Luxembourg", null },
                    { 73, "LT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Lithuania", null },
                    { 74, "LV", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Latvia", null },
                    { 75, "MA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Morocco", null },
                    { 76, "MC", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Monaco", null },
                    { 77, "MD", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), "Moldova", null },
                    { 78, "MF", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Saint Martin", null },
                    { 79, "MG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Madagascar", null },
                    { 80, "MQ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Martinique", null },
                    { 81, "MT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Malta", null },
                    { 82, "MX", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Mexico", null },
                    { 83, "MY", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Malaysia", null },
                    { 84, "NG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Nigeria", null },
                    { 85, "NI", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Nicaragua", null },
                    { 86, "NL", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Netherlands", null },
                    { 87, "NO", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Norway", null },
                    { 88, "NZ", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "New Zealand", null },
                    { 89, "OM", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Oman", null },
                    { 90, "PA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Panama", null },
                    { 91, "PE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Peru", null },
                    { 92, "PL", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Poland", null },
                    { 93, "PM", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Saint Pierre and Miquelon", null },
                    { 94, "PR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Puerto Rico", null },
                    { 95, "PT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Portugal", null },
                    { 96, "PY", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2300), "Paraguay", null },
                    { 97, "QA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Qatar", null },
                    { 98, "RO", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Romania", null },
                    { 99, "RS", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Serbia", null },
                    { 100, "RU", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Russia", null },
                    { 101, "SA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Saudi Arabia", null },
                    { 102, "SE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Sweden", null },
                    { 103, "SG", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Singapore", null },
                    { 104, "SI", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Slovenia", null },
                    { 105, "SK", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Slovakia", null },
                    { 106, "SN", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Senegal", null },
                    { 107, "SR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Suriname", null },
                    { 108, "SV", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "El Salvador", null },
                    { 109, "TR", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Turkey", null },
                    { 110, "TT", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2310), "Trinidad and Tobago", null },
                    { 111, "UA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Ukraine", null },
                    { 112, "US", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "United States", null },
                    { 113, "UY", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Uruguay", null },
                    { 114, "VE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Venezuela", null },
                    { 115, "VI", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Virgin Islands", null },
                    { 116, "VN", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Vietnam", null },
                    { 117, "YE", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Yemen", null },
                    { 118, "ZA", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "South Africa", null },
                    { 119, "ZM", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Zambia", null },
                    { 120, "ZW", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(2330), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { 1, "Berlin", "Deutschland", new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(8500), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(8500), "info@beispiel-hersteller.de", "", "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", null, "https://www.beispiel-hersteller.de", "10115" });

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
                values: new object[] { 1, new DateTime(2025, 8, 23, 9, 26, 27, 630, DateTimeKind.Utc).AddTicks(7840), new DateTime(2025, 8, 23, 9, 26, 27, 630, DateTimeKind.Utc).AddTicks(7840), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", null, 1, "", "" });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9430), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9430), "Company.Name", null, "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Address", null, "Musterstraße 123" },
                    { 3, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.ZipCity", null, "12345 Musterstadt" },
                    { 4, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Country", null, "Deutschland" },
                    { 5, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Phone", null, "+49 123 456789" },
                    { 6, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Email", null, "info@musterfirma.de" },
                    { 7, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Website", null, "www.musterfirma.de" },
                    { 8, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.TaxId", null, "123/456/7890" },
                    { 9, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.VatId", null, "DE123456789" },
                    { 10, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.BankName", null, "Musterbank" },
                    { 11, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Iban", null, "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9640), "Company.Bic", null, "MUSTDEXXX" },
                    { 13, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Company.LogoPath", null, "" },
                    { 14, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Jwt.Key", null, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Jwt.Issuer", null, "maERP.Server" },
                    { 16, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Jwt.Audience", null, "maERP.Client" },
                    { 17, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Jwt.DurationInMinutes", null, "60" },
                    { 18, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Jwt.RefreshTokenExpireDays", null, "7" },
                    { 19, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Email.ApiKey", null, "Sendgrid-Key" },
                    { 20, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Email.FromAddress", null, "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Email.FromName", null, "maERP" },
                    { 22, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Telemetry.Endpoint", null, "http://localhost:4317" },
                    { 23, new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 8, 23, 9, 26, 27, 633, DateTimeKind.Utc).AddTicks(9650), "Telemetry.ServiceName", null, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "tax_class",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 23, 9, 26, 27, 631, DateTimeKind.Utc).AddTicks(510), new DateTime(2025, 8, 23, 9, 26, 27, 631, DateTimeKind.Utc).AddTicks(510), 19.0, null },
                    { 2, new DateTime(2025, 8, 23, 9, 26, 27, 631, DateTimeKind.Utc).AddTicks(610), new DateTime(2025, 8, 23, 9, 26, 27, 631, DateTimeKind.Utc).AddTicks(610), 7.0, null },
                    { 3, new DateTime(2025, 8, 23, 9, 26, 27, 631, DateTimeKind.Utc).AddTicks(610), new DateTime(2025, 8, 23, 9, 26, 27, 631, DateTimeKind.Utc).AddTicks(610), 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { 1, "admin@example.com", new DateTime(2025, 8, 23, 9, 26, 27, 624, DateTimeKind.Utc).AddTicks(9130), new DateTime(2025, 8, 23, 9, 26, 27, 624, DateTimeKind.Utc).AddTicks(9130), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "DefaultTenantId", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "deea8c07-f08f-4ff8-889f-86527ae78568", new DateTime(2025, 8, 23, 9, 26, 27, 554, DateTimeKind.Utc).AddTicks(6370), new DateTime(2025, 8, 23, 9, 26, 27, 554, DateTimeKind.Utc).AddTicks(6370), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEE26jIOI0/hdIcztswVVoXre2qgsgJqqvUyXQT3HySzqRl9kYvYgt5c9WNEKSkdg+g==", null, false, "c014eca1-6238-4c67-9ec0-278789395457", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "6a2d9b1a-1b02-411a-835c-320b6a0bb77d", new DateTime(2025, 8, 23, 9, 26, 27, 590, DateTimeKind.Utc).AddTicks(600), new DateTime(2025, 8, 23, 9, 26, 27, 590, DateTimeKind.Utc).AddTicks(600), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEPPJ0dTcLSrG/GPweKdzPSaUzm+mVMxSg2IJ8oj3lzyH0TpF8fC2KkoxAGRJ08/Bjw==", null, false, "20678e07-1ddc-41f4-92bb-c20c2d76ab0d", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { 1, new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(3640), new DateTime(2025, 8, 23, 9, 26, 27, 629, DateTimeKind.Utc).AddTicks(3650), "Hauptlager", null });

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
