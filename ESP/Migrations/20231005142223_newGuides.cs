using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class newGuides : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Modification_ModificationId",
                table: "Processes");

            migrationBuilder.DropTable(
                name: "Modification");

            migrationBuilder.DropIndex(
                name: "IX_Processes_ModificationId",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "ModificationId",
                table: "Processes");

            migrationBuilder.CreateTable(
                name: "BlockTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DirectionDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<bool>(type: "bit", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedProfile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedProm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedWithNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Integrated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleOKBP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleTechnoglogist = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessInfoBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Current = table.Column<bool>(type: "bit", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    ProcessInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessInfoBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessInfoBlocks_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessOneLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessOneLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessTwoLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessTwoLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnologistBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RsOneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RsOneName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckCall = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologistBlocks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessInfoBlocks_StatusId",
                table: "ProcessInfoBlocks",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockTests");

            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "ProcessInfoBlocks");

            migrationBuilder.DropTable(
                name: "ProcessOneLevels");

            migrationBuilder.DropTable(
                name: "ProcessTwoLevels");

            migrationBuilder.DropTable(
                name: "TechnologistBlocks");

            migrationBuilder.AddColumn<int>(
                name: "ModificationId",
                table: "Processes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Modification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Processes_ModificationId",
                table: "Processes",
                column: "ModificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Modification_ModificationId",
                table: "Processes",
                column: "ModificationId",
                principalTable: "Modification",
                principalColumn: "Id");
        }
    }
}
