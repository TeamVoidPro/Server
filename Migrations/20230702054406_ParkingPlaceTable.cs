using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ParkingPlaceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingPlaces",
                columns: table => new
                {
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ParkingPlaceOwnerOwnerId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPlaces", x => x.ParkingPlaceId);
                    table.ForeignKey(
                        name: "FK_ParkingPlaces_ParkingPlaceOwners_ParkingPlaceOwnerOwnerId",
                        column: x => x.ParkingPlaceOwnerOwnerId,
                        principalTable: "ParkingPlaceOwners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaces_ParkingPlaceOwnerOwnerId",
                table: "ParkingPlaces",
                column: "ParkingPlaceOwnerOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ParkingPlaces_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                principalTable: "ParkingPlaces",
                principalColumn: "ParkingPlaceId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ParkingPlaces_EmployeeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ParkingPlaces");
        }
    }
}
