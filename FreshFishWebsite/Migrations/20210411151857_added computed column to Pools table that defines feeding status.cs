using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class addedcomputedcolumntoPoolstablethatdefinesfeedingstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeFeedingExpired",
                table: "Feedings");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeFeedingExpired",
                table: "Pools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeFeedingExpired",
                table: "Pools");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeFeedingExpired",
                table: "Feedings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
