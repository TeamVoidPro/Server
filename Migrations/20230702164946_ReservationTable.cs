using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZoneId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZonesZoneId = table.Column<string>(type: "varchar(20)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "varchar(10)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservationType = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservationStatus = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservationCreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationCancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CancellationReason = table.Column<string>(type: "text", nullable: true),
                    SpecialNotes = table.Column<string>(type: "text", nullable: true),
                    ParkingStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingEndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingPlaceOperatorId = table.Column<string>(type: "varchar(20)", nullable: false),
                    SlotId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Employees_ParkingPlaceOperatorId",
                        column: x => x.ParkingPlaceOperatorId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reservations_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "SlotId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reservations_Zones_ZonesZoneId",
                        column: x => x.ZonesZoneId,
                        principalTable: "Zones",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingPlaceOperatorId",
                table: "Reservations",
                column: "ParkingPlaceOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SlotId",
                table: "Reservations",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ZonesZoneId",
                table: "Reservations",
                column: "ZonesZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
