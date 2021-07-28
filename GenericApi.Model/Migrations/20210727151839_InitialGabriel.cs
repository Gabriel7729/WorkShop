using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericApi.Model.Migrations
{
    public partial class InitialGabriel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Dob" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 7, 27, 15, 18, 38, 876, DateTimeKind.Unspecified).AddTicks(7734), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 7, 27, 11, 18, 38, 876, DateTimeKind.Local).AddTicks(9064) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "Dob" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 7, 26, 15, 24, 55, 583, DateTimeKind.Unspecified).AddTicks(6357), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2021, 7, 26, 11, 24, 55, 583, DateTimeKind.Local).AddTicks(6858) });
        }
    }
}
