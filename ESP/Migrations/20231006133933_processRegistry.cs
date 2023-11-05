using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class processRegistry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferenceProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemBlockId = table.Column<int>(type: "int", nullable: false),
                    ProcessOneLevelId = table.Column<int>(type: "int", nullable: true),
                    ProcessTwoLevelId = table.Column<int>(type: "int", nullable: true),
                    ProcessCodeThirdLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessNameThirdLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessReferenceUniqueName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReferenceProcesses_ProcessOneLevels_ProcessOneLevelId",
                        column: x => x.ProcessOneLevelId,
                        principalTable: "ProcessOneLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReferenceProcesses_ProcessTwoLevels_ProcessTwoLevelId",
                        column: x => x.ProcessTwoLevelId,
                        principalTable: "ProcessTwoLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReferenceProcesses_SystemBlocks_SystemBlockId",
                        column: x => x.SystemBlockId,
                        principalTable: "SystemBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceProcesses_ProcessOneLevelId",
                table: "ReferenceProcesses",
                column: "ProcessOneLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceProcesses_ProcessTwoLevelId",
                table: "ReferenceProcesses",
                column: "ProcessTwoLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceProcesses_SystemBlockId",
                table: "ReferenceProcesses",
                column: "SystemBlockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceProcesses");
        }
    }
}
