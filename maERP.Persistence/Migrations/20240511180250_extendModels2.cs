using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace maERP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class extendModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouse",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Country",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "CountryCode", "DateCreated", "DateModified", "Name" },
                values: new object[,]
                {
                    { 1, "DE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2623), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2626), "Germany" },
                    { 2, "AT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2629), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2630), "Austria" },
                    { 3, "CH", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2631), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2631), "Switzerland" },
                    { 4, "AD", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2632), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2633), "Andorra" },
                    { 5, "AF", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2634), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2634), "Afghanistan" },
                    { 6, "AG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2636), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2636), "Antigua and Barbuda" },
                    { 7, "AL", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2637), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2638), "Albania" },
                    { 8, "AM", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2639), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2639), "Armenia" },
                    { 9, "AO", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2640), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2641), "Angola" },
                    { 10, "AX", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2642), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2642), "Åland Islands" },
                    { 11, "AR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2643), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2644), "Argentina" },
                    { 12, "AT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2645), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2645), "Antarctica" },
                    { 13, "AU", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2646), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2647), "Australia" },
                    { 14, "AZ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2648), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2648), "Azerbaijan" },
                    { 15, "BA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2649), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2650), "Bosnia and Herzegovina" },
                    { 16, "BB", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2651), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2652), "Barbados" },
                    { 17, "BE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2653), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2653), "Belgium" },
                    { 18, "BG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2654), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2655), "Bulgaria" },
                    { 19, "BL", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2656), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2656), "Saint Barthélemy" },
                    { 20, "BO", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2657), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2658), "Bolivia" },
                    { 21, "BR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2659), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2659), "Brazil" },
                    { 22, "BS", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2660), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2661), "Bahamas" },
                    { 23, "BY", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2662), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2662), "Belarus" },
                    { 24, "BZ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2663), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2664), "Belize" },
                    { 25, "CA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2665), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2666), "Canada" },
                    { 26, "CH", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2667), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2667), "Cocos (Keeling) Islands" },
                    { 27, "CI", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2668), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2669), "Ivory Coast" },
                    { 28, "CL", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2670), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2670), "Chile" },
                    { 29, "CN", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2671), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2672), "China" },
                    { 30, "CO", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2673), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2673), "Colombia" },
                    { 31, "CR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2675), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2675), "Costa Rica" },
                    { 32, "CU", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2676), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2677), "Cuba" },
                    { 33, "CY", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2678), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2678), "Cyprus" },
                    { 34, "CZ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2679), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2680), "Czech Republic" },
                    { 35, "DO", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2681), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2681), "Dominican Republic" },
                    { 36, "DK", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2682), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2683), "Denmark" },
                    { 37, "DZ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2684), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2684), "Algeria" },
                    { 38, "EC", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2685), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2686), "Ecuador" },
                    { 39, "EE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2687), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2687), "Estonia" },
                    { 40, "EG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2688), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2689), "Egypt" },
                    { 41, "ER", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2690), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2690), "Eritrea" },
                    { 42, "ES", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2692), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2692), "Spain" },
                    { 43, "ET", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2693), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2694), "Ethiopia" },
                    { 44, "FI", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2695), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2695), "Finland" },
                    { 45, "FR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2696), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2697), "France" },
                    { 46, "GB", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2698), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2698), "United Kingdom" },
                    { 47, "GE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2699), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2700), "Georgia" },
                    { 48, "GF", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2701), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2701), "French Guiana" },
                    { 49, "GH", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2703), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2703), "Ghana" },
                    { 50, "GL", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2704), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2705), "Greenland" },
                    { 51, "GP", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2706), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2706), "Guadeloupe" },
                    { 52, "GR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2707), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2708), "Greece" },
                    { 53, "GT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2776), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2777), "Guatemala" },
                    { 54, "GY", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2778), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2779), "Guyana" },
                    { 55, "HN", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2780), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2780), "Honduras" },
                    { 56, "HR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2782), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2782), "Croatia" },
                    { 57, "HT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2783), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2784), "Haiti" },
                    { 58, "HU", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2785), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2785), "Hungary" },
                    { 59, "ID", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2786), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2787), "Indonesia" },
                    { 60, "IE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2788), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2788), "Ireland" },
                    { 61, "IN", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2789), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2790), "India" },
                    { 62, "IR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2791), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2791), "Iran" },
                    { 63, "IS", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2792), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2793), "Iceland" },
                    { 64, "IT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2794), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2794), "Italy" },
                    { 65, "JM", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2796), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2796), "Jamaica" },
                    { 66, "JP", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2797), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2798), "Japan" },
                    { 67, "KE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2799), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2799), "Kenya" },
                    { 68, "KG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2800), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2801), "Kyrgyzstan" },
                    { 69, "KR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2802), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2802), "South Korea" },
                    { 70, "KW", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2803), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2804), "Kuwait" },
                    { 71, "KZ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2805), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2805), "Kazakhstan" },
                    { 72, "LU", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2807), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2807), "Luxembourg" },
                    { 73, "LT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2808), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2809), "Lithuania" },
                    { 74, "LV", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2810), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2810), "Latvia" },
                    { 75, "MA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2811), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2812), "Morocco" },
                    { 76, "MC", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2813), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2813), "Monaco" },
                    { 77, "MD", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2814), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2815), "Moldova" },
                    { 78, "MF", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2816), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2816), "Saint Martin" },
                    { 79, "MG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2817), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2818), "Madagascar" },
                    { 80, "MQ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2819), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2819), "Martinique" },
                    { 81, "MT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2821), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2821), "Malta" },
                    { 82, "MX", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2822), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2823), "Mexico" },
                    { 83, "MY", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2824), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2824), "Malaysia" },
                    { 84, "NG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2825), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2826), "Nigeria" },
                    { 85, "NI", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2827), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2827), "Nicaragua" },
                    { 86, "NL", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2828), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2829), "Netherlands" },
                    { 87, "NO", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2830), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2830), "Norway" },
                    { 88, "NZ", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2831), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2832), "New Zealand" },
                    { 89, "OM", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2833), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2833), "Oman" },
                    { 90, "PA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2834), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2835), "Panama" },
                    { 91, "PE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2836), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2836), "Peru" },
                    { 92, "PL", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2837), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2838), "Poland" },
                    { 93, "PM", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2839), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2840), "Saint Pierre and Miquelon" },
                    { 94, "PR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2841), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2841), "Puerto Rico" },
                    { 95, "PT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2842), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2843), "Portugal" },
                    { 96, "PY", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2844), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2844), "Paraguay" },
                    { 97, "QA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2845), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2846), "Qatar" },
                    { 98, "RO", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2847), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2847), "Romania" },
                    { 99, "RS", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2848), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2849), "Serbia" },
                    { 100, "RU", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2850), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2850), "Russia" },
                    { 101, "SA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2851), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2852), "Saudi Arabia" },
                    { 102, "SE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2853), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2853), "Sweden" },
                    { 103, "SG", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2855), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2855), "Singapore" },
                    { 104, "SI", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2856), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2857), "Slovenia" },
                    { 105, "SK", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2858), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2858), "Slovakia" },
                    { 106, "SN", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2859), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2860), "Senegal" },
                    { 107, "SR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2861), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2861), "Suriname" },
                    { 108, "SV", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2862), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2863), "El Salvador" },
                    { 109, "TR", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2864), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2864), "Turkey" },
                    { 110, "TT", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2866), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2866), "Trinidad and Tobago" },
                    { 111, "UA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2867), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2868), "Ukraine" },
                    { 112, "US", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2869), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2869), "United States" },
                    { 113, "UY", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2870), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2871), "Uruguay" },
                    { 114, "VE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2872), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2872), "Venezuela" },
                    { 115, "VI", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2873), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2874), "Virgin Islands" },
                    { 116, "VN", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2875), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2875), "Vietnam" },
                    { 117, "YE", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2876), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2877), "Yemen" },
                    { 118, "ZA", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2878), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2878), "South Africa" },
                    { 119, "ZM", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2880), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2880), "Zambia" },
                    { 120, "ZW", new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2881), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(2882), "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "Warehouse",
                columns: new[] { "Id", "DateCreated", "DateModified", "Name" },
                values: new object[] { 1, new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(4115), new DateTime(2024, 5, 11, 18, 2, 49, 667, DateTimeKind.Utc).AddTicks(4115), "Warehouse 1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Warehouse",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouse",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Country",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
