using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloLab.Infrastructure.Migrations
{
    public partial class SpacesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Workdays");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Reservations",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reservations",
                newName: "SpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                newName: "IX_Reservations_SpaceId");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Spaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaces", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Spaces_SpaceId",
                table: "Reservations",
                column: "SpaceId",
                principalTable: "Spaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Spaces_SpaceId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "Spaces");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SpaceId",
                table: "Reservations",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reservations",
                newName: "To");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_SpaceId",
                table: "Reservations",
                newName: "IX_Reservations_RoomId");

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workdays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workdays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workdays_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workdays_UserId1",
                table: "Workdays",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
