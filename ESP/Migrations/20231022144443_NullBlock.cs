using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESP.Migrations
{
    public partial class NullBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_BlockTests_BlockTestId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Integrations_IntegrationId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Persons_PersonId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_TechnologistBlocks_TechnologistBlockId",
                table: "Revisions");

            migrationBuilder.AlterColumn<int>(
                name: "TechnologistBlockId",
                table: "Revisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Revisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IntegrationId",
                table: "Revisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BlockTestId",
                table: "Revisions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_BlockTests_BlockTestId",
                table: "Revisions",
                column: "BlockTestId",
                principalTable: "BlockTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Integrations_IntegrationId",
                table: "Revisions",
                column: "IntegrationId",
                principalTable: "Integrations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Persons_PersonId",
                table: "Revisions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_TechnologistBlocks_TechnologistBlockId",
                table: "Revisions",
                column: "TechnologistBlockId",
                principalTable: "TechnologistBlocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_BlockTests_BlockTestId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Integrations_IntegrationId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_Persons_PersonId",
                table: "Revisions");

            migrationBuilder.DropForeignKey(
                name: "FK_Revisions_TechnologistBlocks_TechnologistBlockId",
                table: "Revisions");

            migrationBuilder.AlterColumn<int>(
                name: "TechnologistBlockId",
                table: "Revisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Revisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IntegrationId",
                table: "Revisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlockTestId",
                table: "Revisions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_BlockTests_BlockTestId",
                table: "Revisions",
                column: "BlockTestId",
                principalTable: "BlockTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Integrations_IntegrationId",
                table: "Revisions",
                column: "IntegrationId",
                principalTable: "Integrations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_Persons_PersonId",
                table: "Revisions",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Revisions_TechnologistBlocks_TechnologistBlockId",
                table: "Revisions",
                column: "TechnologistBlockId",
                principalTable: "TechnologistBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
