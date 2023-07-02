using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ParkingPlaceTableWithRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ParkingPlaces_EmployeeId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "ParkingPlaceOperatorId",
                table: "ParkingPlaces",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParkingPlaceVerifierId",
                table: "ParkingPlaces",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaces_ParkingPlaceOperatorId",
                table: "ParkingPlaces",
                column: "ParkingPlaceOperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaces_ParkingPlaceVerifierId",
                table: "ParkingPlaces",
                column: "ParkingPlaceVerifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingPlaces_Employees_ParkingPlaceOperatorId",
                table: "ParkingPlaces",
                column: "ParkingPlaceOperatorId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingPlaces_Employees_ParkingPlaceVerifierId",
                table: "ParkingPlaces",
                column: "ParkingPlaceVerifierId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingPlaces_Employees_ParkingPlaceOperatorId",
                table: "ParkingPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingPlaces_Employees_ParkingPlaceVerifierId",
                table: "ParkingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_ParkingPlaces_ParkingPlaceOperatorId",
                table: "ParkingPlaces");

            migrationBuilder.DropIndex(
                name: "IX_ParkingPlaces_ParkingPlaceVerifierId",
                table: "ParkingPlaces");

            migrationBuilder.DropColumn(
                name: "ParkingPlaceOperatorId",
                table: "ParkingPlaces");

            migrationBuilder.DropColumn(
                name: "ParkingPlaceVerifierId",
                table: "ParkingPlaces");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ParkingPlaces_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                principalTable: "ParkingPlaces",
                principalColumn: "ParkingPlaceId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
