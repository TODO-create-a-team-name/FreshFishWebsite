using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class AddednewfiledFishQuantlytoPool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FishQuantly",
                table: "Pools",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FishQuantly",
                table: "Pools");
        }
    }
}
