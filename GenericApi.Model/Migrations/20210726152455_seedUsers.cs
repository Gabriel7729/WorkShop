using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericApi.Model.Migrations
{
    public partial class seedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Deleted", "DeletedBy", "DeletedDate", "Dob", "DocumentType", "DocumentTypeValue", "Gender", "LastName", "MiddleName", "Name", "Password", "PhotoId", "SecondLastName", "UpdatedBy", "UpdatedDate", "UserName" },
                values: new object[] { 1, null, new DateTimeOffset(new DateTime(2021, 7, 26, 15, 24, 55, 583, DateTimeKind.Unspecified).AddTicks(6357), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, new DateTime(2021, 7, 26, 11, 24, 55, 583, DateTimeKind.Local).AddTicks(6858), 0, "00000000000", 0, "Admin", "Admin", "Admin", "Hola123,", null, "Admin", null, null, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
