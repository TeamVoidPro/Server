using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ParkingPlaceRatingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingPlaceRatings",
                columns: table => new
                {
                    RatingId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    RatingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    DriverId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPlaceRatings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_ParkingPlaceRatings_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingPlaceRatings_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaceServices_ParkingPlaceId_ServiceProvide",
                table: "ParkingPlaceServices",
                columns: new[] { "ParkingPlaceId", "ServiceProvide" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaces_ParkingPlaceId_Name_Location_ParkingPlaceOper~",
                table: "ParkingPlaces",
                columns: new[] { "ParkingPlaceId", "Name", "Location", "ParkingPlaceOperatorId", "ParkingPlaceVerifierId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaceImages_ParkingPlaceId_ImageUrl",
                table: "ParkingPlaceImages",
                columns: new[] { "ParkingPlaceId", "ImageUrl" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaceRatings_DriverId",
                table: "ParkingPlaceRatings",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaceRatings_ParkingPlaceId",
                table: "ParkingPlaceRatings",
                column: "ParkingPlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingPlaceRatings");

            migrationBuilder.DropIndex(
                name: "IX_ParkingPlaceServices_ParkingPlaceId_ServiceProvide",
                table: "ParkingPlaceServices");

            migrationBuilder.DropIndex(
                name: "IX_ParkingPlaces_ParkingPlaceId_Name_Location_ParkingPlaceOper~",
                table: "ParkingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_ParkingPlaceImages_ParkingPlaceId_ImageUrl",
                table: "ParkingPlaceImages");
        }
    }
}
