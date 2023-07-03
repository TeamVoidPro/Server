using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class BookingReservationRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingPlanId",
                table: "BookingReservations",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReservations_BookingPlanId_ZonePlanId",
                table: "BookingReservations",
                columns: new[] { "BookingPlanId", "ZonePlanId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingReservations_ZonePlans_BookingPlanId_ZonePlanId",
                table: "BookingReservations",
                columns: new[] { "BookingPlanId", "ZonePlanId" },
                principalTable: "ZonePlans",
                principalColumns: new[] { "ZonePlanId", "BookingPlanId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingReservations_ZonePlans_BookingPlanId_ZonePlanId",
                table: "BookingReservations");

            migrationBuilder.DropIndex(
                name: "IX_BookingReservations_BookingPlanId_ZonePlanId",
                table: "BookingReservations");

            migrationBuilder.DropColumn(
                name: "BookingPlanId",
                table: "BookingReservations");
        }
    }
}
