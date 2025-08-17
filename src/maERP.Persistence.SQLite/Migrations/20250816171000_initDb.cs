using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.SQLite.Migrations
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AiModelType = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ApiUsername = table.Column<string>(type: "TEXT", nullable: false),
                    ApiPassword = table.Column<string>(type: "TEXT", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false),
                    NCtx = table.Column<uint>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: false),
                    VatNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    DateEnrollment = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Street = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ZipCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Website = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Logo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    ImportProducts = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImportCustomers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImportOrders = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExportProducts = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExportCustomers = table.Column<bool>(type: "INTEGER", nullable: false),
                    ExportOrders = table.Column<bool>(type: "INTEGER", nullable: false),
                    InitialProductImportCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    InitialProductExportCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrackingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingCost = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingTaxRate = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingProviderName = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxRate = table.Column<double>(type: "REAL", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    TenantCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContactEmail = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AiPrompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AiModelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Identifier = table.Column<string>(type: "TEXT", nullable: false),
                    PromptText = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    HouseNr = table.Column<string>(type: "TEXT", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    DefaultDeliveryAddress = table.Column<bool>(type: "INTEGER", nullable: false),
                    DefaultInvoiceAddress = table.Column<bool>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesChannelId = table.Column<int>(type: "INTEGER", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesChannelId = table.Column<int>(type: "INTEGER", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentProvider = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerNote = table.Column<string>(type: "TEXT", nullable: false),
                    InternalNote = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressLastName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressCompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressPhone = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "TEXT", nullable: false),
                    DeliverAddressZip = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressLastName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressCompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressPhone = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressStreet = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressCity = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressZip = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressCountry = table.Column<string>(type: "TEXT", nullable: false),
                    OrderConfirmationSent = table.Column<bool>(type: "INTEGER", nullable: false),
                    InvoiceSent = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShippingInformationSent = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    MaxLength = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    MaxWidth = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    MaxHeight = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    ShippingProviderId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sku = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NameOptimized = table.Column<string>(type: "TEXT", nullable: false),
                    Ean = table.Column<string>(type: "TEXT", nullable: false),
                    Asin = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionOptimized = table.Column<string>(type: "TEXT", nullable: false),
                    UseOptimized = table.Column<bool>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Msrp = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    Width = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    Depth = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    TaxClassId = table.Column<int>(type: "INTEGER", nullable: false),
                    ManufacturerId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DefaultTenantId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
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
                    SalesChannelsId = table.Column<int>(type: "INTEGER", nullable: false),
                    WarehousesId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: true),
                    Subtotal = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressLastName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressCompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressPhone = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressStreet = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressCity = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressZip = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceAddressCountry = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressLastName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressCompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressPhone = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressZip = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderStatusOld = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderStatusNew = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentStatusOld = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentStatusNew = table.Column<int>(type: "INTEGER", nullable: true),
                    ShippingStatusOld = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingStatusNew = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IsSystemGenerated = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "REAL", nullable: false),
                    MissingProductSku = table.Column<string>(type: "TEXT", nullable: false),
                    MissingProductEan = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    WarehouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Supplier = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalesChannelId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    RemoteProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    RepricingType = table.Column<bool>(type: "INTEGER", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    WarehouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<double>(type: "REAL", nullable: false),
                    StockMin = table.Column<double>(type: "REAL", nullable: false),
                    StockMax = table.Column<double>(type: "REAL", nullable: false),
                    StorageLocation = table.Column<double>(type: "REAL", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: true),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    SKU = table.Column<string>(type: "TEXT", nullable: false),
                    EAN = table.Column<string>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "REAL", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    OrderItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TenantId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "85183938-af1a-4d7a-a7d0-43217e1fb5d6", new DateTime(2025, 8, 16, 17, 10, 0, 202, DateTimeKind.Utc).AddTicks(7080), new DateTime(2025, 8, 16, 17, 10, 0, 202, DateTimeKind.Utc).AddTicks(7080), null, "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEOvSygbbHKbypG4CUYzLBbfR2SEOMfZdE6EP+CE5aE0I/Cwcgb1CQMnP73uHJy1qQg==", null, false, "7f3fd395-5bab-49cc-ae75-e09220067321", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "a120f299-6531-4060-b182-b3944d7c6595", new DateTime(2025, 8, 16, 17, 10, 0, 238, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 8, 16, 17, 10, 0, 238, DateTimeKind.Utc).AddTicks(300), null, "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEJkhO1jxB7RtHUgGSSgkd5s10PRJooLHatx0pVVlZSViUm3LmRhYcbWuz2q/Ttp+VA==", null, false, "3741dc36-787b-4b62-b5ac-c5d11da6836a", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2630), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2630), "Germany", null },
                    { 2, "AT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Austria", null },
                    { 3, "CH", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Switzerland", null },
                    { 4, "AD", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Andorra", null },
                    { 5, "AF", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Afghanistan", null },
                    { 6, "AG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Antigua and Barbuda", null },
                    { 7, "AL", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Albania", null },
                    { 8, "AM", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Armenia", null },
                    { 9, "AO", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Angola", null },
                    { 10, "AX", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Åland Islands", null },
                    { 11, "AR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Argentina", null },
                    { 12, "AT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Antarctica", null },
                    { 13, "AU", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Australia", null },
                    { 14, "AZ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Azerbaijan", null },
                    { 15, "BA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Bosnia and Herzegovina", null },
                    { 16, "BB", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2920), "Barbados", null },
                    { 17, "BE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Belgium", null },
                    { 18, "BG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Bulgaria", null },
                    { 19, "BL", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Saint Barthélemy", null },
                    { 20, "BO", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Bolivia", null },
                    { 21, "BR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Brazil", null },
                    { 22, "BS", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Bahamas", null },
                    { 23, "BY", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Belarus", null },
                    { 24, "BZ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Belize", null },
                    { 25, "CA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Canada", null },
                    { 26, "CH", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Cocos (Keeling) Islands", null },
                    { 27, "CI", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Ivory Coast", null },
                    { 28, "CL", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Chile", null },
                    { 29, "CN", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "China", null },
                    { 30, "CO", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Colombia", null },
                    { 31, "CR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Costa Rica", null },
                    { 32, "CU", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Cuba", null },
                    { 33, "CY", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2930), "Cyprus", null },
                    { 34, "CZ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Czech Republic", null },
                    { 35, "DO", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Dominican Republic", null },
                    { 36, "DK", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Denmark", null },
                    { 37, "DZ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Algeria", null },
                    { 38, "EC", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Ecuador", null },
                    { 39, "EE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Estonia", null },
                    { 40, "EG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Egypt", null },
                    { 41, "ER", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Eritrea", null },
                    { 42, "ES", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Spain", null },
                    { 43, "ET", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Ethiopia", null },
                    { 44, "FI", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Finland", null },
                    { 45, "FR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "France", null },
                    { 46, "GB", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "United Kingdom", null },
                    { 47, "GE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Georgia", null },
                    { 48, "GF", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "French Guiana", null },
                    { 49, "GH", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Ghana", null },
                    { 50, "GL", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Greenland", null },
                    { 51, "GP", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2940), "Guadeloupe", null },
                    { 52, "GR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Greece", null },
                    { 53, "GT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Guatemala", null },
                    { 54, "GY", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Guyana", null },
                    { 55, "HN", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Honduras", null },
                    { 56, "HR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Croatia", null },
                    { 57, "HT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Haiti", null },
                    { 58, "HU", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Hungary", null },
                    { 59, "ID", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Indonesia", null },
                    { 60, "IE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Ireland", null },
                    { 61, "IN", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "India", null },
                    { 62, "IR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Iran", null },
                    { 63, "IS", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Iceland", null },
                    { 64, "IT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Italy", null },
                    { 65, "JM", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Jamaica", null },
                    { 66, "JP", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Japan", null },
                    { 67, "KE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Kenya", null },
                    { 68, "KG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "Kyrgyzstan", null },
                    { 69, "KR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2950), "South Korea", null },
                    { 70, "KW", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Kuwait", null },
                    { 71, "KZ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Kazakhstan", null },
                    { 72, "LU", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Luxembourg", null },
                    { 73, "LT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Lithuania", null },
                    { 74, "LV", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Latvia", null },
                    { 75, "MA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Morocco", null },
                    { 76, "MC", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Monaco", null },
                    { 77, "MD", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Moldova", null },
                    { 78, "MF", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Saint Martin", null },
                    { 79, "MG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Madagascar", null },
                    { 80, "MQ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Martinique", null },
                    { 81, "MT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Malta", null },
                    { 82, "MX", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Mexico", null },
                    { 83, "MY", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Malaysia", null },
                    { 84, "NG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Nigeria", null },
                    { 85, "NI", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Nicaragua", null },
                    { 86, "NL", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Netherlands", null },
                    { 87, "NO", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), "Norway", null },
                    { 88, "NZ", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2960), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "New Zealand", null },
                    { 89, "OM", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Oman", null },
                    { 90, "PA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Panama", null },
                    { 91, "PE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Peru", null },
                    { 92, "PL", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Poland", null },
                    { 93, "PM", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Saint Pierre and Miquelon", null },
                    { 94, "PR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Puerto Rico", null },
                    { 95, "PT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Portugal", null },
                    { 96, "PY", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Paraguay", null },
                    { 97, "QA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Qatar", null },
                    { 98, "RO", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Romania", null },
                    { 99, "RS", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Serbia", null },
                    { 100, "RU", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Russia", null },
                    { 101, "SA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Saudi Arabia", null },
                    { 102, "SE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Sweden", null },
                    { 103, "SG", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Singapore", null },
                    { 104, "SI", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Slovenia", null },
                    { 105, "SK", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Slovakia", null },
                    { 106, "SN", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2970), "Senegal", null },
                    { 107, "SR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2990), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(2990), "Suriname", null },
                    { 108, "SV", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "El Salvador", null },
                    { 109, "TR", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Turkey", null },
                    { 110, "TT", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Trinidad and Tobago", null },
                    { 111, "UA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Ukraine", null },
                    { 112, "US", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "United States", null },
                    { 113, "UY", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Uruguay", null },
                    { 114, "VE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Venezuela", null },
                    { 115, "VI", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Virgin Islands", null },
                    { 116, "VN", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Vietnam", null },
                    { 117, "YE", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Yemen", null },
                    { 118, "ZA", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "South Africa", null },
                    { 119, "ZM", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Zambia", null },
                    { 120, "ZW", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(3000), "Zimbabwe", null }
                });

            migrationBuilder.InsertData(
                table: "Manufacturer",
                columns: new[] { "Id", "City", "Country", "DateCreated", "DateModified", "Email", "Logo", "Name", "Phone", "State", "Street", "TenantId", "Website", "ZipCode" },
                values: new object[] { 1, "Berlin", "Deutschland", new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(9510), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(9510), "info@beispiel-hersteller.de", "", "Beispiel Hersteller GmbH", "+49 30 12345678", "Berlin", "Musterstraße 123", null, "https://www.beispiel-hersteller.de", "10115" });

            migrationBuilder.InsertData(
                table: "SalesChannel",
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "TenantId", "Type", "Url", "Username" },
                values: new object[] { 1, new DateTime(2025, 8, 16, 17, 10, 0, 278, DateTimeKind.Utc).AddTicks(9480), new DateTime(2025, 8, 16, 17, 10, 0, 278, DateTimeKind.Utc).AddTicks(9480), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", null, 1, "", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "TenantId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2240), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2240), "Company.Name", null, "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2420), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2420), "Company.Address", null, "Musterstraße 123" },
                    { 3, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.ZipCity", null, "12345 Musterstadt" },
                    { 4, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.Country", null, "Deutschland" },
                    { 5, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.Phone", null, "+49 123 456789" },
                    { 6, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.Email", null, "info@musterfirma.de" },
                    { 7, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.Website", null, "www.musterfirma.de" },
                    { 8, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.TaxId", null, "123/456/7890" },
                    { 9, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.VatId", null, "DE123456789" },
                    { 10, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.BankName", null, "Musterbank" },
                    { 11, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.Iban", null, "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.Bic", null, "MUSTDEXXX" },
                    { 13, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Company.LogoPath", null, "" },
                    { 14, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Jwt.Key", null, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Jwt.Issuer", null, "maERP.Server" },
                    { 16, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Jwt.Audience", null, "maERP.Client" },
                    { 17, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Jwt.DurationInMinutes", null, "60" },
                    { 18, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Jwt.RefreshTokenExpireDays", null, "7" },
                    { 19, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Email.ApiKey", null, "Sendgrid-Key" },
                    { 20, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Email.FromAddress", null, "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Email.FromName", null, "maERP" },
                    { 22, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Telemetry.Endpoint", null, "http://localhost:4317" },
                    { 23, new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), new DateTime(2025, 8, 16, 17, 10, 0, 282, DateTimeKind.Utc).AddTicks(2430), "Telemetry.ServiceName", null, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate", "TenantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 16, 17, 10, 0, 279, DateTimeKind.Utc).AddTicks(2370), new DateTime(2025, 8, 16, 17, 10, 0, 279, DateTimeKind.Utc).AddTicks(2370), 19.0, null },
                    { 2, new DateTime(2025, 8, 16, 17, 10, 0, 279, DateTimeKind.Utc).AddTicks(2470), new DateTime(2025, 8, 16, 17, 10, 0, 279, DateTimeKind.Utc).AddTicks(2470), 7.0, null },
                    { 3, new DateTime(2025, 8, 16, 17, 10, 0, 279, DateTimeKind.Utc).AddTicks(2470), new DateTime(2025, 8, 16, 17, 10, 0, 279, DateTimeKind.Utc).AddTicks(2470), 0.0, null }
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "ContactEmail", "DateCreated", "DateModified", "Description", "IsActive", "Name", "TenantCode" },
                values: new object[] { 1, "admin@example.com", new DateTime(2025, 8, 16, 17, 10, 0, 272, DateTimeKind.Utc).AddTicks(8460), new DateTime(2025, 8, 16, 17, 10, 0, 272, DateTimeKind.Utc).AddTicks(8460), "Default tenant for initial setup", true, "Default Tenant", "DEFAULT" });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name", "TenantId" },
                values: new object[] { 1, new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 8, 16, 17, 10, 0, 277, DateTimeKind.Utc).AddTicks(4460), "Hauptlager", null });

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
