using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class RegistryProcessOneProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "ReferenceProcesses");

            migrationBuilder.AddColumn<int>(
                name: "ReferenceProcessId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ReferenceProcessId",
                table: "Processes",
                column: "ReferenceProcessId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ReferenceProcesses_ReferenceProcessId",
                table: "Processes",
                column: "ReferenceProcessId",
                principalTable: "ReferenceProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ReferenceProcesses_ReferenceProcessId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_ReferenceProcessId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ReferenceProcessId",
                table: "Processes");

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "ReferenceProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
