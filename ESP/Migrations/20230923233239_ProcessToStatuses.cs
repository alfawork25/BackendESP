using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class ProcessToStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
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

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesToStatuses_ProcessId",
                table: "ProcessesToStatuses",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessesToStatuses_StatusId",
                table: "ProcessesToStatuses",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessesToStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
