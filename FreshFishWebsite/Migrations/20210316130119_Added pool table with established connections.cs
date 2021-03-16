using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class Addedpooltablewithestablishedconnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PoolId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolNumber = table.Column<int>(type: "int", nullable: false),
                    WaterTemperatureCelsius = table.Column<double>(type: "float", nullable: false),
                    NitrogenLevel = table.Column<double>(type: "float", nullable: false),
                    IsFishFed = table.Column<bool>(type: "bit", nullable: false),
                    StorageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pools_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PoolId",
                table: "Products",
                column: "PoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Pools_StorageId",
                table: "Pools",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pools_PoolId",
                table: "Products",
                column: "PoolId",
                principalTable: "Pools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pools_PoolId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropIndex(
                name: "IX_Products_PoolId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PoolId",
                table: "Products");
        }
    }
}
