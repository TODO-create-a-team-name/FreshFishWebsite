using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class minorchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStorage");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "OrderStorage",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    StoragesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStorage", x => new { x.OrdersId, x.StoragesId });
                    table.ForeignKey(
                        name: "FK_OrderStorage_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStorage_Storages_StoragesId",
                        column: x => x.StoragesId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStorage_StoragesId",
                table: "OrderStorage",
                column: "StoragesId");
        }
    }
}
