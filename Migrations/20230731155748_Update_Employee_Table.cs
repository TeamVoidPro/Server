using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP001",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 31, 15, 57, 47, 98, DateTimeKind.Utc).AddTicks(9583), "$2a$12$k7cht4f1R1fr1nRovujAxeDHQV3T4QUh17cnvNPg85O9O8EZNvFvy", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDgxOTA2NywiZXhwIjoxNjkxNDIzODY3LCJpYXQiOjE2OTA4MTkwNjd9.bzHA26g7BK6K-flxy70JdZcEIfrsA4MlwfpO4iuH9Ss" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP002",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 31, 15, 57, 47, 477, DateTimeKind.Utc).AddTicks(5536), "$2a$12$Dsykn7WAJ5LNWJ9/EEfKNeRlxRo6s.RyOG9CCY6BS81X/eKiEE2Zq", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDgxOTA2NywiZXhwIjoxNjkxNDIzODY3LCJpYXQiOjE2OTA4MTkwNjd9.bzHA26g7BK6K-flxy70JdZcEIfrsA4MlwfpO4iuH9Ss" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP003",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 31, 15, 57, 47, 858, DateTimeKind.Utc).AddTicks(6245), "$2a$12$TWbzt7PJmD40UXyLgkWmve9MMTvBwaLjh32KkNFchATFkKa1EITPO", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDgxOTA2NywiZXhwIjoxNjkxNDIzODY3LCJpYXQiOjE2OTA4MTkwNjd9.bzHA26g7BK6K-flxy70JdZcEIfrsA4MlwfpO4iuH9Ss" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP001",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 27, 10, 54, 6, 380, DateTimeKind.Utc).AddTicks(4296), "$2a$12$LqjfE1qq6njpKzjVdXrvguNksi14BSkgKnsjMTX6XhdjivtFypQg.", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDQ1NTI0NiwiZXhwIjoxNjkxMDYwMDQ2LCJpYXQiOjE2OTA0NTUyNDZ9.DNxjgHEp2hSAFTKT0B84iP6AQfH9zmMyJ03vOjzrlg8" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP002",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 27, 10, 54, 6, 759, DateTimeKind.Utc).AddTicks(6413), "$2a$12$6j7l1IMLhWrXv9jEbS2qdOfo/f8GlRaFVdzIT056KgBDOrCVz2fiu", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDQ1NTI0NiwiZXhwIjoxNjkxMDYwMDQ2LCJpYXQiOjE2OTA0NTUyNDZ9.DNxjgHEp2hSAFTKT0B84iP6AQfH9zmMyJ03vOjzrlg8" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP003",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 27, 10, 54, 7, 147, DateTimeKind.Utc).AddTicks(5755), "$2a$12$fW5lrjcKEoFLrUJa274neuTN2ZiklIIKuFe6Nv8v9idNnCwiXoSeK", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDQ1NTI0NywiZXhwIjoxNjkxMDYwMDQ3LCJpYXQiOjE2OTA0NTUyNDd9.pO-jDS5orHkOrHMsSX6CRs0U8DiRhCo60ZlDGbjRvnU" });
        }
    }
}
