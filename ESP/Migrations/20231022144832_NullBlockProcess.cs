using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class NullBlockProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_ProcessInfoBlocks_ProcessInfoBlockId",
                table: "Revisions");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessInfoBlockId",
                table: "Revisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_ProcessInfoBlocks_ProcessInfoBlockId",
                table: "Revisions",
                column: "ProcessInfoBlockId",
                principalTable: "ProcessInfoBlocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_ProcessInfoBlocks_ProcessInfoBlockId",
                table: "Revisions");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessInfoBlockId",
                table: "Revisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_ProcessInfoBlocks_ProcessInfoBlockId",
                table: "Revisions",
                column: "ProcessInfoBlockId",
                principalTable: "ProcessInfoBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
