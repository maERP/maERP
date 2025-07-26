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
                    NCtx = table.Column<uint>(type: "int unsigned", nullable: false),
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
                    DateEnrollment = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZipCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Website = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Logo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
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
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesChannel", x => x.Id);
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
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                    OrderConfirmationSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InvoiceSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ShippingInformationSent = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                    MaxLength = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    MaxWeight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Msrp = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    Depth = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    TaxClassId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    InvoiceStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentTransactionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
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
                    DeliveryAddressZip = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeliveryAddressCountry = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusOld = table.Column<int>(type: "int", nullable: true),
                    OrderStatusNew = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusOld = table.Column<int>(type: "int", nullable: true),
                    PaymentStatusNew = table.Column<int>(type: "int", nullable: true),
                    ShippingStatusOld = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShippingStatusNew = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsSystemGenerated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReceiptDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Supplier = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Notes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    RepricingType = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MinimumProfit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinimumProfitUnit = table.Column<int>(type: "int", nullable: false),
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
                name: "ProductStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<double>(type: "double", nullable: false),
                    StockMin = table.Column<double>(type: "double", nullable: false),
                    StockMax = table.Column<double>(type: "double", nullable: false),
                    StorageLocation = table.Column<double>(type: "double", nullable: false),
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
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EAN = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<double>(type: "double", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderItemId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "71ec1147-424c-45fe-b1a5-b367ffc62610", new DateTime(2025, 7, 26, 20, 22, 25, 849, DateTimeKind.Utc).AddTicks(6820), new DateTime(2025, 7, 26, 20, 22, 25, 849, DateTimeKind.Utc).AddTicks(6820), "admin@localhost.com", true, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEBvAoyQ226oCP4bpl4C3BcgntamdiNhu3MW3RRZTECEos2h7cZ2cOVknK1JqYBzGIA==", null, false, "b4f4982a-34d0-4428-8df4-4cd55b2e0c6b", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "0e6151c9-f4c1-48ba-8089-1ed2cbaa2981", new DateTime(2025, 7, 26, 20, 22, 25, 881, DateTimeKind.Utc).AddTicks(1430), new DateTime(2025, 7, 26, 20, 22, 25, 881, DateTimeKind.Utc).AddTicks(1430), "user@localhost.com", true, "System", "User", false, null, "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAELWLYJY9+mQuVadJjCQBBjfmHbqOIoJjjQ0TY0qwj97m0LrG4PGjQ7OBYEj6pT5Dig==", null, false, "c0b8c9fe-13d9-4b70-a45c-010de5430261", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(2760), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(2760), "Germany" },
                    { 2, "AT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), "Austria" },
                    { 3, "CH", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), "Switzerland" },
                    { 4, "AD", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), "Andorra" },
                    { 5, "AF", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), "Afghanistan" },
                    { 6, "AG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), "Albania" },
                    { 8, "AM", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3030), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Armenia" },
                    { 9, "AO", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Angola" },
                    { 10, "AX", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Åland Islands" },
                    { 11, "AR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Argentina" },
                    { 12, "AT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Antarctica" },
                    { 13, "AU", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Australia" },
                    { 14, "AZ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Azerbaijan" },
                    { 15, "BA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Barbados" },
                    { 17, "BE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Belgium" },
                    { 18, "BG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Bulgaria" },
                    { 19, "BL", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Bolivia" },
                    { 21, "BR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Brazil" },
                    { 22, "BS", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Bahamas" },
                    { 23, "BY", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Belarus" },
                    { 24, "BZ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Belize" },
                    { 25, "CA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Canada" },
                    { 26, "CH", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), "Ivory Coast" },
                    { 28, "CL", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3040), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Chile" },
                    { 29, "CN", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "China" },
                    { 30, "CO", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Colombia" },
                    { 31, "CR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Costa Rica" },
                    { 32, "CU", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Cuba" },
                    { 33, "CY", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Cyprus" },
                    { 34, "CZ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Czech Republic" },
                    { 35, "DO", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Dominican Republic" },
                    { 36, "DK", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Denmark" },
                    { 37, "DZ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Algeria" },
                    { 38, "EC", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Ecuador" },
                    { 39, "EE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Estonia" },
                    { 40, "EG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Egypt" },
                    { 41, "ER", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Eritrea" },
                    { 42, "ES", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Spain" },
                    { 43, "ET", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Ethiopia" },
                    { 44, "FI", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Finland" },
                    { 45, "FR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "France" },
                    { 46, "GB", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "United Kingdom" },
                    { 47, "GE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3050), "Georgia" },
                    { 48, "GF", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "French Guiana" },
                    { 49, "GH", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Ghana" },
                    { 50, "GL", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Greenland" },
                    { 51, "GP", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Guadeloupe" },
                    { 52, "GR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Greece" },
                    { 53, "GT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Guatemala" },
                    { 54, "GY", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Guyana" },
                    { 55, "HN", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Honduras" },
                    { 56, "HR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Croatia" },
                    { 57, "HT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Haiti" },
                    { 58, "HU", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Hungary" },
                    { 59, "ID", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Indonesia" },
                    { 60, "IE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Ireland" },
                    { 61, "IN", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "India" },
                    { 62, "IR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Iran" },
                    { 63, "IS", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Iceland" },
                    { 64, "IT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Italy" },
                    { 65, "JM", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Jamaica" },
                    { 66, "JP", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Japan" },
                    { 67, "KE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), "Kenya" },
                    { 68, "KG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3060), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "South Korea" },
                    { 70, "KW", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Kuwait" },
                    { 71, "KZ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Kazakhstan" },
                    { 72, "LU", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Luxembourg" },
                    { 73, "LT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Lithuania" },
                    { 74, "LV", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Latvia" },
                    { 75, "MA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Morocco" },
                    { 76, "MC", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Monaco" },
                    { 77, "MD", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Moldova" },
                    { 78, "MF", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Saint Martin" },
                    { 79, "MG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Madagascar" },
                    { 80, "MQ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Martinique" },
                    { 81, "MT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Malta" },
                    { 82, "MX", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Mexico" },
                    { 83, "MY", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Malaysia" },
                    { 84, "NG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Nigeria" },
                    { 85, "NI", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Nicaragua" },
                    { 86, "NL", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Netherlands" },
                    { 87, "NO", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), "Norway" },
                    { 88, "NZ", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3070), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "New Zealand" },
                    { 89, "OM", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Oman" },
                    { 90, "PA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Panama" },
                    { 91, "PE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Peru" },
                    { 92, "PL", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Poland" },
                    { 93, "PM", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Puerto Rico" },
                    { 95, "PT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Portugal" },
                    { 96, "PY", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Paraguay" },
                    { 97, "QA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Qatar" },
                    { 98, "RO", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Romania" },
                    { 99, "RS", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Serbia" },
                    { 100, "RU", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Russia" },
                    { 101, "SA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Sweden" },
                    { 103, "SG", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Singapore" },
                    { 104, "SI", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Slovenia" },
                    { 105, "SK", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Slovakia" },
                    { 106, "SN", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Senegal" },
                    { 107, "SR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "Suriname" },
                    { 108, "SV", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3080), "El Salvador" },
                    { 109, "TR", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Turkey" },
                    { 110, "TT", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Ukraine" },
                    { 112, "US", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "United States" },
                    { 113, "UY", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Uruguay" },
                    { 114, "VE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Venezuela" },
                    { 115, "VI", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Virgin Islands" },
                    { 116, "VN", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Vietnam" },
                    { 117, "YE", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Yemen" },
                    { 118, "ZA", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "South Africa" },
                    { 119, "ZM", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Zambia" },
                    { 120, "ZW", new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(3090), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "SalesChannel",
                columns: new[] { "Id", "DateCreated", "DateModified", "ExportCustomers", "ExportOrders", "ExportProducts", "ImportCustomers", "ImportOrders", "ImportProducts", "InitialProductExportCompleted", "InitialProductImportCompleted", "Name", "Password", "Type", "Url", "Username" },
                values: new object[] { 1, new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(2950), new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(2950), false, false, false, false, false, false, false, false, "Kasse Ladengeschäft", "", 1, "", "" });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9060), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9060), "Company.Name", "Musterfirma GmbH" },
                    { 2, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Address", "Musterstraße 123" },
                    { 3, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.ZipCity", "12345 Musterstadt" },
                    { 4, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Country", "Deutschland" },
                    { 5, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Phone", "+49 123 456789" },
                    { 6, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Email", "info@musterfirma.de" },
                    { 7, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Website", "www.musterfirma.de" },
                    { 8, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.TaxId", "123/456/7890" },
                    { 9, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.VatId", "DE123456789" },
                    { 10, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.BankName", "Musterbank" },
                    { 11, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Iban", "DE89 3704 0044 0532 0130 00" },
                    { 12, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.Bic", "MUSTDEXXX" },
                    { 13, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Company.LogoPath", "" },
                    { 14, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9240), "Jwt.Key", "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 15, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Jwt.Issuer", "maERP.Server" },
                    { 16, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Jwt.Audience", "maERP.Client" },
                    { 17, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Jwt.DurationInMinutes", "60" },
                    { 18, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Jwt.RefreshTokenExpireDays", "7" },
                    { 19, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Email.ApiKey", "Sendgrid-Key" },
                    { 20, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Email.FromAddress", "no-reply@martin-andrich.de" },
                    { 21, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Email.FromName", "maERP" },
                    { 22, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9250), "Telemetry.Endpoint", "http://localhost:4317" },
                    { 23, new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 7, 26, 20, 22, 25, 917, DateTimeKind.Utc).AddTicks(9260), "Telemetry.ServiceName", "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "TaxClass",
                columns: new[] { "Id", "DateCreated", "DateModified", "TaxRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(5300), new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(5300), 19.0 },
                    { 2, new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(5390), 7.0 },
                    { 3, new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 7, 26, 20, 22, 25, 914, DateTimeKind.Utc).AddTicks(5390), 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(4720), new DateTime(2025, 7, 26, 20, 22, 25, 913, DateTimeKind.Utc).AddTicks(4720), "Hauptlager" });

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
                name: "Product");

            migrationBuilder.DropTable(
                name: "SalesChannel");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "ShippingProvider");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "TaxClass");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
