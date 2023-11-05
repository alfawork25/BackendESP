using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class DeleteSystemBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceProcesses_SystemBlocks_SystemBlockId",
                table: "ReferenceProcesses");

            migrationBuilder.DropColumn(
                name: "ReferenceProcessId",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "SystemBlockId",
                table: "ReferenceProcesses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceProcesses_SystemBlocks_SystemBlockId",
                table: "ReferenceProcesses",
                column: "SystemBlockId",
                principalTable: "SystemBlocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceProcesses_SystemBlocks_SystemBlockId",
                table: "ReferenceProcesses");

            migrationBuilder.AlterColumn<int>(
                name: "SystemBlockId",
                table: "ReferenceProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReferenceProcessId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceProcesses_SystemBlocks_SystemBlockId",
                table: "ReferenceProcesses",
                column: "SystemBlockId",
                principalTable: "SystemBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
