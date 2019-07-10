using Microsoft.EntityFrameworkCore.Migrations;

namespace FloLab.Infrastructure.Migrations
{
    public partial class UserDay_UserId_String : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDays_AspNetUsers_UserId1",
                table: "UserDays");

            migrationBuilder.DropIndex(
                name: "IX_UserDays_UserId1",
                table: "UserDays");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserDays");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserDays",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_UserDays_UserId",
                table: "UserDays",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDays_AspNetUsers_UserId",
                table: "UserDays",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDays_AspNetUsers_UserId",
                table: "UserDays");

            migrationBuilder.DropIndex(
                name: "IX_UserDays_UserId",
                table: "UserDays");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserDays",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserDays",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDays_UserId1",
                table: "UserDays",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDays_AspNetUsers_UserId1",
                table: "UserDays",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
