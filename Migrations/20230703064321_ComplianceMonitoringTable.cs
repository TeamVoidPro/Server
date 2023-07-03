using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ComplianceMonitoringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplianceMonitoring",
                columns: table => new
                {
                    ComplianceMonitoringId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ComplianceStatus = table.Column<string>(type: "text", nullable: false),
                    Report = table.Column<string>(type: "text", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: false),
                    ParkingPlaceVerifierId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplianceMonitoring", x => x.ComplianceMonitoringId);
                    table.ForeignKey(
                        name: "FK_ComplianceMonitoring_Employees_ParkingPlaceVerifierId",
                        column: x => x.ParkingPlaceVerifierId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_ComplianceMonitoring_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceMonitoring_ParkingPlaceId",
                table: "ComplianceMonitoring",
                column: "ParkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplianceMonitoring_ParkingPlaceVerifierId",
                table: "ComplianceMonitoring",
                column: "ParkingPlaceVerifierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplianceMonitoring");
        }
    }
}
