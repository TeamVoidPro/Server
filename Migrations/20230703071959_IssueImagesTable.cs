using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class IssueImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    IssueId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReportedBy = table.Column<string>(type: "varchar(20)", nullable: false),
                    IssueDescription = table.Column<string>(type: "text", nullable: false),
                    ReportedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RespondedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Response = table.Column<string>(type: "text", nullable: false),
                    ParkingPlaceVerifierId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK_Issues_Drivers_ReportedBy",
                        column: x => x.ReportedBy,
                        principalTable: "Drivers",
                        principalColumn: "DriverId");
                    table.ForeignKey(
                        name: "FK_Issues_Employees_ParkingPlaceVerifierId",
                        column: x => x.ParkingPlaceVerifierId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Issues_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId");
                });

            migrationBuilder.CreateTable(
                name: "IssueImages",
                columns: table => new
                {
                    IssueId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueImages", x => new { x.IssueId, x.Image });
                    table.ForeignKey(
                        name: "FK_IssueImages_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ParkingPlaceId",
                table: "Issues",
                column: "ParkingPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ParkingPlaceVerifierId",
                table: "Issues",
                column: "ParkingPlaceVerifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReportedBy",
                table: "Issues",
                column: "ReportedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueImages");

            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
