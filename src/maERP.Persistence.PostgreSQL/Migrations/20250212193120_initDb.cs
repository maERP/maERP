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
                name: "AiPrompt",
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
                    table.PrimaryKey("PK_AiPrompt", x => x.Id);
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
                    NameOptimized = table.Column<string>(type: "text", nullable: false),
                    Ean = table.Column<string>(type: "text", nullable: false),
                    Asin = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DescriptionOptimized = table.Column<string>(type: "text", nullable: false),
                    UseOptimized = table.Column<bool>(type: "boolean", nullable: false),
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateCreated", "DateModified", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "83a2a410-615a-4ae4-bc0e-ff93e1bf1c83", new DateTime(2025, 2, 12, 19, 31, 19, 718, DateTimeKind.Utc).AddTicks(770), new DateTime(2025, 2, 12, 19, 31, 19, 718, DateTimeKind.Utc).AddTicks(780), "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEFRnjvtgqWLQvjykAXcAAyDhYt7jwmGsDg6n8cFiEMVYib9jDCsf/jpEa3YRxHAkHA==", null, false, "85d646c4-3e9b-4707-a234-2ed62befb931", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "f8b28b20-9b53-425b-9aca-9c90ac4556c3", new DateTime(2025, 2, 12, 19, 31, 19, 750, DateTimeKind.Utc).AddTicks(3330), new DateTime(2025, 2, 12, 19, 31, 19, 750, DateTimeKind.Utc).AddTicks(3330), "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEI2SrLyKhW01FRYyJ+iosfSN+QkmgjRWVXXFtFWD6MPK2Af4/1M6nKyk5V5zIPk1Eg==", null, false, "9dbed944-840e-411c-864f-025e40388f46", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4170), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4170), "Germany" },
                    { 2, "AT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Austria" },
                    { 3, "CH", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Switzerland" },
                    { 4, "AD", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Andorra" },
                    { 5, "AF", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Afghanistan" },
                    { 6, "AG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Albania" },
                    { 8, "AM", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Armenia" },
                    { 9, "AO", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Angola" },
                    { 10, "AX", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Åland Islands" },
                    { 11, "AR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Argentina" },
                    { 12, "AT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Antarctica" },
                    { 13, "AU", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Australia" },
                    { 14, "AZ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Azerbaijan" },
                    { 15, "BA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4430), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Barbados" },
                    { 17, "BE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Belgium" },
                    { 18, "BG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Bulgaria" },
                    { 19, "BL", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Bolivia" },
                    { 21, "BR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Brazil" },
                    { 22, "BS", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Bahamas" },
                    { 23, "BY", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Belarus" },
                    { 24, "BZ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Belize" },
                    { 25, "CA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Canada" },
                    { 26, "CH", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Ivory Coast" },
                    { 28, "CL", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Chile" },
                    { 29, "CN", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "China" },
                    { 30, "CO", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Colombia" },
                    { 31, "CR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Costa Rica" },
                    { 32, "CU", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Cuba" },
                    { 33, "CY", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Cyprus" },
                    { 34, "CZ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Czech Republic" },
                    { 35, "DO", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4440), "Dominican Republic" },
                    { 36, "DK", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Denmark" },
                    { 37, "DZ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Algeria" },
                    { 38, "EC", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Ecuador" },
                    { 39, "EE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Estonia" },
                    { 40, "EG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Egypt" },
                    { 41, "ER", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Eritrea" },
                    { 42, "ES", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Spain" },
                    { 43, "ET", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Ethiopia" },
                    { 44, "FI", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Finland" },
                    { 45, "FR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "France" },
                    { 46, "GB", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "United Kingdom" },
                    { 47, "GE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Georgia" },
                    { 48, "GF", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "French Guiana" },
                    { 49, "GH", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Ghana" },
                    { 50, "GL", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Greenland" },
                    { 51, "GP", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Guadeloupe" },
                    { 52, "GR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Greece" },
                    { 53, "GT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Guatemala" },
                    { 54, "GY", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Guyana" },
                    { 55, "HN", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4450), "Honduras" },
                    { 56, "HR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Croatia" },
                    { 57, "HT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Haiti" },
                    { 58, "HU", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Hungary" },
                    { 59, "ID", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Indonesia" },
                    { 60, "IE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Ireland" },
                    { 61, "IN", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "India" },
                    { 62, "IR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Iran" },
                    { 63, "IS", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Iceland" },
                    { 64, "IT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Italy" },
                    { 65, "JM", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Jamaica" },
                    { 66, "JP", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Japan" },
                    { 67, "KE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Kenya" },
                    { 68, "KG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "South Korea" },
                    { 70, "KW", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Kuwait" },
                    { 71, "KZ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Kazakhstan" },
                    { 72, "LU", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Luxembourg" },
                    { 73, "LT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Lithuania" },
                    { 74, "LV", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Latvia" },
                    { 75, "MA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Morocco" },
                    { 76, "MC", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4460), "Monaco" },
                    { 77, "MD", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Moldova" },
                    { 78, "MF", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Saint Martin" },
                    { 79, "MG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Madagascar" },
                    { 80, "MQ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Martinique" },
                    { 81, "MT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Malta" },
                    { 82, "MX", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Mexico" },
                    { 83, "MY", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Malaysia" },
                    { 84, "NG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Nigeria" },
                    { 85, "NI", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Nicaragua" },
                    { 86, "NL", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Netherlands" },
                    { 87, "NO", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Norway" },
                    { 88, "NZ", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "New Zealand" },
                    { 89, "OM", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Oman" },
                    { 90, "PA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Panama" },
                    { 91, "PE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Peru" },
                    { 92, "PL", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Poland" },
                    { 93, "PM", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Puerto Rico" },
                    { 95, "PT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Portugal" },
                    { 96, "PY", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), "Paraguay" },
                    { 97, "QA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Qatar" },
                    { 98, "RO", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Romania" },
                    { 99, "RS", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Serbia" },
                    { 100, "RU", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Russia" },
                    { 101, "SA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Sweden" },
                    { 103, "SG", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Singapore" },
                    { 104, "SI", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Slovenia" },
                    { 105, "SK", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Slovakia" },
                    { 106, "SN", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Senegal" },
                    { 107, "SR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Suriname" },
                    { 108, "SV", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "El Salvador" },
                    { 109, "TR", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Turkey" },
                    { 110, "TT", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Ukraine" },
                    { 112, "US", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "United States" },
                    { 113, "UY", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Uruguay" },
                    { 114, "VE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Venezuela" },
                    { 115, "VI", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Virgin Islands" },
                    { 116, "VN", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Vietnam" },
                    { 117, "YE", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4480), "Yemen" },
                    { 118, "ZA", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4490), "South Africa" },
                    { 119, "ZM", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4490), "Zambia" },
                    { 120, "ZW", new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4490), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(4490), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(9780), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(9780), 19.0 },
                    { 2, new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(9920), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(9920), 7.0 },
                    { 3, new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(9920), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(9920), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(6140), "Testlager" });

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
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "Type", "Url", "Username", "WarehouseId" },
                values: new object[] { 1, new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(7520), new DateTime(2025, 2, 12, 19, 31, 19, 782, DateTimeKind.Utc).AddTicks(7520), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "", 1 });

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
