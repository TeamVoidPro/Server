using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ParkingPlaceOwnerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingPlaceOwners",
                columns: table => new
                {
                    OwnerId = table.Column<string>(type: "varchar(20)", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContactNumber = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    DeedCopy = table.Column<string>(type: "varchar(256)", nullable: false),
                    Nic = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPlaceOwners", x => x.OwnerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingPlaceOwners");
        }
    }
}
