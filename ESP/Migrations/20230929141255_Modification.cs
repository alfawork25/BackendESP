using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class Modification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
