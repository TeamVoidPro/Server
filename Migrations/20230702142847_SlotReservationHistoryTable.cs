using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SlotReservationHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlotReservationHistories",
                columns: table => new
                {
                    SlotReservationHistoryId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservationId = table.Column<string>(type: "varchar(20)", nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReservationEndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SlotId = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotReservationHistories", x => x.SlotReservationHistoryId);
                    table.ForeignKey(
                        name: "FK_SlotReservationHistories_Slots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "Slots",
                        principalColumn: "SlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlotReservationHistories_SlotId",
                table: "SlotReservationHistories",
                column: "SlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlotReservationHistories");
        }
    }
}
