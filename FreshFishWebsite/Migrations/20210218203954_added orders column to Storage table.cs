using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class addedorderscolumntoStoragetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StorageId",
                table: "Orders",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Storages_StorageId",
                table: "Orders",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Storages_StorageId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StorageId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Orders");
        }
    }
}
