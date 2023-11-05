using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class CheckCodesAndSubjectTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckCodesAndSubjectTypes",
                columns: table => new
                {
                    CheckCodesId = table.Column<int>(type: "int", nullable: false),
                    SubjectTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckCodesAndSubjectTypes", x => new { x.CheckCodesId, x.SubjectTypesId });
                    table.ForeignKey(
                        name: "FK_CheckCodesAndSubjectTypes_CheckCodes_CheckCodesId",
                        column: x => x.CheckCodesId,
                        principalTable: "CheckCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckCodesAndSubjectTypes_SubjectTypes_SubjectTypesId",
                        column: x => x.SubjectTypesId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckCodesAndSubjectTypes_SubjectTypesId",
                table: "CheckCodesAndSubjectTypes",
                column: "SubjectTypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckCodesAndSubjectTypes");
        }
    }
}
