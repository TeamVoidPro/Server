using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "AccountCreatedAt", "AddressLine1", "City", "ContactNumber", "Email", "FirstName", "LastName", "Nic", "Password", "ProfilePicture", "Role", "Street", "Token" },
                values: new object[,]
                {
                    { "EMP001", new DateTime(2023, 7, 27, 10, 54, 6, 380, DateTimeKind.Utc).AddTicks(4296), "108/5 A", "Wadduwa", "0711234567", "danodya_s@yahoo.com", "Danodya", "Supun", "199914212942", "$2a$12$LqjfE1qq6njpKzjVdXrvguNksi14BSkgKnsjMTX6XhdjivtFypQg.", "https://i.imgur.com/1qk4XKn.jpg", "Operator", "Weragama Road", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDQ1NTI0NiwiZXhwIjoxNjkxMDYwMDQ2LCJpYXQiOjE2OTA0NTUyNDZ9.DNxjgHEp2hSAFTKT0B84iP6AQfH9zmMyJ03vOjzrlg8" },
                    { "EMP002", new DateTime(2023, 7, 27, 10, 54, 6, 759, DateTimeKind.Utc).AddTicks(6413), "108/5 A", "Wadduwa", "0711234567", "isudrikaarunodi99@gmail.com", "Isurika", "Arunodi", "199914212942", "$2a$12$6j7l1IMLhWrXv9jEbS2qdOfo/f8GlRaFVdzIT056KgBDOrCVz2fiu", "https://i.imgur.com/1qk4XKn.jpg", "Verifier", "Weragama Road", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDQ1NTI0NiwiZXhwIjoxNjkxMDYwMDQ2LCJpYXQiOjE2OTA0NTUyNDZ9.DNxjgHEp2hSAFTKT0B84iP6AQfH9zmMyJ03vOjzrlg8" },
                    { "EMP003", new DateTime(2023, 7, 27, 10, 54, 7, 147, DateTimeKind.Utc).AddTicks(5755), "108/5 A", "Wadduwa", "0711234567", "viharshapramodi@gmail.com", "Viharsha", "Pramodi", "199914212942", "$2a$12$fW5lrjcKEoFLrUJa274neuTN2ZiklIIKuFe6Nv8v9idNnCwiXoSeK", "https://i.imgur.com/1qk4XKn.jpg", "Administrator", "Weragama Road", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDQ1NTI0NywiZXhwIjoxNjkxMDYwMDQ3LCJpYXQiOjE2OTA0NTUyNDd9.pO-jDS5orHkOrHMsSX6CRs0U8DiRhCo60ZlDGbjRvnU" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP001");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP002");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP003");
        }
    }
}
