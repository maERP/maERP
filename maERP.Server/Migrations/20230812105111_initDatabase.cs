﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Server.Migrations
{
    /// <inheritdoc />
    public partial class initDatabase : Migration
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
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
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
                    Name = table.Column<string>(type: "text", nullable: false),
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
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CustomerStatus = table.Column<int>(type: "integer", nullable: false),
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
                    Name = table.Column<string>(type: "text", nullable: false),
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
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNr = table.Column<string>(type: "text", nullable: false),
                    Zip = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
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
                    Name = table.Column<string>(type: "text", nullable: false),
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
                    { "02174cf0–9412–4cfe-afbf-59f706d72cf6", null, "Admin", "ADMIN" },
                    { "341743f0-asd2–42de-afbf-59kmkkmk21ab1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "05474ea3–7543-8aef-bcae-33e812c35fc3", 0, "3edb3943-97db-4087-9d63-e971a16ae523", "admin@localhost.com", false, "Admin", "Admin", false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEKUhukBrvWhW3G1x1OctntttGIyRifidj/BvOiufsN288qdsQE58ALwZRsUbuHy5Gw==", null, false, "8992ec17-a985-4d05-a68c-4cde18a4de5d", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "de", new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(3711), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(3715), "Deutschland" },
                    { 2, "at", new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(3717), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(3718), "Österreich" },
                    { 3, "ch", new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(3718), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(3719), "Schweiz" }
                });

            migrationBuilder.InsertData(
                table: "Setting",
                columns: new[] { "Id", "DateCreated", "DateModified", "Key", "Section", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8096), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8097), "JwtSettings:Key", 0, "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY" },
                    { 2, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8099), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8099), "JwtSettings:Issuer", 0, "maERP.Server" },
                    { 3, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8100), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8100), "JwtSettings:Audience", 0, "maERP.Client" },
                    { 4, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8101), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8101), "JwtSettings:DurationInMinutes", 0, "60" },
                    { 5, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8102), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8102), "RemoteLog:Enabled", 0, "false" },
                    { 6, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8103), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8103), "RemoteLog:Host", 0, "graylog.martin-andrich.de" },
                    { 7, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8104), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8104), "RemoteLog:Port", 0, "12301" },
                    { 8, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8131), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8132), "RemoteLog:TransportType", 0, "Tcp" },
                    { 9, new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8133), new DateTime(2023, 8, 12, 10, 51, 11, 254, DateTimeKind.Utc).AddTicks(8133), "RemoteLog:Facility", 0, "maERP.Server" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "02174cf0–9412–4cfe-afbf-59f706d72cf6", "05474ea3–7543-8aef-bcae-33e812c35fc3" });

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
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductSalesChannel");

            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ShippingProviderRate");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "SalesChannel");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShippingProvider");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "TaxClass");
        }
    }
}
