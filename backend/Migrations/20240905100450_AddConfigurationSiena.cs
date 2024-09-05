using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace EcoSensorApi.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationSiena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Geometry>(
                name: "geom",
                table: "osm_vector",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geography");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "osm_properties",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string[]>(
                name: "tags",
                table: "osm_properties",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "osm_properties",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Geometry>(
                name: "geom",
                table: "air_quality",
                type: "geometry",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geography");

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 1L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(1350));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 2L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 3L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 4L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 5L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 6L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 7L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 8L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 9L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 10L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 11L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 12L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 13L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 14L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7480));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 15L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 16L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 17L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 18L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 19L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 20L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 21L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 22L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 23L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 24L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 25L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 26L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 27L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 28L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 29L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 30L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                table: "layers",
                keyColumn: "id",
                keyValue: 1L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 50, 52, DateTimeKind.Utc).AddTicks(6890));

            migrationBuilder.InsertData(
                table: "layers",
                columns: new[] { "id", "city_code", "city_name", "entity_key", "prov_code", "prov_name", "region_code", "region_name", "search_text", "timestamp" },
                values: new object[] { 2L, 52032, "Siena", "Siena", null, null, 9, null, null, new DateTime(2024, 9, 5, 10, 4, 50, 52, DateTimeKind.Utc).AddTicks(6950) });

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 1L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 758, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 2L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(770));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 3L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 4L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 5L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 6L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(810));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 7L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 8L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 9L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 10L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 11L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(820));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 12L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(830));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 13L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(830));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 14L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(840));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 15L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 16L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 17L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 18L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 19L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(850));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 20L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 21L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 22L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 23L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 24L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(860));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 25L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(870));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 26L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(880));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 27L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(880));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 28L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 29L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 30L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(890));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 31L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 32L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 33L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 34L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 35L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 36L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 37L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 38L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(960));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 39L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 10, 4, 49, 759, DateTimeKind.Utc).AddTicks(970));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "layers",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.AlterColumn<Geometry>(
                name: "geom",
                table: "osm_vector",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geometry");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "osm_properties",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string[]>(
                name: "tags",
                table: "osm_properties",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0],
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "osm_properties",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<Geometry>(
                name: "geom",
                table: "air_quality",
                type: "geography",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geometry");

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 1L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(1560));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 2L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 3L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 4L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 5L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 6L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6110));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 7L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6120));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 8L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6120));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 9L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6120));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 10L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6120));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 11L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 12L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 13L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 14L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6180));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 15L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 16L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 17L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 18L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 19L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 20L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 21L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 22L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 23L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 24L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 25L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6220));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 26L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6240));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 27L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 28L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 29L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "eu_air_quality_index",
                keyColumn: "id",
                keyValue: 30L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "layers",
                keyColumn: "id",
                keyValue: 1L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 518, DateTimeKind.Utc).AddTicks(2770));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 1L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 160, DateTimeKind.Utc).AddTicks(6770));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 2L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 3L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 4L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 5L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 6L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 7L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 8L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 9L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 10L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 11L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 12L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(350));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 13L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 14L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 15L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 16L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(360));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 17L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(370));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 18L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 19L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 20L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 21L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 22L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 23L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 24L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(400));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 25L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 26L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 27L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 28L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(410));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 29L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(420));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 30L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(420));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 31L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(420));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 32L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(430));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 33L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(430));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 34L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(430));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 35L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 36L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 37L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 38L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "us_air_quality_index",
                keyColumn: "id",
                keyValue: 39L,
                column: "timestamp",
                value: new DateTime(2024, 9, 5, 8, 22, 22, 161, DateTimeKind.Utc).AddTicks(540));
        }
    }
}
