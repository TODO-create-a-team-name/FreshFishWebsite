using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class setnulltorefindrivertabletostoragewhendeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Storages_StorageId",
                table: "Drivers");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Storages_StorageId",
                table: "Drivers",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Storages_StorageId",
                table: "Drivers");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Storages_StorageId",
                table: "Drivers",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
