using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloLab.Infrastructure.Migrations
{
    public partial class ChangedSubscriptionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscriptions_SubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Subscription",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subscription",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscription_SubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subscription");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SubscriptionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subscriptions_SubscriptionId",
                table: "AspNetUsers",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubscriptionTypeId",
                table: "Subscriptions",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
