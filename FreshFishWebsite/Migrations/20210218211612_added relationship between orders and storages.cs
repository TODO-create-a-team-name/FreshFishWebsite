using Microsoft.EntityFrameworkCore.Migrations;

namespace FreshFishWebsite.Migrations
{
    public partial class addedrelationshipbetweenordersandstorages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsOrderAssigned",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStorage");

            migrationBuilder.DropColumn(
                name: "IsOrderAssigned",
                table: "Orders");

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
    }
}
