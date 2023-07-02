using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SlotCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlotCategories",
                columns: table => new
                {
                    SlotCategoryId = table.Column<string>(type: "varchar(20)", nullable: false),
                    SlotCategoryName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SlotCategoryDescription = table.Column<string>(type: "text", nullable: false),
                    CategoryCreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotCategories", x => x.SlotCategoryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlotCategories");
        }
    }
}
