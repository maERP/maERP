﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using maERP.Server.Models;

#nullable disable

namespace maERP.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230804183748_initDatabase")]
    partial class initDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "341743f0-asd2–42de-afbf-59kmkkmk21ab1",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "05474ea3–7543-8aef-bcae-33e812c35fc3",
                            RoleId = "02174cf0–9412–4cfe-afbf-59f706d72cf6"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("maERP.Server.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "05474ea3–7543-8aef-bcae-33e812c35fc3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a213c062-6c2c-4bb1-9aa7-cab8bd3396ae",
                            Email = "admin@localhost.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@LOCALHOST.COM",
                            NormalizedUserName = "ADMIN@LOCALHOST.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEN1fPCshaR+sQS2MqTJHHMQ0zm2jL3Y9ur+uXutpPRDb4fVrRZ72/3qy3Ua6IHKVMA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3572b235-584a-404f-a864-e499161297c5",
                            TwoFactorEnabled = false,
                            UserName = "admin@localhost.com"
                        });
                });

            modelBuilder.Entity("maERP.Server.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryCode = "de",
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 928, DateTimeKind.Utc).AddTicks(9300),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 928, DateTimeKind.Utc).AddTicks(9300),
                            Name = "Deutschland"
                        },
                        new
                        {
                            Id = 2,
                            CountryCode = "at",
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 928, DateTimeKind.Utc).AddTicks(9310),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 928, DateTimeKind.Utc).AddTicks(9310),
                            Name = "Österreich"
                        },
                        new
                        {
                            Id = 3,
                            CountryCode = "ch",
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 928, DateTimeKind.Utc).AddTicks(9310),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 928, DateTimeKind.Utc).AddTicks(9310),
                            Name = "Schweiz"
                        });
                });

            modelBuilder.Entity("maERP.Server.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("maERP.Server.Models.CustomerAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HouseNr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerAddress");
                });

            modelBuilder.Entity("maERP.Server.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("maERP.Server.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Asin")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ean")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Msrp")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TaxClassId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Sku")
                        .IsUnique();

                    b.HasIndex("TaxClassId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("maERP.Server.Models.ProductSalesChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("RemoteProductId")
                        .HasColumnType("integer");

                    b.Property<int>("SalesChannelId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalesChannelId");

                    b.ToTable("ProductSalesChannel");
                });

            modelBuilder.Entity("maERP.Server.Models.ProductStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ProductStock");
                });

            modelBuilder.Entity("maERP.Server.Models.SalesChannel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("ExportCustomers")
                        .HasColumnType("boolean");

                    b.Property<bool>("ExportOrders")
                        .HasColumnType("boolean");

                    b.Property<bool>("ExportProducts")
                        .HasColumnType("boolean");

                    b.Property<bool>("ImportCustomers")
                        .HasColumnType("boolean");

                    b.Property<bool>("ImportOrders")
                        .HasColumnType("boolean");

                    b.Property<bool>("ImportProducts")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("SalesChannel");
                });

            modelBuilder.Entity("maERP.Server.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Section")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Setting");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2030),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2030),
                            Key = "JwtSettings:Key",
                            Section = 0,
                            Value = "CHANGE_TO_YOUR_VERY_SECRET_JWT_SIGNING_KEY"
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2030),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2030),
                            Key = "JwtSettings:Issuer",
                            Section = 0,
                            Value = "maERP.Server"
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2030),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2030),
                            Key = "JwtSettings:Audience",
                            Section = 0,
                            Value = "maERP.Client"
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            Key = "JwtSettings:DurationInMinutes",
                            Section = 0,
                            Value = "60"
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            Key = "RemoteLog:Enabled",
                            Section = 0,
                            Value = "false"
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            Key = "RemoteLog:Host",
                            Section = 0,
                            Value = "graylog.martin-andrich.de"
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            Key = "RemoteLog:Port",
                            Section = 0,
                            Value = "12301"
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2050),
                            Key = "RemoteLog:TransportType",
                            Section = 0,
                            Value = "Tcp"
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2060),
                            DateModified = new DateTime(2023, 8, 4, 18, 37, 47, 929, DateTimeKind.Utc).AddTicks(2060),
                            Key = "RemoteLog:Facility",
                            Section = 0,
                            Value = "maERP.Server"
                        });
                });

            modelBuilder.Entity("maERP.Server.Models.ShippingProvider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ShippingProvider");
                });

            modelBuilder.Entity("maERP.Server.Models.ShippingProviderRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("MaxHeight")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MaxLength")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MaxWeight")
                        .HasColumnType("numeric");

                    b.Property<decimal>("MaxWidth")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ShippingProviderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ShippingProviderId");

                    b.ToTable("ShippingProviderRate");
                });

            modelBuilder.Entity("maERP.Server.Models.TaxClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("TaxRate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("TaxClass");
                });

            modelBuilder.Entity("maERP.Server.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("maERP.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("maERP.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maERP.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("maERP.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("maERP.Server.Models.CustomerAddress", b =>
                {
                    b.HasOne("maERP.Server.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maERP.Server.Models.Customer", "Customer")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("maERP.Server.Models.Order", b =>
                {
                    b.HasOne("maERP.Server.Models.Customer", null)
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("maERP.Server.Models.Product", b =>
                {
                    b.HasOne("maERP.Server.Models.TaxClass", "TaxClass")
                        .WithMany("Products")
                        .HasForeignKey("TaxClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxClass");
                });

            modelBuilder.Entity("maERP.Server.Models.ProductSalesChannel", b =>
                {
                    b.HasOne("maERP.Server.Models.Product", "Product")
                        .WithMany("ProductSalesChannel")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maERP.Server.Models.SalesChannel", "SalesChannel")
                        .WithMany()
                        .HasForeignKey("SalesChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SalesChannel");
                });

            modelBuilder.Entity("maERP.Server.Models.ProductStock", b =>
                {
                    b.HasOne("maERP.Server.Models.Product", "Product")
                        .WithMany("ProductStock")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("maERP.Server.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("maERP.Server.Models.SalesChannel", b =>
                {
                    b.HasOne("maERP.Server.Models.Warehouse", "Warehouse")
                        .WithMany("SalesChannels")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("maERP.Server.Models.ShippingProviderRate", b =>
                {
                    b.HasOne("maERP.Server.Models.ShippingProvider", null)
                        .WithMany("ShippingRates")
                        .HasForeignKey("ShippingProviderId");
                });

            modelBuilder.Entity("maERP.Server.Models.Customer", b =>
                {
                    b.Navigation("CustomerAddresses");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("maERP.Server.Models.Product", b =>
                {
                    b.Navigation("ProductSalesChannel");

                    b.Navigation("ProductStock");
                });

            modelBuilder.Entity("maERP.Server.Models.ShippingProvider", b =>
                {
                    b.Navigation("ShippingRates");
                });

            modelBuilder.Entity("maERP.Server.Models.TaxClass", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("maERP.Server.Models.Warehouse", b =>
                {
                    b.Navigation("SalesChannels");
                });
#pragma warning restore 612, 618
        }
    }
}