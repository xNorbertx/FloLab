using Microsoft.EntityFrameworkCore.Migrations;

namespace FloLab.Infrastructure.Migrations
{
    public partial class RemovedEntryFromContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscription_SubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subscriptions_SubscriptionId",
                table: "AspNetUsers",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscriptions_SubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subscription_SubscriptionId",
                table: "AspNetUsers",
                column: "SubscriptionId",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
