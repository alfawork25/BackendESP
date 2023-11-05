using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class RevisionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Processes_ProcessId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_ProcessId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "Statuses");

            migrationBuilder.AddColumn<string>(
                name: "RevisionType",
                table: "Revisions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RevisionType",
                table: "Revisions");

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "Statuses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_ProcessId",
                table: "Statuses",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Processes_ProcessId",
                table: "Statuses",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id");
        }
    }
}
