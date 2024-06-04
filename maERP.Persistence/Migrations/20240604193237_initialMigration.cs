using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "8fcb1c13-f65e-49f8-8d28-53b89efa4a09", "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEMkKvTeWRHLVAEFZppkEl0qaTy0qtc7u8tBbEt5klc+fhiqdT2GOv7tOIcYSfsUg2g==", null, false, "382c65c8-6d76-428b-b760-706b02b50fcf", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "a7f9d4e3-b7ac-46b2-bdfd-41075fabcf52", "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEBq85fZPcJ+y5L73zBAu8Tp0YM8XtZdfJGadKc5w7U5+kTjtyR0BjSSqSahD7+Pz7A==", null, false, "b75637f7-1d18-4f20-90e1-7535b49fbf4d", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6057), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6072), "Germany" },
                    { 2, "AT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6080), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6082), "Austria" },
                    { 3, "CH", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6084), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6084), "Switzerland" },
                    { 4, "AD", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6085), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6086), "Andorra" },
                    { 5, "AF", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6087), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6089), "Afghanistan" },
                    { 6, "AG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6089), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6090), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6090), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6090), "Albania" },
                    { 8, "AM", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6091), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6091), "Armenia" },
                    { 9, "AO", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6092), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6094), "Angola" },
                    { 10, "AX", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6094), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6094), "Åland Islands" },
                    { 11, "AR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6095), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6097), "Argentina" },
                    { 12, "AT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6098), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6098), "Antarctica" },
                    { 13, "AU", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6099), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6100), "Australia" },
                    { 14, "AZ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6100), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6101), "Azerbaijan" },
                    { 15, "BA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6101), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6101), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6102), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6103), "Barbados" },
                    { 17, "BE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6104), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6104), "Belgium" },
                    { 18, "BG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6105), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6105), "Bulgaria" },
                    { 19, "BL", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6105), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6107), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6107), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6108), "Bolivia" },
                    { 21, "BR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6108), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6109), "Brazil" },
                    { 22, "BS", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6109), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6110), "Bahamas" },
                    { 23, "BY", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6110), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6110), "Belarus" },
                    { 24, "BZ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6111), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6111), "Belize" },
                    { 25, "CA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6112), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6112), "Canada" },
                    { 26, "CH", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6113), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6113), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6114), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6116), "Ivory Coast" },
                    { 28, "CL", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6116), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6117), "Chile" },
                    { 29, "CN", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6117), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6119), "China" },
                    { 30, "CO", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6119), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6121), "Colombia" },
                    { 31, "CR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6122), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6122), "Costa Rica" },
                    { 32, "CU", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6123), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6125), "Cuba" },
                    { 33, "CY", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6126), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6127), "Cyprus" },
                    { 34, "CZ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6128), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6130), "Czech Republic" },
                    { 35, "DO", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6130), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6132), "Dominican Republic" },
                    { 36, "DK", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6132), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6135), "Denmark" },
                    { 37, "DZ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6135), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6138), "Algeria" },
                    { 38, "EC", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6138), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6141), "Ecuador" },
                    { 39, "EE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6141), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6142), "Estonia" },
                    { 40, "EG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6142), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6144), "Egypt" },
                    { 41, "ER", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6145), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6146), "Eritrea" },
                    { 42, "ES", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6147), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6148), "Spain" },
                    { 43, "ET", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6149), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6151), "Ethiopia" },
                    { 44, "FI", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6152), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6154), "Finland" },
                    { 45, "FR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6155), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6158), "France" },
                    { 46, "GB", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6159), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6161), "United Kingdom" },
                    { 47, "GE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6161), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6161), "Georgia" },
                    { 48, "GF", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6162), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6164), "French Guiana" },
                    { 49, "GH", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6164), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6166), "Ghana" },
                    { 50, "GL", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6166), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6168), "Greenland" },
                    { 51, "GP", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6169), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6172), "Guadeloupe" },
                    { 52, "GR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6172), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6174), "Greece" },
                    { 53, "GT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6175), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6176), "Guatemala" },
                    { 54, "GY", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6177), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6179), "Guyana" },
                    { 55, "HN", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6179), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6180), "Honduras" },
                    { 56, "HR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6180), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6182), "Croatia" },
                    { 57, "HT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6183), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6184), "Haiti" },
                    { 58, "HU", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6185), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6187), "Hungary" },
                    { 59, "ID", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6187), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6190), "Indonesia" },
                    { 60, "IE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6190), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6192), "Ireland" },
                    { 61, "IN", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6192), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6197), "India" },
                    { 62, "IR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6197), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6199), "Iran" },
                    { 63, "IS", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6200), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6200), "Iceland" },
                    { 64, "IT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6201), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6203), "Italy" },
                    { 65, "JM", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6204), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6206), "Jamaica" },
                    { 66, "JP", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6206), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6208), "Japan" },
                    { 67, "KE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6209), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6211), "Kenya" },
                    { 68, "KG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6212), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6214), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6215), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6216), "South Korea" },
                    { 70, "KW", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6217), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6219), "Kuwait" },
                    { 71, "KZ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6220), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6220), "Kazakhstan" },
                    { 72, "LU", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6223), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6224), "Luxembourg" },
                    { 73, "LT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6224), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6226), "Lithuania" },
                    { 74, "LV", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6227), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6228), "Latvia" },
                    { 75, "MA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6229), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6231), "Morocco" },
                    { 76, "MC", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6231), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6233), "Monaco" },
                    { 77, "MD", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6234), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6236), "Moldova" },
                    { 78, "MF", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6236), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6238), "Saint Martin" },
                    { 79, "MG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6238), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6239), "Madagascar" },
                    { 80, "MQ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6239), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6242), "Martinique" },
                    { 81, "MT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6242), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6244), "Malta" },
                    { 82, "MX", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6244), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6247), "Mexico" },
                    { 83, "MY", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6247), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6249), "Malaysia" },
                    { 84, "NG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6250), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6252), "Nigeria" },
                    { 85, "NI", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6252), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6254), "Nicaragua" },
                    { 86, "NL", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6255), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6257), "Netherlands" },
                    { 87, "NO", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6258), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6258), "Norway" },
                    { 88, "NZ", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6258), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6260), "New Zealand" },
                    { 89, "OM", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6386), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6386), "Oman" },
                    { 90, "PA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6387), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6387), "Panama" },
                    { 91, "PE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6388), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6388), "Peru" },
                    { 92, "PL", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6388), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6389), "Poland" },
                    { 93, "PM", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6389), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6390), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6390), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6391), "Puerto Rico" },
                    { 95, "PT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6391), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6391), "Portugal" },
                    { 96, "PY", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6392), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6392), "Paraguay" },
                    { 97, "QA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6393), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6393), "Qatar" },
                    { 98, "RO", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6394), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6394), "Romania" },
                    { 99, "RS", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6395), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6395), "Serbia" },
                    { 100, "RU", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6396), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6396), "Russia" },
                    { 101, "SA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6396), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6397), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6397), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6398), "Sweden" },
                    { 103, "SG", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6398), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6398), "Singapore" },
                    { 104, "SI", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6399), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6399), "Slovenia" },
                    { 105, "SK", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6400), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6400), "Slovakia" },
                    { 106, "SN", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6401), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6401), "Senegal" },
                    { 107, "SR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6402), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6402), "Suriname" },
                    { 108, "SV", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6403), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6403), "El Salvador" },
                    { 109, "TR", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6403), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6404), "Turkey" },
                    { 110, "TT", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6404), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6405), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6405), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6405), "Ukraine" },
                    { 112, "US", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6406), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6406), "United States" },
                    { 113, "UY", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6407), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6407), "Uruguay" },
                    { 114, "VE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6408), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6408), "Venezuela" },
                    { 115, "VI", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6409), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6409), "Virgin Islands" },
                    { 116, "VN", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6410), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6410), "Vietnam" },
                    { 117, "YE", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6410), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6411), "Yemen" },
                    { 118, "ZA", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6411), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6412), "South Africa" },
                    { 119, "ZM", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6412), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6412), "Zambia" },
                    { 120, "ZW", new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6413), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(6413), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8667), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8668), 19.0 },
                    { 2, new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8706), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8706), 7.0 },
                    { 3, new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8707), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8707), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8040), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8041), "Testlager" });

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
                values: new object[] { 1, new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8353), new DateTime(2024, 6, 4, 15, 32, 36, 976, DateTimeKind.Utc).AddTicks(8353), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "", 1 });

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
