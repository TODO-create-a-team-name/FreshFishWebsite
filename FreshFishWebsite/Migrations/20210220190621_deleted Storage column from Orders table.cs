using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class deletedStoragecolumnfromOrderstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Orders_OrderId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_OrderId",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Storages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Storages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storages_OrderId",
                table: "Storages",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Orders_OrderId",
                table: "Storages",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
