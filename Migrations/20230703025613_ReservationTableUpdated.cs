using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ReservationTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoneReservations");

            migrationBuilder.AddColumn<string>(
                name: "ParkingPlaceId",
                table: "Reservations",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleNumber",
                table: "Reservations",
                type: "varchar(8)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SlotReservationHistories_ReservationId",
                table: "SlotReservationHistories",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingPlaceId",
                table: "Reservations",
                column: "ParkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VehicleNumber",
                table: "Reservations",
                column: "VehicleNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingPlaces_ParkingPlaceId",
                table: "Reservations",
                column: "ParkingPlaceId",
                principalTable: "ParkingPlaces",
                principalColumn: "ParkingPlaceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Vehicles_VehicleNumber",
                table: "Reservations",
                column: "VehicleNumber",
                principalTable: "Vehicles",
                principalColumn: "VehicleNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SlotReservationHistories_Reservations_ReservationId",
                table: "SlotReservationHistories",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingPlaces_ParkingPlaceId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Vehicles_VehicleNumber",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_SlotReservationHistories_Reservations_ReservationId",
                table: "SlotReservationHistories");

            migrationBuilder.DropIndex(
                name: "IX_SlotReservationHistories_ReservationId",
                table: "SlotReservationHistories");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ParkingPlaceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_VehicleNumber",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ParkingPlaceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "VehicleNumber",
                table: "Reservations");

            migrationBuilder.CreateTable(
                name: "ZoneReservations",
                columns: table => new
                {
                    ReservationId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    VehicleNumber = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneReservations", x => new { x.ReservationId, x.ParkingPlaceId, x.VehicleNumber });
                    table.ForeignKey(
                        name: "FK_ZoneReservations_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoneReservations_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "ReservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoneReservations_Vehicles_VehicleNumber",
                        column: x => x.VehicleNumber,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZoneReservations_ParkingPlaceId",
                table: "ZoneReservations",
                column: "ParkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneReservations_ReservationId",
                table: "ZoneReservations",
                column: "ReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZoneReservations_VehicleNumber",
                table: "ZoneReservations",
                column: "VehicleNumber");
        }
    }
}
