using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class DriverTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    ContactNumber = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    AccountCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
