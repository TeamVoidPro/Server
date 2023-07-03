using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ParkingPlaceSlotCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingPlaceSlotCapacities",
                columns: table => new
                {
                    ParkingPlaceId = table.Column<string>(type: "varchar(20)", nullable: false),
                    SlotCategoryId = table.Column<string>(type: "varchar(20)", nullable: false),
                    SlotCapacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPlaceSlotCapacities", x => new { x.ParkingPlaceId, x.SlotCategoryId });
                    table.ForeignKey(
                        name: "FK_ParkingPlaceSlotCapacities_ParkingPlaces_ParkingPlaceId",
                        column: x => x.ParkingPlaceId,
                        principalTable: "ParkingPlaces",
                        principalColumn: "ParkingPlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParkingPlaceSlotCapacities_SlotCategories_SlotCategoryId",
                        column: x => x.SlotCategoryId,
                        principalTable: "SlotCategories",
                        principalColumn: "SlotCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPlaceSlotCapacities_SlotCategoryId",
                table: "ParkingPlaceSlotCapacities",
                column: "SlotCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingPlaceSlotCapacities");
        }
    }
}
