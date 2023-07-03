using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class BookingReservationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingPlans",
                columns: table => new
                {
                    BookingPlanId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    PlanDuration = table.Column<string>(type: "text", nullable: false),
                    PlanName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPlans", x => x.BookingPlanId);
                    table.ForeignKey(
                        name: "FK_BookingPlans_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingReservations",
                columns: table => new
                {
                    BookingReservationId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZonePlanId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalPayment = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VehicleNumber = table.Column<string>(type: "varchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingReservations", x => x.BookingReservationId);
                    table.ForeignKey(
                        name: "FK_BookingReservations_Vehicles_VehicleNumber",
                        column: x => x.VehicleNumber,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "varchar(20)", nullable: false),
                    QrCode = table.Column<string>(type: "text", nullable: false),
                    Validity = table.Column<string>(type: "text", nullable: false),
                    TicketCreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TicketExpiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VerifiedBy = table.Column<string>(type: "varchar(20)", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Employees_VerifiedBy",
                        column: x => x.VerifiedBy,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "ZonePlans",
                columns: table => new
                {
                    ZonePlanId = table.Column<string>(type: "varchar(20)", nullable: false),
                    BookingPlanId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ZoneId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonePlans", x => new { x.ZonePlanId, x.BookingPlanId });
                    table.ForeignKey(
                        name: "FK_ZonePlans_BookingPlans_BookingPlanId",
                        column: x => x.BookingPlanId,
                        principalTable: "BookingPlans",
                        principalColumn: "BookingPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZonePlans_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "ZoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingParkings",
                columns: table => new
                {
                    BookingReservationId = table.Column<string>(type: "varchar(20)", nullable: false),
                    SlotId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParkingId = table.Column<string>(type: "varchar(20)", nullable: true),
                    ParkingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkedDuration = table.Column<string>(type: "text", nullable: false),
                    IsParkOnNextDay = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingParkings", x => new { x.BookingReservationId, x.SlotId });
                    table.ForeignKey(
                        name: "FK_BookingParkings_BookingReservations_BookingReservationId",
                        column: x => x.BookingReservationId,
                        principalTable: "BookingReservations",
                        principalColumn: "BookingReservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingParkings_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "SlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingParkings_SlotId",
                table: "BookingParkings",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPlans_ParkingPlaceId",
                table: "BookingPlans",
                column: "ParkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReservations_VehicleNumber",
                table: "BookingReservations",
                column: "VehicleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VerifiedBy",
                table: "Tickets",
                column: "VerifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePlans_BookingPlanId",
                table: "ZonePlans",
                column: "BookingPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePlans_ZoneId",
                table: "ZonePlans",
                column: "ZoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingParkings");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "ZonePlans");

            migrationBuilder.DropTable(
                name: "BookingReservations");

            migrationBuilder.DropTable(
                name: "BookingPlans");
        }
    }
}
