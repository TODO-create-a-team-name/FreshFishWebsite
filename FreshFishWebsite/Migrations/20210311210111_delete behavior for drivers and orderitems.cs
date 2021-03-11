using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class deletebehaviorfordriversandorderitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Drivers_DriverId",
                table: "OrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Drivers_DriverId",
                table: "OrderItems",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Drivers_DriverId",
                table: "OrderItems");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Drivers_DriverId",
                table: "OrderItems",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
