using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class deleterestrictproductandproductsinpool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Pools_PoolId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PoolId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PoolId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "MaxProductsKg",
                table: "Pools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductsInPool",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PoolId = table.Column<int>(type: "int", nullable: false),
                    TotalProductWeight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsInPool", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsInPool_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsInPool_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInPool_PoolId",
                table: "ProductsInPool",
                column: "PoolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInPool_ProductId",
                table: "ProductsInPool",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsInPool");

            migrationBuilder.DropColumn(
                name: "MaxProductsKg",
                table: "Pools");

            migrationBuilder.AddColumn<int>(
                name: "PoolId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PoolId",
                table: "Products",
                column: "PoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Pools_PoolId",
                table: "Products",
                column: "PoolId",
                principalTable: "Pools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
