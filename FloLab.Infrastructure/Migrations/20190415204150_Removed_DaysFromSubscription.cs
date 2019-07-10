using Microsoft.EntityFrameworkCore.Migrations;

namespace FloLab.Infrastructure.Migrations
{
    public partial class Removed_DaysFromSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Subscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Subscriptions",
                nullable: false,
                defaultValue: "");
        }
    }
}
