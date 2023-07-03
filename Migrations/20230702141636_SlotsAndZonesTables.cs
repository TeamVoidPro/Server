using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SlotsAndZonesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ZoneId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZoneName = table.Column<string>(type: "text", nullable: false),
                    ZoneDescription = table.Column<string>(type: "text", nullable: true),
                    ZoneCreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ZoneId);
                    table.ForeignKey(
                        name: "FK_Zones_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotId = table.Column<string>(type: "varchar(20)", nullable: false),
                    SlotNumber = table.Column<int>(type: "integer", nullable: false),
                    SlotCategoryId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZoneId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZonesZoneId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    SlotCreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservedUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_Slots_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_SlotCategories_SlotCategoryId",
                        column: x => x.SlotCategoryId,
                        principalTable: "SlotCategories",
                        principalColumn: "SlotCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Slots_Zones_ZonesZoneId",
                        column: x => x.ZonesZoneId,
                        principalTable: "Zones",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ParkingPlaceId",
                table: "Slots",
                column: "ParkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_SlotCategoryId",
                table: "Slots",
                column: "SlotCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ZonesZoneId",
                table: "Slots",
                column: "ZonesZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_ParkingPlaceId",
                table: "Zones",
                column: "ParkingPlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}
