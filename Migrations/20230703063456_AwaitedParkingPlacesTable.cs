using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AwaitedParkingPlacesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AwaitedParkingPlaces",
                columns: table => new
                {
                    AwaitedParkingPlacesId = table.Column<string>(type: "varchar(20)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    DeedCopy = table.Column<string>(type: "text", nullable: false),
                    ConfirmationStatus = table.Column<string>(type: "text", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmationReport = table.Column<string>(type: "text", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RejectionReason = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParkingPlaceVerifierId = table.Column<string>(type: "varchar(20)", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AwaitedParkingPlaces", x => x.AwaitedParkingPlacesId);
                    table.ForeignKey(
                        name: "FK_AwaitedParkingPlaces_Employees_ParkingPlaceVerifierId",
                        column: x => x.ParkingPlaceVerifierId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_AwaitedParkingPlaces_ParkingPlaceOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ParkingPlaceOwners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AwaitedParkingPlaces_OwnerId",
                table: "AwaitedParkingPlaces",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AwaitedParkingPlaces_ParkingPlaceVerifierId",
                table: "AwaitedParkingPlaces",
                column: "ParkingPlaceVerifierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AwaitedParkingPlaces");
        }
    }
}
