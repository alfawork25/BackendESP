using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class CheckBlocksAndClientTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckBlocksAndClientTypes",
                columns: table => new
                {
                    CheckBlocksId = table.Column<int>(type: "int", nullable: false),
                    ClientTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBlocksAndClientTypes", x => new { x.CheckBlocksId, x.ClientTypesId });
                    table.ForeignKey(
                        name: "FK_CheckBlocksAndClientTypes_CheckBlocks_CheckBlocksId",
                        column: x => x.CheckBlocksId,
                        principalTable: "CheckBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckBlocksAndClientTypes_ClientTypes_ClientTypesId",
                        column: x => x.ClientTypesId,
                        principalTable: "ClientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckBlocksAndClientTypes_ClientTypesId",
                table: "CheckBlocksAndClientTypes",
                column: "ClientTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckBlocksAndClientTypes");
        }
    }
}
