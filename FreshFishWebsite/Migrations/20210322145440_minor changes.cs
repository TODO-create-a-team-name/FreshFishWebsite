using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class minorchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalProductWeight",
                table: "ProductsInPool",
                newName: "TotalProductQuantityKg");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityKg",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "RemainingQuantityKg",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingQuantityKg",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "TotalProductQuantityKg",
                table: "ProductsInPool",
                newName: "TotalProductWeight");

            migrationBuilder.AlterColumn<double>(
                name: "QuantityKg",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
