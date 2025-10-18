using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace COMP2139_ICE.Migrations
{
    /// <inheritdoc />
    public partial class Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Description", "EndDate", "Name", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, "COMP2139 - Assignment 1", new DateTime(2025, 10, 17, 17, 59, 8, 517, DateTimeKind.Local).AddTicks(6693), "Assignment 1", new DateTime(2025, 10, 17, 17, 59, 8, 515, DateTimeKind.Local).AddTicks(8956), null },
                    { 2, "COMP2139 - Assignment 2", new DateTime(2025, 10, 17, 17, 59, 8, 517, DateTimeKind.Local).AddTicks(7601), "Assignment 2", new DateTime(2025, 10, 17, 17, 59, 8, 517, DateTimeKind.Local).AddTicks(7593), null }
                });
        }
    }
}
