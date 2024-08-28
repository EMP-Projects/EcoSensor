﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoSensorApi.Migrations
{
    /// <inheritdoc />
    public partial class InitEcoSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "air_quality",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_data = table.Column<int>(type: "integer", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lng = table.Column<double>(type: "double precision", nullable: false),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    geom = table.Column<Geometry>(type: "geography", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_air_quality", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eu_air_quality_index",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    period = table.Column<TimeSpan>(type: "interval", nullable: false),
                    min = table.Column<double>(type: "double precision", nullable: false),
                    max = table.Column<double>(type: "double precision", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false),
                    pollution = table.Column<int>(type: "integer", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eu_air_quality_index", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "layers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    region_name = table.Column<string>(type: "text", nullable: true),
                    region_code = table.Column<int>(type: "integer", nullable: true),
                    prov_name = table.Column<string>(type: "text", nullable: true),
                    prov_code = table.Column<int>(type: "integer", nullable: true),
                    city_code = table.Column<int>(type: "integer", nullable: true),
                    city_name = table.Column<string>(type: "text", nullable: true),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_layers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "osm_properties",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "text", nullable: false),
                    tags = table.Column<string[]>(type: "text[]", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_osm_properties", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "us_air_quality_index",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    period = table.Column<TimeSpan>(type: "interval", nullable: false),
                    min = table.Column<double>(type: "double precision", nullable: false),
                    max = table.Column<double>(type: "double precision", nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    pollution = table.Column<int>(type: "integer", nullable: false),
                    unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_us_air_quality_index", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "air_quality_measures",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lng = table.Column<double>(type: "double precision", nullable: false),
                    value = table.Column<double>(type: "double precision", nullable: false),
                    unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    european_aqi = table.Column<long>(type: "bigint", nullable: true),
                    us_aqi = table.Column<long>(type: "bigint", nullable: true),
                    elevation = table.Column<double>(type: "double precision", nullable: false),
                    source = table.Column<int>(type: "integer", nullable: false),
                    pollution = table.Column<int>(type: "integer", nullable: false),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    gis_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_air_quality_measures", x => x.id);
                    table.ForeignKey(
                        name: "FK_air_quality_measures_air_quality_gis_id",
                        column: x => x.gis_id,
                        principalTable: "air_quality",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "osm_vector",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    entity_key = table.Column<string>(type: "text", nullable: false),
                    search_text = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: true),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    geom = table.Column<Geometry>(type: "geography", nullable: false),
                    id_properties = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_osm_vector", x => x.id);
                    table.ForeignKey(
                        name: "FK_osm_vector_osm_properties_id_properties",
                        column: x => x.id_properties,
                        principalTable: "osm_properties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "eu_air_quality_index",
                columns: new[] { "id", "color", "entity_key", "level", "max", "min", "period", "pollution", "search_text", "timestamp", "unit" },
                values: new object[,]
                {
                    { 1L, "#47EEE0", "Eu AirQuality Level", 0, 10.0, 0.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(4190), "μg/m3" },
                    { 2L, "#44C39A", "Eu AirQuality Level", 1, 20.0, 10.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8540), "μg/m3" },
                    { 3L, "#ECE433", "Eu AirQuality Level", 2, 25.0, 20.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8540), "μg/m3" },
                    { 4L, "#E8333C", "Eu AirQuality Level", 3, 50.0, 25.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8540), "μg/m3" },
                    { 5L, "#820026", "Eu AirQuality Level", 4, 75.0, 50.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8550), "μg/m3" },
                    { 6L, "#680D6D", "Eu AirQuality Level", 5, 800.0, 75.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8550), "μg/m3" },
                    { 7L, "#47EEE0", "Eu AirQuality Level", 0, 20.0, 0.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8550), "μg/m3" },
                    { 8L, "#44C39A", "Eu AirQuality Level", 1, 40.0, 20.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8550), "μg/m3" },
                    { 9L, "#ECE433", "Eu AirQuality Level", 2, 50.0, 40.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8560), "μg/m3" },
                    { 10L, "#E8333C", "Eu AirQuality Level", 3, 100.0, 50.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8560), "μg/m3" },
                    { 11L, "#820026", "Eu AirQuality Level", 4, 150.0, 100.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8560), "μg/m3" },
                    { 12L, "#680D6D", "Eu AirQuality Level", 5, 1200.0, 150.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8590), "μg/m3" },
                    { 13L, "#47EEE0", "Eu AirQuality Level", 0, 40.0, 0.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8590), "μg/m3" },
                    { 14L, "#44C39A", "Eu AirQuality Level", 1, 90.0, 40.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8610), "μg/m3" },
                    { 15L, "#ECE433", "Eu AirQuality Level", 2, 120.0, 90.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8620), "μg/m3" },
                    { 16L, "#E8333C", "Eu AirQuality Level", 3, 230.0, 120.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8620), "μg/m3" },
                    { 17L, "#820026", "Eu AirQuality Level", 4, 340.0, 230.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8620), "μg/m3" },
                    { 18L, "#680D6D", "Eu AirQuality Level", 5, 1000.0, 340.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8620), "μg/m3" },
                    { 19L, "#47EEE0", "Eu AirQuality Level", 0, 50.0, 0.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8620), "μg/m3" },
                    { 20L, "#44C39A", "Eu AirQuality Level", 1, 100.0, 50.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8630), "μg/m3" },
                    { 21L, "#ECE433", "Eu AirQuality Level", 2, 130.0, 100.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8630), "μg/m3" },
                    { 22L, "#E8333C", "Eu AirQuality Level", 3, 240.0, 130.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8630), "μg/m3" },
                    { 23L, "#820026", "Eu AirQuality Level", 4, 380.0, 240.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8630), "μg/m3" },
                    { 24L, "#680D6D", "Eu AirQuality Level", 5, 800.0, 380.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8650), "μg/m3" },
                    { 25L, "#47EEE0", "Eu AirQuality Level", 0, 100.0, 0.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8660), "μg/m3" },
                    { 26L, "#44C39A", "Eu AirQuality Level", 1, 200.0, 100.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8660), "μg/m3" },
                    { 27L, "#ECE433", "Eu AirQuality Level", 2, 350.0, 200.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8660), "μg/m3" },
                    { 28L, "#E8333C", "Eu AirQuality Level", 3, 500.0, 350.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8660), "μg/m3" },
                    { 29L, "#820026", "Eu AirQuality Level", 4, 750.0, 500.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8670), "μg/m3" },
                    { 30L, "#680D6D", "Eu AirQuality Level", 5, 1250.0, 750.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(8670), "μg/m3" }
                });

            migrationBuilder.InsertData(
                table: "layers",
                columns: new[] { "id", "city_code", "city_name", "entity_key", "prov_code", "prov_name", "region_code", "region_name", "search_text", "timestamp" },
                values: new object[] { 1L, 72021, "Gioia del Colle", "Gioia del Colle", null, null, 16, null, null, new DateTime(2024, 8, 28, 16, 2, 57, 96, DateTimeKind.Utc).AddTicks(8020) });

            migrationBuilder.InsertData(
                table: "us_air_quality_index",
                columns: new[] { "id", "color", "entity_key", "level", "max", "min", "period", "pollution", "search_text", "timestamp", "unit" },
                values: new object[,]
                {
                    { 1L, "#47EEE0", "Us AirQuality Level", 0, 55.0, 0.0, new TimeSpan(0, 8, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 706, DateTimeKind.Utc).AddTicks(9150), "ppb" },
                    { 2L, "#44C39A", "Us AirQuality Level", 1, 70.0, 55.0, new TimeSpan(0, 8, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2630), "ppb" },
                    { 3L, "#ECE433", "Us AirQuality Level", 2, 85.0, 70.0, new TimeSpan(0, 8, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2640), "ppb" },
                    { 4L, "#E8333C", "Us AirQuality Level", 3, 105.0, 85.0, new TimeSpan(0, 8, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2640), "ppb" },
                    { 5L, "#820026", "Us AirQuality Level", 4, 200.0, 105.0, new TimeSpan(0, 8, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2680), "ppb" },
                    { 6L, "#ECE433", "Us AirQuality Level", 2, 165.0, 125.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2680), "ppb" },
                    { 7L, "#E8333C", "Us AirQuality Level", 3, 205.0, 165.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2680), "ppb" },
                    { 8L, "#820026", "Us AirQuality Level", 4, 405.0, 205.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2690), "ppb" },
                    { 9L, "#680D6D", "Us AirQuality Level", 5, 605.0, 405.0, new TimeSpan(0, 1, 0, 0, 0), 3, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2690), "ppb" },
                    { 10L, "#47EEE0", "Us AirQuality Level", 0, 12.0, 0.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2690), "μg/m3" },
                    { 11L, "#44C39A", "Us AirQuality Level", 1, 35.5, 12.0, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2690), "μg/m3" },
                    { 12L, "#ECE433", "Us AirQuality Level", 2, 55.5, 35.5, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2700), "μg/m3" },
                    { 13L, "#E8333C", "Us AirQuality Level", 3, 105.5, 55.5, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2700), "μg/m3" },
                    { 14L, "#820026", "Us AirQuality Level", 4, 250.5, 150.5, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2700), "μg/m3" },
                    { 15L, "#680D6D", "Us AirQuality Level", 5, 500.5, 250.5, new TimeSpan(1, 0, 0, 0, 0), 7, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2700), "μg/m3" },
                    { 16L, "#47EEE0", "Us AirQuality Level", 0, 55.0, 0.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2710), "μg/m3" },
                    { 17L, "#44C39A", "Us AirQuality Level", 1, 155.0, 55.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2730), "μg/m3" },
                    { 18L, "#ECE433", "Us AirQuality Level", 2, 255.0, 155.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2730), "μg/m3" },
                    { 19L, "#E8333C", "Us AirQuality Level", 3, 355.0, 255.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2730), "μg/m3" },
                    { 20L, "#820026", "Us AirQuality Level", 4, 425.0, 355.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2730), "μg/m3" },
                    { 21L, "#680D6D", "Us AirQuality Level", 5, 605.0, 425.0, new TimeSpan(1, 0, 0, 0, 0), 6, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2740), "μg/m3" },
                    { 22L, "#47EEE0", "Us AirQuality Level", 0, 4.5, 0.0, new TimeSpan(0, 8, 0, 0, 0), 0, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2740), "ppm" },
                    { 23L, "#44C39A", "Us AirQuality Level", 1, 9.5, 4.5, new TimeSpan(0, 8, 0, 0, 0), 0, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2740), "ppm" },
                    { 24L, "#ECE433", "Us AirQuality Level", 2, 12.5, 9.5, new TimeSpan(0, 8, 0, 0, 0), 0, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2740), "ppm" },
                    { 25L, "#E8333C", "Us AirQuality Level", 3, 15.5, 12.5, new TimeSpan(0, 8, 0, 0, 0), 0, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2750), "ppm" },
                    { 26L, "#820026", "Us AirQuality Level", 4, 30.5, 15.5, new TimeSpan(0, 8, 0, 0, 0), 0, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2750), "ppm" },
                    { 27L, "#680D6D", "Us AirQuality Level", 5, 50.5, 30.5, new TimeSpan(0, 8, 0, 0, 0), 0, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2750), "ppm" },
                    { 28L, "#47EEE0", "Us AirQuality Level", 0, 35.0, 0.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2770), "ppb" },
                    { 29L, "#44C39A", "Us AirQuality Level", 1, 75.0, 35.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2770), "ppb" },
                    { 30L, "#ECE433", "Us AirQuality Level", 2, 185.0, 75.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2770), "ppb" },
                    { 31L, "#E8333C", "Us AirQuality Level", 3, 305.0, 185.0, new TimeSpan(0, 1, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2780), "ppb" },
                    { 32L, "#820026", "Us AirQuality Level", 4, 605.0, 305.0, new TimeSpan(1, 0, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2780), "ppb" },
                    { 33L, "#680D6D", "Us AirQuality Level", 5, 1005.0, 605.0, new TimeSpan(1, 0, 0, 0, 0), 2, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2780), "ppb" },
                    { 34L, "#47EEE0", "Us AirQuality Level", 0, 54.0, 0.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2780), "ppb" },
                    { 35L, "#44C39A", "Us AirQuality Level", 1, 100.0, 54.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2790), "ppb" },
                    { 36L, "#ECE433", "Us AirQuality Level", 2, 360.0, 100.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2790), "ppb" },
                    { 37L, "#E8333C", "Us AirQuality Level", 3, 650.0, 360.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2790), "ppb" },
                    { 38L, "#820026", "Us AirQuality Level", 4, 1250.0, 650.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2790), "ppb" },
                    { 39L, "#680D6D", "Us AirQuality Level", 5, 2050.0, 1250.0, new TimeSpan(0, 1, 0, 0, 0), 1, null, new DateTime(2024, 8, 28, 16, 2, 56, 707, DateTimeKind.Utc).AddTicks(2800), "ppb" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_air_quality_entity_key",
                table: "air_quality",
                column: "entity_key");

            migrationBuilder.CreateIndex(
                name: "IX_air_quality_guid",
                table: "air_quality",
                column: "guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_air_quality_measures_entity_key",
                table: "air_quality_measures",
                column: "entity_key");

            migrationBuilder.CreateIndex(
                name: "IX_air_quality_measures_gis_id",
                table: "air_quality_measures",
                column: "gis_id");

            migrationBuilder.CreateIndex(
                name: "IX_eu_air_quality_index_entity_key",
                table: "eu_air_quality_index",
                column: "entity_key");

            migrationBuilder.CreateIndex(
                name: "IX_layers_entity_key",
                table: "layers",
                column: "entity_key");

            migrationBuilder.CreateIndex(
                name: "IX_osm_properties_entity_key",
                table: "osm_properties",
                column: "entity_key");

            migrationBuilder.CreateIndex(
                name: "IX_osm_vector_entity_key",
                table: "osm_vector",
                column: "entity_key");

            migrationBuilder.CreateIndex(
                name: "IX_osm_vector_guid",
                table: "osm_vector",
                column: "guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_osm_vector_id_properties",
                table: "osm_vector",
                column: "id_properties");

            migrationBuilder.CreateIndex(
                name: "IX_us_air_quality_index_entity_key",
                table: "us_air_quality_index",
                column: "entity_key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "air_quality_measures");

            migrationBuilder.DropTable(
                name: "eu_air_quality_index");

            migrationBuilder.DropTable(
                name: "layers");

            migrationBuilder.DropTable(
                name: "osm_vector");

            migrationBuilder.DropTable(
                name: "us_air_quality_index");

            migrationBuilder.DropTable(
                name: "air_quality");

            migrationBuilder.DropTable(
                name: "osm_properties");
        }
    }
}