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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiModelType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ApiUrl = table.Column<string>(type: "text", nullable: false),
                    ApiUsername = table.Column<string>(type: "text", nullable: false),
                    ApiPassword = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    NCtx = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_model", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Country = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Website = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Logo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saleschannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "setting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ShippingProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrackingNumber = table.Column<string>(type: "text", nullable: false),
                    ShippingCost = table.Column<string>(type: "text", nullable: false),
                    ShippingTaxRate = table.Column<string>(type: "text", nullable: false),
                    ShippingProviderName = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_provider",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tax_class",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ai_prompt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AiModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    PromptText = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNr = table.Column<string>(type: "text", nullable: false),
                    Zip = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    DefaultDeliveryAddress = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultInvoiceAddress = table.Column<bool>(type: "boolean", nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    DeliveryAddressZip = table.Column<string>(type: "text", nullable: false),
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
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxLength = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWidth = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxHeight = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    ShippingProviderId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameOptimized = table.Column<string>(type: "text", nullable: true),
                    Ean = table.Column<string>(type: "text", nullable: true),
                    Asin = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionOptimized = table.Column<string>(type: "text", nullable: true),
                    UseOptimized = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Msrp = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Width = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    Depth = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    TaxClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManufacturerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    DefaultTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    SalesChannelsId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehousesId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    MissingProductSku = table.Column<string>(type: "text", nullable: false),
                    MissingProductEan = table.Column<string>(type: "text", nullable: false),
                    ShippingId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Supplier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RemoteProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    RepricingType = table.Column<bool>(type: "boolean", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Stock = table.Column<double>(type: "double precision", nullable: false),
                    StockMin = table.Column<double>(type: "double precision", nullable: false),
                    StockMax = table.Column<double>(type: "double precision", nullable: false),
                    StorageLocation = table.Column<double>(type: "double precision", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    RoleManageUser = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    { new Guid("00000000-0000-0000-0000-000000000001"), "DE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(1860), "Germany", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "AT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2290), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2290), "Austria", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "CH", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), "Switzerland", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "AD", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), "Andorra", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "AF", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2300), "Afghanistan", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "AG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), "Antigua and Barbuda", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "AL", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), "Albania", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "AM", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2310), "Armenia", null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "AO", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), "Angola", null },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "AX", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), "Åland Islands", null },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "AR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), "Argentina", null },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "AT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2320), "Antarctica", null },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "AU", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), "Australia", null },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "AZ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), "Azerbaijan", null },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "BA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2330), "Bosnia and Herzegovina", null },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "BB", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), "Barbados", null },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "BE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), "Belgium", null },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "BG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2340), "Bulgaria", null },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "BL", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), "Saint Barthélemy", null },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "BO", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), "Bolivia", null },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "BR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), "Brazil", null },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "BS", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), "Bahamas", null },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "BY", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), "Belarus", null },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "BZ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), "Belize", null },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "CA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2360), "Canada", null },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "CH", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), "Cocos (Keeling) Islands", null },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "CI", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), "Ivory Coast", null },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "CL", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2370), "Chile", null },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "CN", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), "China", null },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "CO", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), "Colombia", null },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "CR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), "Costa Rica", null },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "CU", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2380), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), "Cuba", null },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "CY", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), "Cyprus", null },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "CZ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), "Czech Republic", null },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "DO", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2390), "Dominican Republic", null },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "DK", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), "Denmark", null },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "DZ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), "Algeria", null },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "EC", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2400), "Ecuador", null },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "EE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), "Estonia", null },
                    { new Guid("00000000-0000-0000-0000-000000000040"), "EG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), "Egypt", null },
                    { new Guid("00000000-0000-0000-0000-000000000041"), "ER", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), "Eritrea", null },
                    { new Guid("00000000-0000-0000-0000-000000000042"), "ES", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2410), "Spain", null },
                    { new Guid("00000000-0000-0000-0000-000000000043"), "ET", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), "Ethiopia", null },
                    { new Guid("00000000-0000-0000-0000-000000000044"), "FI", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), "Finland", null },
                    { new Guid("00000000-0000-0000-0000-000000000045"), "FR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2420), "France", null },
                    { new Guid("00000000-0000-0000-0000-000000000046"), "GB", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), "United Kingdom", null },
                    { new Guid("00000000-0000-0000-0000-000000000047"), "GE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), "Georgia", null },
                    { new Guid("00000000-0000-0000-0000-000000000048"), "GF", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2430), "French Guiana", null },
                    { new Guid("00000000-0000-0000-0000-000000000049"), "GH", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), "Ghana", null },
                    { new Guid("00000000-0000-0000-0000-000000000050"), "GL", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), "Greenland", null },
                    { new Guid("00000000-0000-0000-0000-000000000051"), "GP", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), "Guadeloupe", null },
                    { new Guid("00000000-0000-0000-0000-000000000052"), "GR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2440), "Greece", null },
                    { new Guid("00000000-0000-0000-0000-000000000053"), "GT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), "Guatemala", null },
                    { new Guid("00000000-0000-0000-0000-000000000054"), "GY", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), "Guyana", null },
                    { new Guid("00000000-0000-0000-0000-000000000055"), "HN", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2450), "Honduras", null },
                    { new Guid("00000000-0000-0000-0000-000000000056"), "HR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), "Croatia", null },
                    { new Guid("00000000-0000-0000-0000-000000000057"), "HT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), "Haiti", null },
                    { new Guid("00000000-0000-0000-0000-000000000058"), "HU", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2460), "Hungary", null },
                    { new Guid("00000000-0000-0000-0000-000000000059"), "ID", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470), "Indonesia", null },
                    { new Guid("00000000-0000-0000-0000-000000000060"), "IE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2470), "Ireland", null },
                    { new Guid("00000000-0000-0000-0000-000000000061"), "IN", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), "India", null },
                    { new Guid("00000000-0000-0000-0000-000000000062"), "IR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), "Iran", null },
                    { new Guid("00000000-0000-0000-0000-000000000063"), "IS", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2480), "Iceland", null },
                    { new Guid("00000000-0000-0000-0000-000000000064"), "IT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), "Italy", null },
                    { new Guid("00000000-0000-0000-0000-000000000065"), "JM", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), "Jamaica", null },
                    { new Guid("00000000-0000-0000-0000-000000000066"), "JP", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2490), "Japan", null },
                    { new Guid("00000000-0000-0000-0000-000000000067"), "KE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), "Kenya", null },
                    { new Guid("00000000-0000-0000-0000-000000000068"), "KG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), "Kyrgyzstan", null },
                    { new Guid("00000000-0000-0000-0000-000000000069"), "KR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2500), "South Korea", null },
                    { new Guid("00000000-0000-0000-0000-000000000070"), "KW", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), "Kuwait", null },
                    { new Guid("00000000-0000-0000-0000-000000000071"), "KZ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), "Kazakhstan", null },
                    { new Guid("00000000-0000-0000-0000-000000000072"), "LU", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), "Luxembourg", null },
                    { new Guid("00000000-0000-0000-0000-000000000073"), "LT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2510), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), "Lithuania", null },
                    { new Guid("00000000-0000-0000-0000-000000000074"), "LV", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), "Latvia", null },
                    { new Guid("00000000-0000-0000-0000-000000000075"), "MA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), "Morocco", null },
                    { new Guid("00000000-0000-0000-0000-000000000076"), "MC", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2520), "Monaco", null },
                    { new Guid("00000000-0000-0000-0000-000000000077"), "MD", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), "Moldova", null },
                    { new Guid("00000000-0000-0000-0000-000000000078"), "MF", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), "Saint Martin", null },
                    { new Guid("00000000-0000-0000-0000-000000000079"), "MG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2530), "Madagascar", null },
                    { new Guid("00000000-0000-0000-0000-000000000080"), "MQ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), "Martinique", null },
                    { new Guid("00000000-0000-0000-0000-000000000081"), "MT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), "Malta", null },
                    { new Guid("00000000-0000-0000-0000-000000000082"), "MX", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), "Mexico", null },
                    { new Guid("00000000-0000-0000-0000-000000000083"), "MY", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2540), "Malaysia", null },
                    { new Guid("00000000-0000-0000-0000-000000000084"), "NG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), "Nigeria", null },
                    { new Guid("00000000-0000-0000-0000-000000000085"), "NI", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), "Nicaragua", null },
                    { new Guid("00000000-0000-0000-0000-000000000086"), "NL", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2550), "Netherlands", null },
                    { new Guid("00000000-0000-0000-0000-000000000087"), "NO", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), "Norway", null },
                    { new Guid("00000000-0000-0000-0000-000000000088"), "NZ", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), "New Zealand", null },
                    { new Guid("00000000-0000-0000-0000-000000000089"), "OM", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2560), "Oman", null },
                    { new Guid("00000000-0000-0000-0000-000000000090"), "PA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), "Panama", null },
                    { new Guid("00000000-0000-0000-0000-000000000091"), "PE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), "Peru", null },
                    { new Guid("00000000-0000-0000-0000-000000000092"), "PL", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), "Poland", null },
                    { new Guid("00000000-0000-0000-0000-000000000093"), "PM", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2570), "Saint Pierre and Miquelon", null },
                    { new Guid("00000000-0000-0000-0000-000000000094"), "PR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), "Puerto Rico", null },
                    { new Guid("00000000-0000-0000-0000-000000000095"), "PT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), "Portugal", null },
                    { new Guid("00000000-0000-0000-0000-000000000096"), "PY", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2580), "Paraguay", null },
                    { new Guid("00000000-0000-0000-0000-000000000097"), "QA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), "Qatar", null },
                    { new Guid("00000000-0000-0000-0000-000000000098"), "RO", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), "Romania", null },
                    { new Guid("00000000-0000-0000-0000-000000000099"), "RS", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), "Serbia", null },
                    { new Guid("00000000-0000-0000-0000-000000000100"), "RU", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2590), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), "Russia", null },
                    { new Guid("00000000-0000-0000-0000-000000000101"), "SA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), "Saudi Arabia", null },
                    { new Guid("00000000-0000-0000-0000-000000000102"), "SE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), "Sweden", null },
                    { new Guid("00000000-0000-0000-0000-000000000103"), "SG", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2600), "Singapore", null },
                    { new Guid("00000000-0000-0000-0000-000000000104"), "SI", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), "Slovenia", null },
                    { new Guid("00000000-0000-0000-0000-000000000105"), "SK", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), "Slovakia", null },
                    { new Guid("00000000-0000-0000-0000-000000000106"), "SN", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2610), "Senegal", null },
                    { new Guid("00000000-0000-0000-0000-000000000107"), "SR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), "Suriname", null },
                    { new Guid("00000000-0000-0000-0000-000000000108"), "SV", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), "El Salvador", null },
                    { new Guid("00000000-0000-0000-0000-000000000109"), "TR", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), "Turkey", null },
                    { new Guid("00000000-0000-0000-0000-000000000110"), "TT", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2620), "Trinidad and Tobago", null },
                    { new Guid("00000000-0000-0000-0000-000000000111"), "UA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), "Ukraine", null },
                    { new Guid("00000000-0000-0000-0000-000000000112"), "US", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), "United States", null },
                    { new Guid("00000000-0000-0000-0000-000000000113"), "UY", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2630), "Uruguay", null },
                    { new Guid("00000000-0000-0000-0000-000000000114"), "VE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), "Venezuela", null },
                    { new Guid("00000000-0000-0000-0000-000000000115"), "VI", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), "Virgin Islands", null },
                    { new Guid("00000000-0000-0000-0000-000000000116"), "VN", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2640), "Vietnam", null },
                    { new Guid("00000000-0000-0000-0000-000000000117"), "YE", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), "Yemen", null },
                    { new Guid("00000000-0000-0000-0000-000000000118"), "ZA", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), "South Africa", null },
                    { new Guid("00000000-0000-0000-0000-000000000119"), "ZM", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), "Zambia", null },
                    { new Guid("00000000-0000-0000-0000-000000000120"), "ZW", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(2650), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Berlin", "Deutschland", new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(9620), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(9620), "info@beispiel-hersteller.de", null, "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "https://www.beispiel-hersteller.de", "10115" });

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
                values: new object[] { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 9, 22, 20, 23, 49, 877, DateTimeKind.Utc).AddTicks(6520), new DateTime(2025, 9, 22, 20, 23, 49, 877, DateTimeKind.Utc).AddTicks(6520), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 1, "", "" });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666614"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(7970), "Jwt.Key", "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { new Guid("66666666-6666-6666-6666-666666666615"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200), "Jwt.Issuer", "maERP.Server" },
                    { new Guid("66666666-6666-6666-6666-666666666616"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8200), "Jwt.Audience", "maERP.Client" },
                    { new Guid("66666666-6666-6666-6666-666666666617"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), "Jwt.DurationInMinutes", "60" },
                    { new Guid("66666666-6666-6666-6666-666666666618"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), "Jwt.RefreshTokenExpireDays", "7" },
                    { new Guid("66666666-6666-6666-6666-666666666619"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), "Email.ApiKey", "Sendgrid-Key" },
                    { new Guid("66666666-6666-6666-6666-666666666620"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8210), "Email.FromAddress", "no-reply@martin-andrich.de" },
                    { new Guid("66666666-6666-6666-6666-666666666621"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), "Email.FromName", "maERP" },
                    { new Guid("66666666-6666-6666-6666-666666666622"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), "Telemetry.Endpoint", "http://localhost:4317" },
                    { new Guid("66666666-6666-6666-6666-666666666623"), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), new DateTime(2025, 9, 22, 20, 23, 49, 880, DateTimeKind.Utc).AddTicks(8220), "Telemetry.ServiceName", "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "tax_class",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777771"), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(160), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(160), 19.0, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("77777777-7777-7777-7777-777777777772"), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(290), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(290), 7.0, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("77777777-7777-7777-7777-777777777773"), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 9, 22, 20, 23, 49, 878, DateTimeKind.Utc).AddTicks(300), 0.0, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });

            migrationBuilder.InsertData(
                table: "tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "admin@example.com", new DateTime(2025, 9, 22, 20, 23, 49, 860, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 9, 22, 20, 23, 49, 860, DateTimeKind.Utc).AddTicks(3510), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "DefaultTenantId", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "ed75f91f-8d0d-43f2-b01e-42fc728a801f", new DateTime(2025, 9, 22, 20, 23, 49, 790, DateTimeKind.Utc).AddTicks(4320), new DateTime(2025, 9, 22, 20, 23, 49, 790, DateTimeKind.Utc).AddTicks(4320), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEI7vwFie7n0z+UWCXj/yBAQqBe81+wvX6YLCd+YDAcCWbaC1BgylVcLrqErtzexkTQ==", null, false, "15fead44-b2dc-4e4b-abe2-ea90c75bfaf7", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "9c39248f-65a1-4b93-ab8a-2abe33c15f65", new DateTime(2025, 9, 22, 20, 23, 49, 825, DateTimeKind.Utc).AddTicks(8040), new DateTime(2025, 9, 22, 20, 23, 49, 825, DateTimeKind.Utc).AddTicks(8040), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEKyULp+Xysrsn5eGOt/aquZF+0ictkoelNKH+IjOdUVwq3NQcocbmwGZYzXXQcC9NQ==", null, false, "74f128cb-3e75-46dc-94f5-2a6b19166d14", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 9, 22, 20, 23, 49, 866, DateTimeKind.Utc).AddTicks(4490), "Hauptlager", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") });

            migrationBuilder.InsertData(
                table: "user_role",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "9e224968-33e4-4652-b7b7-8574d048cdb9" }
                });

            migrationBuilder.InsertData(
                table: "user_tenant",
                columns: new[] { "TenantId", "UserId", "DateCreated", "DateModified", "Id", "IsDefault", "RoleManageUser" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "8e445865-a24d-4543-a6c6-9443d048cdb9", new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5310), new Guid("e28d1200-98dd-4b50-ba3f-794cb5628e81"), true, true });

            migrationBuilder.InsertData(
                table: "user_tenant",
                columns: new[] { "TenantId", "UserId", "DateCreated", "DateModified", "Id", "IsDefault" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "9e224968-33e4-4652-b7b7-8574d048cdb9", new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5450), new DateTime(2025, 9, 22, 20, 23, 49, 863, DateTimeKind.Utc).AddTicks(5450), new Guid("97aec4e7-8ce0-4e08-af74-956ce4f796e5"), true });

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
