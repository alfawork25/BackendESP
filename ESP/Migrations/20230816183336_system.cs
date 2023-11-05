using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class system : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemBlockId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SystemTypeId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_SystemBlockId",
                table: "Processes",
                column: "SystemBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_SystemTypeId",
                table: "Processes",
                column: "SystemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes",
                column: "SystemBlockId",
                principalTable: "SystemBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_SystemTypes_SystemTypeId",
                table: "Processes",
                column: "SystemTypeId",
                principalTable: "SystemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemTypes_SystemTypeId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_SystemBlockId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_SystemTypeId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "SystemBlockId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "SystemTypeId",
                table: "Processes");
        }
    }
}
