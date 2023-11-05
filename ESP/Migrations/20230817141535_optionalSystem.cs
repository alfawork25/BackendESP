using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class optionalSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemTypes_SystemTypeId",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "SystemTypeId",
                table: "Processes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SystemBlockId",
                table: "Processes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes",
                column: "SystemBlockId",
                principalTable: "SystemBlocks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_SystemTypes_SystemTypeId",
                table: "Processes",
                column: "SystemTypeId",
                principalTable: "SystemTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemBlocks_SystemBlockId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_SystemTypes_SystemTypeId",
                table: "Processes");

            migrationBuilder.AlterColumn<int>(
                name: "SystemTypeId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SystemBlockId",
                table: "Processes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
