using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Add_Statuses_to_Processes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRevisionStatus",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "PrimaryConnectionStatus",
                table: "Processes");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProcessStatus",
                columns: table => new
                {
                    ProcessesId = table.Column<int>(type: "int", nullable: false),
                    StatusesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessStatus", x => new { x.ProcessesId, x.StatusesId });
                    table.ForeignKey(
                        name: "FK_ProcessStatus_Processes_ProcessesId",
                        column: x => x.ProcessesId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessStatus_Statuses_StatusesId",
                        column: x => x.StatusesId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStatus_StatusesId",
                table: "ProcessStatus",
                column: "StatusesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessStatus");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Statuses");

            migrationBuilder.AddColumn<string>(
                name: "LastRevisionStatus",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryConnectionStatus",
                table: "Processes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
