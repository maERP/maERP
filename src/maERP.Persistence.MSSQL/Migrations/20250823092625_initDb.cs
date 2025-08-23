using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.MSSQL.Migrations
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AiModelType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NCtx = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ai_model", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerStatus = table.Column<int>(type: "int", nullable: false),
                    DateEnrollment = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "saleschannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportProducts = table.Column<bool>(type: "bit", nullable: false),
                    ImportCustomers = table.Column<bool>(type: "bit", nullable: false),
                    ImportOrders = table.Column<bool>(type: "bit", nullable: false),
                    ExportProducts = table.Column<bool>(type: "bit", nullable: false),
                    ExportCustomers = table.Column<bool>(type: "bit", nullable: false),
                    ExportOrders = table.Column<bool>(type: "bit", nullable: false),
                    InitialProductImportCompleted = table.Column<bool>(type: "bit", nullable: false),
                    InitialProductExportCompleted = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saleschannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingCost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingTaxRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingProviderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_provider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tax_class",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tax_class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ai_prompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AiModelId = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromptText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "customer_address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultDeliveryAddress = table.Column<bool>(type: "bit", nullable: false),
                    DefaultInvoiceAddress = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "customer_saleschannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverAddressZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceAddressCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderConfirmationSent = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceSent = table.Column<bool>(type: "bit", nullable: false),
                    ShippingInformationSent = table.Column<bool>(type: "bit", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "role_claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLength = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sku = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOptimized = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ean = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionOptimized = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseOptimized = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Msrp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Depth = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TaxClassId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultTenantId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
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
                });

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
                });

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "order_history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusOld = table.Column<int>(type: "int", nullable: true),
                    OrderStatusNew = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusOld = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusNew = table.Column<int>(type: "int", nullable: true),
                    ShippingStatusOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingStatusNew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSystemGenerated = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    MissingProductSku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissingProductEan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "goods_receipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "product_saleschannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RemoteProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RepricingType = table.Column<bool>(type: "bit", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "product_stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false),
                    StockMin = table.Column<double>(type: "float", nullable: false),
                    StockMax = table.Column<double>(type: "float", nullable: false),
                    StorageLocation = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "user_claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "order_item_serialnumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.InsertData(
                table: "country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3640), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3640), "Germany", null },
                    { 2, "AT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Austria", null },
                    { 3, "CH", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Switzerland", null },
                    { 4, "AD", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Andorra", null },
                    { 5, "AF", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Afghanistan", null },
                    { 6, "AG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Antigua and Barbuda", null },
                    { 7, "AL", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Albania", null },
                    { 8, "AM", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Armenia", null },
                    { 9, "AO", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Angola", null },
                    { 10, "AX", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Åland Islands", null },
                    { 11, "AR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Argentina", null },
                    { 12, "AT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Antarctica", null },
                    { 13, "AU", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Australia", null },
                    { 14, "AZ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Azerbaijan", null },
                    { 15, "BA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Bosnia and Herzegovina", null },
                    { 16, "BB", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Barbados", null },
                    { 17, "BE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3950), "Belgium", null },
                    { 18, "BG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3960), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3960), "Bulgaria", null },
                    { 19, "BL", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3960), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3960), "Saint Barthélemy", null },
                    { 20, "BO", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Bolivia", null },
                    { 21, "BR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Brazil", null },
                    { 22, "BS", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Bahamas", null },
                    { 23, "BY", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Belarus", null },
                    { 24, "BZ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Belize", null },
                    { 25, "CA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Canada", null },
                    { 26, "CH", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Cocos (Keeling) Islands", null },
                    { 27, "CI", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Ivory Coast", null },
                    { 28, "CL", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Chile", null },
                    { 29, "CN", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "China", null },
                    { 30, "CO", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Colombia", null },
                    { 31, "CR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3970), "Costa Rica", null },
                    { 32, "CU", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Cuba", null },
                    { 33, "CY", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Cyprus", null },
                    { 34, "CZ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Czech Republic", null },
                    { 35, "DO", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Dominican Republic", null },
                    { 36, "DK", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Denmark", null },
                    { 37, "DZ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Algeria", null },
                    { 38, "EC", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Ecuador", null },
                    { 39, "EE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Estonia", null },
                    { 40, "EG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Egypt", null },
                    { 41, "ER", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Eritrea", null },
                    { 42, "ES", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Spain", null },
                    { 43, "ET", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Ethiopia", null },
                    { 44, "FI", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Finland", null },
                    { 45, "FR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "France", null },
                    { 46, "GB", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "United Kingdom", null },
                    { 47, "GE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3980), "Georgia", null },
                    { 48, "GF", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "French Guiana", null },
                    { 49, "GH", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Ghana", null },
                    { 50, "GL", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Greenland", null },
                    { 51, "GP", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Guadeloupe", null },
                    { 52, "GR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Greece", null },
                    { 53, "GT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Guatemala", null },
                    { 54, "GY", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Guyana", null },
                    { 55, "HN", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Honduras", null },
                    { 56, "HR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Croatia", null },
                    { 57, "HT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Haiti", null },
                    { 58, "HU", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Hungary", null },
                    { 59, "ID", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Indonesia", null },
                    { 60, "IE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Ireland", null },
                    { 61, "IN", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "India", null },
                    { 62, "IR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Iran", null },
                    { 63, "IS", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Iceland", null },
                    { 64, "IT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Italy", null },
                    { 65, "JM", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), "Jamaica", null },
                    { 66, "JP", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(3990), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Japan", null },
                    { 67, "KE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Kenya", null },
                    { 68, "KG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Kyrgyzstan", null },
                    { 69, "KR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "South Korea", null },
                    { 70, "KW", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Kuwait", null },
                    { 71, "KZ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Kazakhstan", null },
                    { 72, "LU", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Luxembourg", null },
                    { 73, "LT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Lithuania", null },
                    { 74, "LV", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Latvia", null },
                    { 75, "MA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Morocco", null },
                    { 76, "MC", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Monaco", null },
                    { 77, "MD", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Moldova", null },
                    { 78, "MF", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Saint Martin", null },
                    { 79, "MG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Madagascar", null },
                    { 80, "MQ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Martinique", null },
                    { 81, "MT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Malta", null },
                    { 82, "MX", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Mexico", null },
                    { 83, "MY", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4000), "Malaysia", null },
                    { 84, "NG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Nigeria", null },
                    { 85, "NI", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Nicaragua", null },
                    { 86, "NL", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Netherlands", null },
                    { 87, "NO", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Norway", null },
                    { 88, "NZ", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "New Zealand", null },
                    { 89, "OM", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Oman", null },
                    { 90, "PA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Panama", null },
                    { 91, "PE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Peru", null },
                    { 92, "PL", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Poland", null },
                    { 93, "PM", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Saint Pierre and Miquelon", null },
                    { 94, "PR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Puerto Rico", null },
                    { 95, "PT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Portugal", null },
                    { 96, "PY", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Paraguay", null },
                    { 97, "QA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Qatar", null },
                    { 98, "RO", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Romania", null },
                    { 99, "RS", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Serbia", null },
                    { 100, "RU", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Russia", null },
                    { 101, "SA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4010), "Saudi Arabia", null },
                    { 102, "SE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Sweden", null },
                    { 103, "SG", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Singapore", null },
                    { 104, "SI", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Slovenia", null },
                    { 105, "SK", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Slovakia", null },
                    { 106, "SN", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Senegal", null },
                    { 107, "SR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Suriname", null },
                    { 108, "SV", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "El Salvador", null },
                    { 109, "TR", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Turkey", null },
                    { 110, "TT", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Trinidad and Tobago", null },
                    { 111, "UA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Ukraine", null },
                    { 112, "US", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "United States", null },
                    { 113, "UY", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Uruguay", null },
                    { 114, "VE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Venezuela", null },
                    { 115, "VI", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Virgin Islands", null },
                    { 116, "VN", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Vietnam", null },
                    { 117, "YE", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "Yemen", null },
                    { 118, "ZA", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), "South Africa", null },
                    { 119, "ZM", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4020), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4030), "Zambia", null },
                    { 120, "ZW", new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4030), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(4030), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { 1, "Berlin", "Deutschland", new DateTime(2025, 8, 23, 9, 26, 24, 632, DateTimeKind.Utc).AddTicks(610), new DateTime(2025, 8, 23, 9, 26, 24, 632, DateTimeKind.Utc).AddTicks(610), "info@beispiel-hersteller.de", "", "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", null, "https://www.beispiel-hersteller.de", "10115" });

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
                values: new object[] { 1, new DateTime(2025, 8, 23, 9, 26, 24, 632, DateTimeKind.Utc).AddTicks(9850), new DateTime(2025, 8, 23, 9, 26, 24, 632, DateTimeKind.Utc).AddTicks(9850), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", null, 1, "", "" });

            migrationBuilder.InsertData(
                table: "setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4130), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4130), "Company.Name", null, "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Address", null, "Musterstraße 123" },
                    { 3, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.ZipCity", null, "12345 Musterstadt" },
                    { 4, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Country", null, "Deutschland" },
                    { 5, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Phone", null, "+49 123 456789" },
                    { 6, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Email", null, "info@musterfirma.de" },
                    { 7, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Website", null, "www.musterfirma.de" },
                    { 8, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.TaxId", null, "123/456/7890" },
                    { 9, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.VatId", null, "DE123456789" },
                    { 10, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.BankName", null, "Musterbank" },
                    { 11, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Iban", null, "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.Bic", null, "MUSTDEXXX" },
                    { 13, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Company.LogoPath", null, "" },
                    { 14, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Jwt.Key", null, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Jwt.Issuer", null, "maERP.Server" },
                    { 16, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Jwt.Audience", null, "maERP.Client" },
                    { 17, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), "Jwt.DurationInMinutes", null, "60" },
                    { 18, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4330), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), "Jwt.RefreshTokenExpireDays", null, "7" },
                    { 19, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), "Email.ApiKey", null, "Sendgrid-Key" },
                    { 20, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), "Email.FromAddress", null, "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), "Email.FromName", null, "maERP" },
                    { 22, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), "Telemetry.Endpoint", null, "http://localhost:4317" },
                    { 23, new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), new DateTime(2025, 8, 23, 9, 26, 24, 636, DateTimeKind.Utc).AddTicks(4340), "Telemetry.ServiceName", null, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "tax_class",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 23, 9, 26, 24, 633, DateTimeKind.Utc).AddTicks(2440), new DateTime(2025, 8, 23, 9, 26, 24, 633, DateTimeKind.Utc).AddTicks(2440), 19.0, null },
                    { 2, new DateTime(2025, 8, 23, 9, 26, 24, 633, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 8, 23, 9, 26, 24, 633, DateTimeKind.Utc).AddTicks(2540), 7.0, null },
                    { 3, new DateTime(2025, 8, 23, 9, 26, 24, 633, DateTimeKind.Utc).AddTicks(2540), new DateTime(2025, 8, 23, 9, 26, 24, 633, DateTimeKind.Utc).AddTicks(2540), 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { 1, "admin@example.com", new DateTime(2025, 8, 23, 9, 26, 24, 626, DateTimeKind.Utc).AddTicks(5780), new DateTime(2025, 8, 23, 9, 26, 24, 626, DateTimeKind.Utc).AddTicks(5780), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "DefaultTenantId", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "0ee1381b-bfa7-420e-8435-5522de9967a6", new DateTime(2025, 8, 23, 9, 26, 24, 555, DateTimeKind.Utc).AddTicks(1060), new DateTime(2025, 8, 23, 9, 26, 24, 555, DateTimeKind.Utc).AddTicks(1060), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEDN14ccCCE6CwFqFQBdWtYV1CsEwNJ0XCeqdwQU24fWBzKuoy8naYmC1vxjjUyPn5Q==", null, false, "c88aa501-df37-496c-9d44-4096cc0d9d16", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "eb3f398b-ed5b-4025-8ee4-9493542f8b8c", new DateTime(2025, 8, 23, 9, 26, 24, 591, DateTimeKind.Utc).AddTicks(2010), new DateTime(2025, 8, 23, 9, 26, 24, 591, DateTimeKind.Utc).AddTicks(2010), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAELdfBgpzkzMauYMU6CAW6buoluqel60FmOduxosyiZL/ReTiLsje0S19V8ICAAf31A==", null, false, "003b67b9-a50a-4161-a031-89d722ab6b54", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { 1, new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(5430), new DateTime(2025, 8, 23, 9, 26, 24, 631, DateTimeKind.Utc).AddTicks(5430), "Hauptlager", null });

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
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
