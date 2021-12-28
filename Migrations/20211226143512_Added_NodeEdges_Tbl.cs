using Microsoft.EntityFrameworkCore.Migrations;

namespace DepotManagementSystem.Migrations
{
    public partial class Added_NodeEdges_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NodeEdges",
                columns: table => new
                {
                    EdgeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EdgeLength = table.Column<int>(type: "int", nullable: false),
                    StartNode = table.Column<long>(type: "bigint", nullable: false),
                    EndNode = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeEdges", x => x.EdgeId);
                    table.ForeignKey(
                        name: "FK_NodeEdges_Nodes_EndNode",
                        column: x => x.EndNode,
                        principalTable: "Nodes",
                        principalColumn: "NodeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NodeEdges_Nodes_StartNode",
                        column: x => x.StartNode,
                        principalTable: "Nodes",
                        principalColumn: "NodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NodeEdges_EndNode",
                table: "NodeEdges",
                column: "EndNode");

            migrationBuilder.CreateIndex(
                name: "IX_NodeEdges_StartNode",
                table: "NodeEdges",
                column: "StartNode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeEdges");
        }
    }
}
