using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Revisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckDate",
                table: "BlockTests");

            migrationBuilder.CreateTable(
                name: "Revisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    ProcessInfoBlockId = table.Column<int>(type: "int", nullable: false),
                    TechnologistBlockId = table.Column<int>(type: "int", nullable: false),
                    BlockTestId = table.Column<int>(type: "int", nullable: false),
                    IntegrationId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisions_BlockTests_BlockTestId",
                        column: x => x.BlockTestId,
                        principalTable: "BlockTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisions_Integrations_IntegrationId",
                        column: x => x.IntegrationId,
                        principalTable: "Integrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisions_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisions_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisions_ProcessInfoBlocks_ProcessInfoBlockId",
                        column: x => x.ProcessInfoBlockId,
                        principalTable: "ProcessInfoBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Revisions_TechnologistBlocks_TechnologistBlockId",
                        column: x => x.TechnologistBlockId,
                        principalTable: "TechnologistBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_BlockTestId",
                table: "Revisions",
                column: "BlockTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_IntegrationId",
                table: "Revisions",
                column: "IntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_PersonId",
                table: "Revisions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_ProcessId",
                table: "Revisions",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_ProcessInfoBlockId",
                table: "Revisions",
                column: "ProcessInfoBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_TechnologistBlockId",
                table: "Revisions",
                column: "TechnologistBlockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Revisions");

            migrationBuilder.AddColumn<string>(
                name: "CheckDate",
                table: "BlockTests",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
