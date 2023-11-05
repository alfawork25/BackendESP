using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class deleteStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessesToStatuses");

            migrationBuilder.DropTable(
                name: "ProcessStatus");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProcessesToStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessesToStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessesToStatuses_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessesToStatuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ProcessesToStatuses_ProcessId",
                table: "ProcessesToStatuses",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesToStatuses_StatusId",
                table: "ProcessesToStatuses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessStatus_StatusesId",
                table: "ProcessStatus",
                column: "StatusesId");
        }
    }
}
