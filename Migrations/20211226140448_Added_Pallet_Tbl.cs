using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DepotManagementSystem.Migrations
{
    public partial class Added_Pallet_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pallets",
                columns: table => new
                {
                    PalletId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    LPNId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallets", x => x.PalletId);
                    table.ForeignKey(
                        name: "FK_Pallets_LPNs_LPNId",
                        column: x => x.LPNId,
                        principalTable: "LPNs",
                        principalColumn: "LPNId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pallets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_LPNId",
                table: "Pallets",
                column: "LPNId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_ProductId",
                table: "Pallets",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pallets");
        }
    }
}
