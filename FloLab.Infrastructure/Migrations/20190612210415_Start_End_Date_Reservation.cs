using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloLab.Infrastructure.Migrations
{
    public partial class Start_End_Date_Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reservations",
                newName: "Start");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Reservations",
                newName: "Date");
        }
    }
}
