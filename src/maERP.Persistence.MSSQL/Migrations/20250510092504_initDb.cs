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
                name: "AiModel",
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
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
                    DateEnrollment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AiPrompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AiModelId = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PromptText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Msrp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Depth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxClassId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    MissingProductSku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissingProductEan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RemoteProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepricingType = table.Column<bool>(type: "bit", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "InvoiceItem",
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
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<double>(type: "float", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "a9053a9b-d89f-48ed-83a6-48cc7c2d9f80", new DateTime(2025, 5, 10, 9, 25, 3, 711, DateTimeKind.Utc).AddTicks(7710), new DateTime(2025, 5, 10, 9, 25, 3, 711, DateTimeKind.Utc).AddTicks(7740), "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEB+W5PW1/iDn7Otli2xoavwReS3D0yB//o7zdorPR229WTU2PSEG9+E+KkjNyYm+aw==", null, false, "9acae09a-9d56-40ed-ba4c-4aafef507d99", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "1b8f7dd0-dc97-4210-a020-6f37513e0213", new DateTime(2025, 5, 10, 9, 25, 3, 746, DateTimeKind.Utc).AddTicks(5090), new DateTime(2025, 5, 10, 9, 25, 3, 746, DateTimeKind.Utc).AddTicks(5100), "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEBeVd+7KCEsT9qCv6ZLTdP9BaOaqidjE4V2cOz2CmsL8ZP4pl4wJN7aEVgfFynpHBQ==", null, false, "f610e4b9-6ab7-47ed-9e03-c5870df29575", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(4880), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(4880), "Germany" },
                    { 2, "AT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), "Austria" },
                    { 3, "CH", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), "Switzerland" },
                    { 4, "AD", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5170), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Andorra" },
                    { 5, "AF", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Afghanistan" },
                    { 6, "AG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Albania" },
                    { 8, "AM", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Armenia" },
                    { 9, "AO", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Angola" },
                    { 10, "AX", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Åland Islands" },
                    { 11, "AR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Argentina" },
                    { 12, "AT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Antarctica" },
                    { 13, "AU", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Australia" },
                    { 14, "AZ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Azerbaijan" },
                    { 15, "BA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Barbados" },
                    { 17, "BE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Belgium" },
                    { 18, "BG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Bulgaria" },
                    { 19, "BL", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Bolivia" },
                    { 21, "BR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5180), "Brazil" },
                    { 22, "BS", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Bahamas" },
                    { 23, "BY", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Belarus" },
                    { 24, "BZ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Belize" },
                    { 25, "CA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Canada" },
                    { 26, "CH", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Ivory Coast" },
                    { 28, "CL", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Chile" },
                    { 29, "CN", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "China" },
                    { 30, "CO", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Colombia" },
                    { 31, "CR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Costa Rica" },
                    { 32, "CU", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Cuba" },
                    { 33, "CY", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Cyprus" },
                    { 34, "CZ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Czech Republic" },
                    { 35, "DO", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Dominican Republic" },
                    { 36, "DK", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Denmark" },
                    { 37, "DZ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Algeria" },
                    { 38, "EC", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Ecuador" },
                    { 39, "EE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5190), "Estonia" },
                    { 40, "EG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Egypt" },
                    { 41, "ER", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Eritrea" },
                    { 42, "ES", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Spain" },
                    { 43, "ET", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Ethiopia" },
                    { 44, "FI", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Finland" },
                    { 45, "FR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "France" },
                    { 46, "GB", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "United Kingdom" },
                    { 47, "GE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Georgia" },
                    { 48, "GF", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "French Guiana" },
                    { 49, "GH", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Ghana" },
                    { 50, "GL", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Greenland" },
                    { 51, "GP", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Guadeloupe" },
                    { 52, "GR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Greece" },
                    { 53, "GT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Guatemala" },
                    { 54, "GY", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Guyana" },
                    { 55, "HN", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Honduras" },
                    { 56, "HR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Croatia" },
                    { 57, "HT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), "Haiti" },
                    { 58, "HU", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5200), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Hungary" },
                    { 59, "ID", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Indonesia" },
                    { 60, "IE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Ireland" },
                    { 61, "IN", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "India" },
                    { 62, "IR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Iran" },
                    { 63, "IS", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Iceland" },
                    { 64, "IT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Italy" },
                    { 65, "JM", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Jamaica" },
                    { 66, "JP", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Japan" },
                    { 67, "KE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Kenya" },
                    { 68, "KG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "South Korea" },
                    { 70, "KW", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Kuwait" },
                    { 71, "KZ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Kazakhstan" },
                    { 72, "LU", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Luxembourg" },
                    { 73, "LT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Lithuania" },
                    { 74, "LV", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Latvia" },
                    { 75, "MA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5210), "Morocco" },
                    { 76, "MC", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Monaco" },
                    { 77, "MD", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Moldova" },
                    { 78, "MF", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Saint Martin" },
                    { 79, "MG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Madagascar" },
                    { 80, "MQ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Martinique" },
                    { 81, "MT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Malta" },
                    { 82, "MX", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Mexico" },
                    { 83, "MY", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Malaysia" },
                    { 84, "NG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Nigeria" },
                    { 85, "NI", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Nicaragua" },
                    { 86, "NL", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Netherlands" },
                    { 87, "NO", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Norway" },
                    { 88, "NZ", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "New Zealand" },
                    { 89, "OM", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Oman" },
                    { 90, "PA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Panama" },
                    { 91, "PE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Peru" },
                    { 92, "PL", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Poland" },
                    { 93, "PM", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5220), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Puerto Rico" },
                    { 95, "PT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Portugal" },
                    { 96, "PY", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Paraguay" },
                    { 97, "QA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Qatar" },
                    { 98, "RO", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Romania" },
                    { 99, "RS", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Serbia" },
                    { 100, "RU", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Russia" },
                    { 101, "SA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Sweden" },
                    { 103, "SG", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Singapore" },
                    { 104, "SI", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Slovenia" },
                    { 105, "SK", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Slovakia" },
                    { 106, "SN", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Senegal" },
                    { 107, "SR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Suriname" },
                    { 108, "SV", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "El Salvador" },
                    { 109, "TR", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5230), "Turkey" },
                    { 110, "TT", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Ukraine" },
                    { 112, "US", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "United States" },
                    { 113, "UY", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Uruguay" },
                    { 114, "VE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Venezuela" },
                    { 115, "VI", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Virgin Islands" },
                    { 116, "VN", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Vietnam" },
                    { 117, "YE", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Yemen" },
                    { 118, "ZA", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "South Africa" },
                    { 119, "ZM", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Zambia" },
                    { 120, "ZW", new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(5240), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1080), new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1080), 19.0 },
                    { 2, new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1170), new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1170), 7.0 },
                    { 3, new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1220), new DateTime(2025, 5, 10, 9, 25, 3, 781, DateTimeKind.Utc).AddTicks(1220), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(7010), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(7010), "Testlager" });

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
                values: new object[] { 1, new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(8510), new DateTime(2025, 5, 10, 9, 25, 3, 780, DateTimeKind.Utc).AddTicks(8510), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "", 1 });

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
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "ShippingProviderRate");

            migrationBuilder.DropTable(
                name: "AiModel");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Invoice");

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
