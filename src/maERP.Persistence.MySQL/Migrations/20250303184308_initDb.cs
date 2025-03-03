using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AiModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AiModelType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiUsername = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiPassword = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountryCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VatNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Note = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerStatus = table.Column<int>(type: "int", nullable: false),
                    DateEnrollment = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: false),
                    TrackingNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingCost = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingTaxRate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingProviderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipping", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShippingProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProvider", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaxClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxRate = table.Column<double>(type: "double", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxClass", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AiPrompt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AiModelId = table.Column<int>(type: "int", nullable: false),
                    Identifier = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PromptText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lastname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseNr = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Zip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DefaultDeliveryAddress = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DefaultInvoiceAddress = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteCustomerId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    RemoteOrderId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentProvider = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentTransactionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomerNote = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InternalNote = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressStreet = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliverAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressFirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressLastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressStreet = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOrdered = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ShippingProviderRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxLength = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ShippingProviderId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProviderRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingProviderRate_ShippingProvider_ShippingProviderId",
                        column: x => x.ShippingProviderId,
                        principalTable: "ShippingProvider",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sku = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameOptimized = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ean = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Asin = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionOptimized = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UseOptimized = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Msrp = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Depth = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TaxClassId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImportProducts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ImportCustomers = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ImportOrders = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExportProducts = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExportCustomers = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ExportOrders = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InitialProductImportCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InitialProductExportCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TaxRate = table.Column<double>(type: "double", nullable: false),
                    MissingProductSku = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MissingProductEan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<double>(type: "double", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductSalesChannel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SalesChannelId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RemoteProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderItemSerialNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "764f1e43-9106-4eb5-971c-f6b4f6f2f75d", new DateTime(2025, 3, 3, 18, 43, 7, 937, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 3, 3, 18, 43, 7, 937, DateTimeKind.Utc).AddTicks(4870), "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEAdUh6X4nOFpjk005KcP2Khp4o4OL1iSZWgcMXveGGiGCJgwTT1zODS3YIUyLnke2Q==", null, false, "ef1d64cb-fea2-439b-bf4b-6d0d022178c2", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "4100d184-c198-47a2-ac46-9a8ff5a0c8ff", new DateTime(2025, 3, 3, 18, 43, 7, 972, DateTimeKind.Utc).AddTicks(4680), new DateTime(2025, 3, 3, 18, 43, 7, 972, DateTimeKind.Utc).AddTicks(4680), "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEA11Gbv2XGP8fitz2bC095PE2i0XeM4EBAj/GIsAOTc/y2U3YcOrVUDDiMw4xu8BfQ==", null, false, "4a3005dc-8b84-468d-ba2c-c5496e629392", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3190), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3190), "Germany" },
                    { 2, "AT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), "Austria" },
                    { 3, "CH", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), "Switzerland" },
                    { 4, "AD", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), "Andorra" },
                    { 5, "AF", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), "Afghanistan" },
                    { 6, "AG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3480), "Albania" },
                    { 8, "AM", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Armenia" },
                    { 9, "AO", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Angola" },
                    { 10, "AX", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Åland Islands" },
                    { 11, "AR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Argentina" },
                    { 12, "AT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Antarctica" },
                    { 13, "AU", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Australia" },
                    { 14, "AZ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Azerbaijan" },
                    { 15, "BA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Barbados" },
                    { 17, "BE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Belgium" },
                    { 18, "BG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Bulgaria" },
                    { 19, "BL", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Bolivia" },
                    { 21, "BR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Brazil" },
                    { 22, "BS", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Bahamas" },
                    { 23, "BY", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Belarus" },
                    { 24, "BZ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3490), "Belize" },
                    { 25, "CA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Canada" },
                    { 26, "CH", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Ivory Coast" },
                    { 28, "CL", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Chile" },
                    { 29, "CN", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "China" },
                    { 30, "CO", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Colombia" },
                    { 31, "CR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Costa Rica" },
                    { 32, "CU", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Cuba" },
                    { 33, "CY", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Cyprus" },
                    { 34, "CZ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Czech Republic" },
                    { 35, "DO", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Dominican Republic" },
                    { 36, "DK", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Denmark" },
                    { 37, "DZ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Algeria" },
                    { 38, "EC", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Ecuador" },
                    { 39, "EE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Estonia" },
                    { 40, "EG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Egypt" },
                    { 41, "ER", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Eritrea" },
                    { 42, "ES", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3500), "Spain" },
                    { 43, "ET", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Ethiopia" },
                    { 44, "FI", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Finland" },
                    { 45, "FR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "France" },
                    { 46, "GB", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "United Kingdom" },
                    { 47, "GE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Georgia" },
                    { 48, "GF", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "French Guiana" },
                    { 49, "GH", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Ghana" },
                    { 50, "GL", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Greenland" },
                    { 51, "GP", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Guadeloupe" },
                    { 52, "GR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Greece" },
                    { 53, "GT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Guatemala" },
                    { 54, "GY", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Guyana" },
                    { 55, "HN", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Honduras" },
                    { 56, "HR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Croatia" },
                    { 57, "HT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Haiti" },
                    { 58, "HU", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Hungary" },
                    { 59, "ID", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Indonesia" },
                    { 60, "IE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), "Ireland" },
                    { 61, "IN", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3510), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "India" },
                    { 62, "IR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Iran" },
                    { 63, "IS", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Iceland" },
                    { 64, "IT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Italy" },
                    { 65, "JM", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Jamaica" },
                    { 66, "JP", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Japan" },
                    { 67, "KE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Kenya" },
                    { 68, "KG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "South Korea" },
                    { 70, "KW", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Kuwait" },
                    { 71, "KZ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Kazakhstan" },
                    { 72, "LU", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Luxembourg" },
                    { 73, "LT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Lithuania" },
                    { 74, "LV", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Latvia" },
                    { 75, "MA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Morocco" },
                    { 76, "MC", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Monaco" },
                    { 77, "MD", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Moldova" },
                    { 78, "MF", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), "Saint Martin" },
                    { 79, "MG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3520), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Madagascar" },
                    { 80, "MQ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Martinique" },
                    { 81, "MT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Malta" },
                    { 82, "MX", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Mexico" },
                    { 83, "MY", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Malaysia" },
                    { 84, "NG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Nigeria" },
                    { 85, "NI", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Nicaragua" },
                    { 86, "NL", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Netherlands" },
                    { 87, "NO", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Norway" },
                    { 88, "NZ", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "New Zealand" },
                    { 89, "OM", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Oman" },
                    { 90, "PA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Panama" },
                    { 91, "PE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Peru" },
                    { 92, "PL", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Poland" },
                    { 93, "PM", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Puerto Rico" },
                    { 95, "PT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Portugal" },
                    { 96, "PY", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Paraguay" },
                    { 97, "QA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3530), "Qatar" },
                    { 98, "RO", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Romania" },
                    { 99, "RS", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Serbia" },
                    { 100, "RU", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Russia" },
                    { 101, "SA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Sweden" },
                    { 103, "SG", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Singapore" },
                    { 104, "SI", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Slovenia" },
                    { 105, "SK", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Slovakia" },
                    { 106, "SN", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Senegal" },
                    { 107, "SR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Suriname" },
                    { 108, "SV", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "El Salvador" },
                    { 109, "TR", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Turkey" },
                    { 110, "TT", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Ukraine" },
                    { 112, "US", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "United States" },
                    { 113, "UY", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Uruguay" },
                    { 114, "VE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Venezuela" },
                    { 115, "VI", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3540), "Virgin Islands" },
                    { 116, "VN", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), "Vietnam" },
                    { 117, "YE", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), "Yemen" },
                    { 118, "ZA", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), "South Africa" },
                    { 119, "ZM", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), "Zambia" },
                    { 120, "ZW", new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(3550), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9650), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9650), 19.0 },
                    { 2, new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750), 7.0 },
                    { 3, new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(9750), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(5560), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(5560), "Testlager" });

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
                values: new object[] { 1, new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(7120), new DateTime(2025, 3, 3, 18, 43, 8, 8, DateTimeKind.Utc).AddTicks(7120), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "", 1 });

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
                name: "AiModel");

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
