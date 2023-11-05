using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class ClientTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientTypeId",
                table: "Processes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ClientTypeId",
                table: "Processes",
                column: "ClientTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_ClientTypes_ClientTypeId",
                table: "Processes",
                column: "ClientTypeId",
                principalTable: "ClientTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_ClientTypes_ClientTypeId",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Processes_ClientTypeId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ClientTypeId",
                table: "Processes");
        }
    }
}
