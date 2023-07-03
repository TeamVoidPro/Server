using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class BookingReservationRelationships2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookingPlanId",
                table: "Vehicles",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZonePlanId",
                table: "Vehicles",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_BookingPlanId_ZonePlanId",
                table: "Vehicles",
                columns: new[] { "BookingPlanId", "ZonePlanId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ZonePlans_BookingPlanId_ZonePlanId",
                table: "Vehicles",
                columns: new[] { "BookingPlanId", "ZonePlanId" },
                principalTable: "ZonePlans",
                principalColumns: new[] { "ZonePlanId", "BookingPlanId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ZonePlans_BookingPlanId_ZonePlanId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_BookingPlanId_ZonePlanId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "BookingPlanId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ZonePlanId",
                table: "Vehicles");
        }
    }
}
