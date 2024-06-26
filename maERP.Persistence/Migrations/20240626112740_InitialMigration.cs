using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    ApiUsername = table.Column<string>(type: "text", nullable: false),
                    ApiPassword = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
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
                name: "Prompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AiModelType = table.Column<int>(type: "integer", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    PromptText = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompt", x => x.Id);
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
                name: "CustomerSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    SalesChannelId = table.Column<int>(type: "integer", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Subtotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
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
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    URL = table.Column<string>(type: "text", nullable: false),
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
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    MissingProductSku = table.Column<string>(type: "text", nullable: false),
                    MissingProductEan = table.Column<string>(type: "text", nullable: false),
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
                    Stock = table.Column<double>(type: "double precision", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "OrderItemSerialNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderItemId = table.Column<int>(type: "integer", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "c55e9898-3637-4db3-ba4a-0d5babd63531", "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEDmD1w9LKWP+a9IrJpMENWKNDUGw7zNDyN6JMNa08eYwGtd3CuLVU/b1KD3OD0HDwg==", null, false, "f7f7c5b5-f125-43c3-a828-ab19fb7d89b1", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "c41ffaa9-712a-41e1-ba31-c92dc8ae65e3", "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEGqMrPIoQfmjWeYw+6lysbyKd4E2JMxANo5Ou8DBqaM9RFxFnXuYrqIZnoJTeyBPjQ==", null, false, "32806f91-3b47-424c-9503-515dac425ad7", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(940), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(940), "Germany" },
                    { 2, "AT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(940), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(940), "Austria" },
                    { 3, "CH", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(940), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Switzerland" },
                    { 4, "AD", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Andorra" },
                    { 5, "AF", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Afghanistan" },
                    { 6, "AG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Albania" },
                    { 8, "AM", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Armenia" },
                    { 9, "AO", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Angola" },
                    { 10, "AX", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Åland Islands" },
                    { 11, "AR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Argentina" },
                    { 12, "AT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Antarctica" },
                    { 13, "AU", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Australia" },
                    { 14, "AZ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Azerbaijan" },
                    { 15, "BA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), "Barbados" },
                    { 17, "BE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(950), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Belgium" },
                    { 18, "BG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Bulgaria" },
                    { 19, "BL", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Bolivia" },
                    { 21, "BR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Brazil" },
                    { 22, "BS", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Bahamas" },
                    { 23, "BY", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Belarus" },
                    { 24, "BZ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Belize" },
                    { 25, "CA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Canada" },
                    { 26, "CH", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Ivory Coast" },
                    { 28, "CL", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Chile" },
                    { 29, "CN", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "China" },
                    { 30, "CO", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(960), "Colombia" },
                    { 31, "CR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Costa Rica" },
                    { 32, "CU", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Cuba" },
                    { 33, "CY", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Cyprus" },
                    { 34, "CZ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Czech Republic" },
                    { 35, "DO", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Dominican Republic" },
                    { 36, "DK", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Denmark" },
                    { 37, "DZ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Algeria" },
                    { 38, "EC", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Ecuador" },
                    { 39, "EE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Estonia" },
                    { 40, "EG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Egypt" },
                    { 41, "ER", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Eritrea" },
                    { 42, "ES", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Spain" },
                    { 43, "ET", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(970), "Ethiopia" },
                    { 44, "FI", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Finland" },
                    { 45, "FR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "France" },
                    { 46, "GB", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "United Kingdom" },
                    { 47, "GE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Georgia" },
                    { 48, "GF", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "French Guiana" },
                    { 49, "GH", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Ghana" },
                    { 50, "GL", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Greenland" },
                    { 51, "GP", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Guadeloupe" },
                    { 52, "GR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Greece" },
                    { 53, "GT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Guatemala" },
                    { 54, "GY", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Guyana" },
                    { 55, "HN", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Honduras" },
                    { 56, "HR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Croatia" },
                    { 57, "HT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), "Haiti" },
                    { 58, "HU", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(980), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Hungary" },
                    { 59, "ID", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Indonesia" },
                    { 60, "IE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Ireland" },
                    { 61, "IN", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "India" },
                    { 62, "IR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Iran" },
                    { 63, "IS", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Iceland" },
                    { 64, "IT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Italy" },
                    { 65, "JM", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Jamaica" },
                    { 66, "JP", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Japan" },
                    { 67, "KE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Kenya" },
                    { 68, "KG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "South Korea" },
                    { 70, "KW", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Kuwait" },
                    { 71, "KZ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(990), "Kazakhstan" },
                    { 72, "LU", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Luxembourg" },
                    { 73, "LT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Lithuania" },
                    { 74, "LV", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Latvia" },
                    { 75, "MA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Morocco" },
                    { 76, "MC", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Monaco" },
                    { 77, "MD", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Moldova" },
                    { 78, "MF", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Saint Martin" },
                    { 79, "MG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Madagascar" },
                    { 80, "MQ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Martinique" },
                    { 81, "MT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Malta" },
                    { 82, "MX", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Mexico" },
                    { 83, "MY", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Malaysia" },
                    { 84, "NG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Nigeria" },
                    { 85, "NI", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1000), "Nicaragua" },
                    { 86, "NL", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Netherlands" },
                    { 87, "NO", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Norway" },
                    { 88, "NZ", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "New Zealand" },
                    { 89, "OM", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Oman" },
                    { 90, "PA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Panama" },
                    { 91, "PE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Peru" },
                    { 92, "PL", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Poland" },
                    { 93, "PM", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Puerto Rico" },
                    { 95, "PT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Portugal" },
                    { 96, "PY", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Paraguay" },
                    { 97, "QA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Qatar" },
                    { 98, "RO", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Romania" },
                    { 99, "RS", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), "Serbia" },
                    { 100, "RU", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1010), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Russia" },
                    { 101, "SA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Sweden" },
                    { 103, "SG", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Singapore" },
                    { 104, "SI", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Slovenia" },
                    { 105, "SK", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Slovakia" },
                    { 106, "SN", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Senegal" },
                    { 107, "SR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Suriname" },
                    { 108, "SV", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "El Salvador" },
                    { 109, "TR", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Turkey" },
                    { 110, "TT", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "Ukraine" },
                    { 112, "US", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), "United States" },
                    { 113, "UY", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1020), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Uruguay" },
                    { 114, "VE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Venezuela" },
                    { 115, "VI", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Virgin Islands" },
                    { 116, "VN", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Vietnam" },
                    { 117, "YE", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Yemen" },
                    { 118, "ZA", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "South Africa" },
                    { 119, "ZM", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Zambia" },
                    { 120, "ZW", new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1030), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(2100), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(2100), 19.0 },
                    { 2, new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(2110), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(2110), 7.0 },
                    { 3, new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(2110), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(2110), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1800), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1800), "Testlager" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "9e224968-33e4-4652-b7b7-8574d048cdb9" }
                });

            migrationBuilder.InsertData(
                table: "SalesChannel",
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "Type", "URL", "Username", "WarehouseId" },
                values: new object[] { 1, new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1970), new DateTime(2024, 6, 26, 11, 27, 39, 646, DateTimeKind.Utc).AddTicks(1970), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "", 1 });

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
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemSerialNumber_OrderItemId",
                table: "OrderItemSerialNumber",
                column: "OrderItemId");

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
                name: "AiModel");

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
                name: "OrderItemSerialNumber");

            migrationBuilder.DropTable(
                name: "ProductSalesChannel");

            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.DropTable(
                name: "Prompt");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "ShippingProviderRate");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "SalesChannel");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShippingProvider");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "TaxClass");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
