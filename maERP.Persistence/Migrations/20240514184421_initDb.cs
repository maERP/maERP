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
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesChannelId = table.Column<int>(type: "integer", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    PaymentProvider = table.Column<string>(type: "text", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "text", nullable: false),
                    ShippingMethod = table.Column<string>(type: "text", nullable: false),
                    ShippingStatus = table.Column<string>(type: "text", nullable: false),
                    ShippingProvider = table.Column<string>(type: "text", nullable: false),
                    ShippingTrackingId = table.Column<string>(type: "text", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "numeric", nullable: false),
                    Tax = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "1d27e8c0-ffb8-478b-a750-97273c3d72f5", "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEOXhHeXl4NVx1Zf+QSWHdFSZn5UhATvNcwW19jezs04AekmxdL7T7V9GvWaT9Htaxw==", null, false, "565e7a6c-23e4-4a78-a012-264a0b68f91f", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "71345e0c-c782-4e69-a2ec-2cfc5271e115", "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEFmRbhv9XYlVs/ssYZhzYzq0gw/m8IDZel59u7cyO4jfixtabnArBuHJ3k0rR4oFWA==", null, false, "d86463a0-da27-47ff-9d30-2c866f553943", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3940), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3953), "Germany" },
                    { 2, "AT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3959), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3959), "Austria" },
                    { 3, "CH", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3960), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3960), "Switzerland" },
                    { 4, "AD", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3961), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3961), "Andorra" },
                    { 5, "AF", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3962), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3962), "Afghanistan" },
                    { 6, "AG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3963), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3963), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3963), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3964), "Albania" },
                    { 8, "AM", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3964), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3965), "Armenia" },
                    { 9, "AO", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3967), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3967), "Angola" },
                    { 10, "AX", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3968), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3968), "Åland Islands" },
                    { 11, "AR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3969), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3969), "Argentina" },
                    { 12, "AT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3969), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3970), "Antarctica" },
                    { 13, "AU", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3970), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3971), "Australia" },
                    { 14, "AZ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3971), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3972), "Azerbaijan" },
                    { 15, "BA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3972), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3972), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3973), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3973), "Barbados" },
                    { 17, "BE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3974), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3974), "Belgium" },
                    { 18, "BG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3975), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3975), "Bulgaria" },
                    { 19, "BL", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3976), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3976), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3977), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3977), "Bolivia" },
                    { 21, "BR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3978), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3978), "Brazil" },
                    { 22, "BS", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3979), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3979), "Bahamas" },
                    { 23, "BY", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3979), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3980), "Belarus" },
                    { 24, "BZ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3980), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3981), "Belize" },
                    { 25, "CA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3981), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3982), "Canada" },
                    { 26, "CH", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3982), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3982), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3983), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3983), "Ivory Coast" },
                    { 28, "CL", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3984), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3984), "Chile" },
                    { 29, "CN", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3985), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3985), "China" },
                    { 30, "CO", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3986), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3986), "Colombia" },
                    { 31, "CR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3987), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3987), "Costa Rica" },
                    { 32, "CU", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3988), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3988), "Cuba" },
                    { 33, "CY", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3988), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3989), "Cyprus" },
                    { 34, "CZ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3989), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3990), "Czech Republic" },
                    { 35, "DO", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3990), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3991), "Dominican Republic" },
                    { 36, "DK", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3991), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3991), "Denmark" },
                    { 37, "DZ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3992), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3992), "Algeria" },
                    { 38, "EC", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3993), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3993), "Ecuador" },
                    { 39, "EE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3994), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3994), "Estonia" },
                    { 40, "EG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3995), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3995), "Egypt" },
                    { 41, "ER", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3996), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3996), "Eritrea" },
                    { 42, "ES", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3997), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3997), "Spain" },
                    { 43, "ET", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3998), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3998), "Ethiopia" },
                    { 44, "FI", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3998), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3999), "Finland" },
                    { 45, "FR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(3999), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4000), "France" },
                    { 46, "GB", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4000), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4001), "United Kingdom" },
                    { 47, "GE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4001), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4001), "Georgia" },
                    { 48, "GF", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4002), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4002), "French Guiana" },
                    { 49, "GH", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4003), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4003), "Ghana" },
                    { 50, "GL", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4004), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4004), "Greenland" },
                    { 51, "GP", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4005), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4005), "Guadeloupe" },
                    { 52, "GR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4006), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4006), "Greece" },
                    { 53, "GT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4007), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4007), "Guatemala" },
                    { 54, "GY", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4008), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4008), "Guyana" },
                    { 55, "HN", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4009), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4009), "Honduras" },
                    { 56, "HR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4009), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4010), "Croatia" },
                    { 57, "HT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4010), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4011), "Haiti" },
                    { 58, "HU", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4011), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4011), "Hungary" },
                    { 59, "ID", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4012), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4012), "Indonesia" },
                    { 60, "IE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4013), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4013), "Ireland" },
                    { 61, "IN", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4014), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4014), "India" },
                    { 62, "IR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4015), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4015), "Iran" },
                    { 63, "IS", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4016), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4016), "Iceland" },
                    { 64, "IT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4017), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4017), "Italy" },
                    { 65, "JM", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4018), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4018), "Jamaica" },
                    { 66, "JP", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4018), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4019), "Japan" },
                    { 67, "KE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4019), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4020), "Kenya" },
                    { 68, "KG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4020), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4020), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4021), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4021), "South Korea" },
                    { 70, "KW", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4022), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4022), "Kuwait" },
                    { 71, "KZ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4023), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4023), "Kazakhstan" },
                    { 72, "LU", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4024), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4024), "Luxembourg" },
                    { 73, "LT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4025), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4025), "Lithuania" },
                    { 74, "LV", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4026), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4026), "Latvia" },
                    { 75, "MA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4027), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4027), "Morocco" },
                    { 76, "MC", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4027), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4028), "Monaco" },
                    { 77, "MD", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4028), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4029), "Moldova" },
                    { 78, "MF", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4029), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4030), "Saint Martin" },
                    { 79, "MG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4030), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4030), "Madagascar" },
                    { 80, "MQ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4031), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4031), "Martinique" },
                    { 81, "MT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4032), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4032), "Malta" },
                    { 82, "MX", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4033), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4033), "Mexico" },
                    { 83, "MY", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4034), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4034), "Malaysia" },
                    { 84, "NG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4035), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4035), "Nigeria" },
                    { 85, "NI", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4036), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4036), "Nicaragua" },
                    { 86, "NL", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4037), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4037), "Netherlands" },
                    { 87, "NO", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4037), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4038), "Norway" },
                    { 88, "NZ", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4038), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4039), "New Zealand" },
                    { 89, "OM", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4039), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4040), "Oman" },
                    { 90, "PA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4040), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4040), "Panama" },
                    { 91, "PE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4041), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4041), "Peru" },
                    { 92, "PL", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4042), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4042), "Poland" },
                    { 93, "PM", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4043), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4043), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4044), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4044), "Puerto Rico" },
                    { 95, "PT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4045), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4045), "Portugal" },
                    { 96, "PY", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4046), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4046), "Paraguay" },
                    { 97, "QA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4046), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4047), "Qatar" },
                    { 98, "RO", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4047), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4048), "Romania" },
                    { 99, "RS", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4048), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4049), "Serbia" },
                    { 100, "RU", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4049), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4049), "Russia" },
                    { 101, "SA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4050), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4050), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4051), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4051), "Sweden" },
                    { 103, "SG", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4052), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4052), "Singapore" },
                    { 104, "SI", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4053), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4053), "Slovenia" },
                    { 105, "SK", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4054), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4054), "Slovakia" },
                    { 106, "SN", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4055), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4055), "Senegal" },
                    { 107, "SR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4056), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4056), "Suriname" },
                    { 108, "SV", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4056), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4057), "El Salvador" },
                    { 109, "TR", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4057), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4058), "Turkey" },
                    { 110, "TT", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4058), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4058), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4059), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4060), "Ukraine" },
                    { 112, "US", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4060), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4060), "United States" },
                    { 113, "UY", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4061), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4061), "Uruguay" },
                    { 114, "VE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4062), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4062), "Venezuela" },
                    { 115, "VI", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4063), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4063), "Virgin Islands" },
                    { 116, "VN", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4064), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4064), "Vietnam" },
                    { 117, "YE", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4065), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4065), "Yemen" },
                    { 118, "ZA", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4066), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4066), "South Africa" },
                    { 119, "ZM", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4067), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4067), "Zambia" },
                    { 120, "ZW", new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4067), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(4068), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6246), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6246), 19.0 },
                    { 2, new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6250), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6251), 7.0 },
                    { 3, new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6251), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(6251), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5613), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5613), "Testlager" });

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
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "Name", "Password", "Type", "URL", "Username", "WarehouseId" },
                values: new object[] { 1, new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5916), new DateTime(2024, 5, 14, 18, 44, 20, 789, DateTimeKind.Utc).AddTicks(5917), false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "", 1 });

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
