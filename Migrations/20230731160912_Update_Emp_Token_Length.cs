using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmpTokenLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "Employees",
                type: "varchar(512)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP001",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 31, 16, 9, 11, 622, DateTimeKind.Utc).AddTicks(425), "$2a$12$ltpBfXDJSBaJQcOatBBxrOxcEE8WAFt29lp4G94Bpgr5ZUlKZZz8u", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDgxOTc1MSwiZXhwIjoxNjkxNDI0NTUxLCJpYXQiOjE2OTA4MTk3NTF9.5YglAau3MzcDi8hKnJ7E1RDt3CPHRAeaSGAln_jmHgg" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP002",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 31, 16, 9, 12, 5, DateTimeKind.Utc).AddTicks(5002), "$2a$12$8buBOyFR/re6ijx/K8Ri0.TpEXqKnMNUJcXxvfViFpS.pr.44CCzS", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDgxOTc1MiwiZXhwIjoxNjkxNDI0NTUyLCJpYXQiOjE2OTA4MTk3NTJ9.6vBaxQ3iMBWaO4nfXzsvGkAfcgqcj9SWZxphklJ5pdI" });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: "EMP003",
                columns: new[] { "AccountCreatedAt", "Password", "Token" },
                values: new object[] { new DateTime(2023, 7, 31, 16, 9, 12, 394, DateTimeKind.Utc).AddTicks(357), "$2a$12$Ts2xFNj/wKGMX168Zvr1j.SEp0u2u5H.avmbaWnVXSKQZmcF8t0B2", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEyMyIsIm5iZiI6MTY5MDgxOTc1MiwiZXhwIjoxNjkxNDI0NTUyLCJpYXQiOjE2OTA4MTk3NTJ9.6vBaxQ3iMBWaO4nfXzsvGkAfcgqcj9SWZxphklJ5pdI" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "Employees",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(512)");

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
    }
}
