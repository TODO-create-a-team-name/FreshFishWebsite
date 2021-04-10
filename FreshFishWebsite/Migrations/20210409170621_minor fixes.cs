using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class minorfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoolState_Pools_PoolId",
                table: "PoolState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PoolState",
                table: "PoolState");

            migrationBuilder.DropColumn(
                name: "NitrogenLevel",
                table: "Pools");

            migrationBuilder.DropColumn(
                name: "WaterTemperatureCelsius",
                table: "Pools");

            migrationBuilder.RenameTable(
                name: "PoolState",
                newName: "PoolStates");

            migrationBuilder.RenameIndex(
                name: "IX_PoolState_PoolId",
                table: "PoolStates",
                newName: "IX_PoolStates_PoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PoolStates",
                table: "PoolStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PoolStates_Pools_PoolId",
                table: "PoolStates",
                column: "PoolId",
                principalTable: "Pools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoolStates_Pools_PoolId",
                table: "PoolStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PoolStates",
                table: "PoolStates");

            migrationBuilder.RenameTable(
                name: "PoolStates",
                newName: "PoolState");

            migrationBuilder.RenameIndex(
                name: "IX_PoolStates_PoolId",
                table: "PoolState",
                newName: "IX_PoolState_PoolId");

            migrationBuilder.AddColumn<double>(
                name: "NitrogenLevel",
                table: "Pools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WaterTemperatureCelsius",
                table: "Pools",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PoolState",
                table: "PoolState",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PoolState_Pools_PoolId",
                table: "PoolState",
                column: "PoolId",
                principalTable: "Pools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
