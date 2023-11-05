using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Steps",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StepNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    CheckBlockId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_StepNumbers_CheckBlockId",
                table: "StepNumbers",
                column: "CheckBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_StepNumbers_ProcessId",
                table: "StepNumbers",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StepNumbers");

            migrationBuilder.DropColumn(
                name: "Steps",
                table: "Processes");
        }
    }
}
