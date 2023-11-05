using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class deleteForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes");

            migrationBuilder.DropTable(
                name: "StepNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Processes_SystemBlockId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "SystemBlockId",
                table: "Processes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemBlockId",
                table: "Processes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StepNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckBlockId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepNumbers_CheckBlocks_CheckBlockId",
                        column: x => x.CheckBlockId,
                        principalTable: "CheckBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepNumbers_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_SystemBlockId",
                table: "Processes",
                column: "SystemBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_StepNumbers_CheckBlockId",
                table: "StepNumbers",
                column: "CheckBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_StepNumbers_ProcessId",
                table: "StepNumbers",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes",
                column: "SystemBlockId",
                principalTable: "SystemBlocks",
                principalColumn: "Id");
        }
    }
}
